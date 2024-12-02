using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using Multiplayer.Player_Multi;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class NetGameManager : MonoBehaviour, INetworkRunnerCallbacks
{
    private NetworkRunner _runner;
    [SerializeField] private NetworkPrefabRef playerPref;
    [Networked, Capacity(2)]private NetworkDictionary<PlayerRef, NetworkObject> _assignedPlayers =>  new NetworkDictionary<PlayerRef, NetworkObject>();
    
    public void InitializeMultiplayer(GameMode mode) => StartGame(mode);
    
    private void SyncUIScene()
    {
        if (_runner.IsServer)
        {
            if (!SceneManager.GetSceneByName("UI").isLoaded)
            {
                SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
            }
        }
    }
    
    private async void StartGame(GameMode mode)
    {
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;

        var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
        var sceneInfo = new NetworkSceneInfo();
        if (scene.IsValid) {
            sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
        }

        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = "TestRoom",
            Scene = scene, 
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }
    
    private void OnGUI()
    {
        if (_runner == null)
        {
            if (GUI.Button(new Rect(0, 0, 200, 40), "Host"))
            {
                StartGame(GameMode.Host);
            }
            if (GUI.Button(new Rect(0, 40, 200, 40), "Join"))
            {
                StartGame(GameMode.Client);
            }
        }
    }
    
    
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"Player joined: {player.PlayerId}");

        if (runner.IsServer)
        {
            Debug.Log("Server is spawning the player object.");

            if (playerPref == null)
            {
                Debug.LogError("Player prefab reference is null! Make sure it is assigned in the inspector.");
                return;
            }

            Vector3 spawnPosition = new Vector3((player.RawEncoded % runner.Config.Simulation.PlayerCount) * 3, 1, 0);
            try
            {
                NetworkObject networkPlayerObject = runner.Spawn(playerPref, spawnPosition, Quaternion.identity, player);
                Debug.Log("Player object spawned successfully.");
                _assignedPlayers.Add(player, networkPlayerObject);
                networkPlayerObject.GetComponent<PlayerMulti>().SetPlayerId(player.PlayerId);
                networkPlayerObject.GetComponent<PlayerMulti>().SetReady(true);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to spawn player object: {ex.Message}");
            }
        }
    }


    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            if (_assignedPlayers.TryGet(player, out NetworkObject networkObject))
            {
                _assignedPlayers.Remove(player);
                runner.Despawn(networkObject);
            }
        }
    }
    

    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("Client connected to server.");
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        Debug.LogError($"Client disconnected: {reason}");
    }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
}