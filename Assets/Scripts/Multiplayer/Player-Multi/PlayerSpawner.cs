using System.Collections.Generic;
using Fusion;
using Multiplayer.Player_Multi;
using UnityEngine;
using UnityEngine.Serialization;
using static Unity.Collections.Unicode;

public class PlayerSpawner : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
       [SerializeField] private NetworkPrefabRef _granadeNetworkPrefab = NetworkPrefabRef.Empty;
        [SerializeField] private GameObject[] _spawnPoints;

        private bool _gameIsReady;
        private GameStateController _gameStateController;
        private int index;
        private HashSet<int> usedSpawnPoints = new HashSet<int>();

    
        // The spawner is started when the GameStateController switches to GameState.Running.
        public void StartPlayerSpawner(GameStateController gameStateController)
        {
            _gameIsReady = true;
            _gameStateController = gameStateController;
            foreach (var player in Runner.ActivePlayers)
            {
                SpawnPlayer(player);
            }
        }

        // Spawns a new spaceship if a client joined after the game already started
        public void PlayerJoined(PlayerRef player)
        {
            if (_gameIsReady == false) return;
            SpawnPlayer(player);
        }

        // Spawns a granade for a player.
        // The spawn point is chosen in the _spawnPoints array using the implicit playerRef to int conversion 
        private void SpawnPlayer(PlayerRef player)
        {
            int index = player.PlayerId == 0 ? 0 : 1;
            if (usedSpawnPoints.Contains(index))
            {
                index = index == 0 ? 1 : 0;
                
                if (index >= _spawnPoints.Length)
                {
                    Debug.LogError($"Spawn point for index {index} does not exist.");
                    return;
                }
            }
            
            usedSpawnPoints.Add(index);
            
            Vector3 spawnPosition = _spawnPoints[index].transform.position;

            NetworkObject playerObject = Runner.Spawn(_granadeNetworkPrefab, spawnPosition, Quaternion.identity, player);
            Runner.SetPlayerObject(player, playerObject);
            PlayerMulti playerMulti = playerObject.GetComponent<PlayerMulti>();
            if (playerMulti != null)
            {
                _gameStateController.TrackNewPlayer(playerMulti.Id);
            }
        }

        // Despawns the spaceship associated with a player when their client leaves the game session.
        public void PlayerLeft(PlayerRef player)
        {
            DespawnGranade(player);
        }

        private void DespawnGranade(PlayerRef player)
        {
            if (Runner.TryGetPlayerObject(player, out var spaceshipNetworkObject))
            {
                Runner.Despawn(spaceshipNetworkObject);
            }
            Runner.SetPlayerObject(player, null);
        }
}
