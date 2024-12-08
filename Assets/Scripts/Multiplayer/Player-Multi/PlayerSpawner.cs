using Fusion;
using Multiplayer.Player_Multi;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSpawner : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
       [SerializeField] private NetworkPrefabRef _granadeNetworkPrefab = NetworkPrefabRef.Empty;
        [SerializeField] private GameObject[] _spawnPoints = null;

        private bool _gameIsReady = false;
        private GameStateController _gameStateController = null;


        public override void Spawned()
        {
            if (Object.HasStateAuthority == false) return;
        }

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
            int index = player.PlayerId % _spawnPoints.Length;
            var spawnPosition = _spawnPoints[index].transform.position;

            NetworkObject playerObject = Runner.Spawn(_granadeNetworkPrefab, spawnPosition, Quaternion.identity, player);
            Runner.SetPlayerObject(player, playerObject);
            _gameStateController.TrackNewPlayer(playerObject.GetComponent<PlayerMulti>().Id);
        }

        // Despawns the spaceship associated with a player when their client leaves the game session.
        public void PlayerLeft(PlayerRef player)
        {
            DespawnSpaceship(player);
        }

        private void DespawnSpaceship(PlayerRef player)
        {
            if (Runner.TryGetPlayerObject(player, out var spaceshipNetworkObject))
            {
                Runner.Despawn(spaceshipNetworkObject);
            }

            Runner.SetPlayerObject(player, null);
        }
}
