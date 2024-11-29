using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using Multiplayer.Player_Multi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetGameManager : MonoBehaviour, INetworkRunnerCallbacks
{
    private NetworkRunner _runner;
    [SerializeField] private NetworkObject _playerOneObject;
    [SerializeField] private NetworkObject _playerTwoObject;
    private Dictionary<PlayerRef, NetworkObject> _assignedPlayers = new Dictionary<PlayerRef, NetworkObject>();

    
    public void InitializeMultiplayer(GameMode mode)
    {
        StartGame(mode);
    }
    
    
    private void SyncUIScene()
    {
        if (_runner.IsServer)
        {
            var uiScene = SceneManager.GetSceneByName("UI");
            if (!uiScene.isLoaded)
            {
                SceneManager.LoadScene("UI", LoadSceneMode.Additive);
            }
        }
    }
    
    private async void StartGame(GameMode mode)
    {
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;

        var startGameArgs = new StartGameArgs
        {
            GameMode = mode,
            SessionName = "TestRoom",
            Scene = null, 
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        };

        Debug.Log("Starting game...");
        var result = await _runner.StartGame(startGameArgs);
        if (result.Ok)
        {
            SyncUIScene(); 
            Debug.Log("Game started successfully.");
        }
        else
        {
            Debug.LogError($"Failed to start game: {result.ShutdownReason}");
        }
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
        if (runner.IsServer)
        {
            int playerId = player.PlayerId;
            NetworkObject playerObject = playerId == 1 ? _playerOneObject : _playerTwoObject;

            if (playerObject == null || !playerObject.IsValid)
            {
                Debug.LogError($"PlayerObject for PlayerId {playerId} is invalid.");
                return;
            }

            _assignedPlayers[player] = playerObject;
            runner.SetPlayerObject(player, playerObject);
            playerObject.AssignInputAuthority(player);

            var playerMulti = playerObject.GetComponent<PlayerMulti>();
            if (playerMulti != null)
            {
                playerMulti.SetPlayerId(playerId);
                playerMulti.SetReady(true);
            }
            else
            {
                Debug.LogError($"PlayerObject does not contain a PlayerMulti component for PlayerId {playerId}.");
            }
        }
    }




    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (_assignedPlayers.TryGetValue(player, out NetworkObject assignedObject))
        {
            //assignedObject.ClearInputAuthority();
            _assignedPlayers.Remove(player);
        }
    }


    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
}