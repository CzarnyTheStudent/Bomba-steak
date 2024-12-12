using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

public class NetworkInputHandler : SimulationBehaviour, IBeforeUpdate, INetworkRunnerCallbacks
{
    private Touch _currentTouch;
    private Vector3 _touchPosition;
    private NetworkInputData.TouchState _touchState;
    private Dictionary<PlayerRef, NetworkInputData> _playerInputs = new Dictionary<PlayerRef, NetworkInputData>();

    
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        // Obsługa zdarzenia opuszczenia AOI
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        // Obsługa zdarzenia wejścia AOI
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (!_playerInputs.ContainsKey(player))
        {
            _playerInputs[player] = new NetworkInputData
            {
                touchPos = Vector3.zero,
                touchState = NetworkInputData.TouchState.None
            };
        }
    }


    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
    }

    void IBeforeUpdate.BeforeUpdate()
    {
        if (Input.touchCount > 0)
        {
            _currentTouch = Input.GetTouch(0);
            _touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(
                _currentTouch.position.x,
                _currentTouch.position.y,
                Mathf.Abs(Camera.main.transform.position.z - transform.position.z)
            ));

            switch (_currentTouch.phase)
            {
                case TouchPhase.Began:
                    _touchState = NetworkInputData.TouchState.Began;
                    break;

                case TouchPhase.Moved:
                    _touchState = NetworkInputData.TouchState.Moved;
                    break;

                case TouchPhase.Ended:
                    _touchState = NetworkInputData.TouchState.Ended;
                    break;
                
                default:
                    _touchState = NetworkInputData.TouchState.None;
                    break;
            }
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        NetworkInputData localInput = new NetworkInputData();
        

        localInput.touchPos = _touchPosition;
        localInput.touchState = _touchState;
        input.Set(localInput);
    }


    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
     
    }



    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
       
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
       
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
        
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
        
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        
    }
}