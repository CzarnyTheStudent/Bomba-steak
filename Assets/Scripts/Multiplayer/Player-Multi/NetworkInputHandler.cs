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
    private bool _isDragging;
    
    
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
      
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        throw new NotImplementedException();
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
                    _isDragging = true;
                    break;

                case TouchPhase.Moved:
                    if (_isDragging)
                        _touchState = NetworkInputData.TouchState.Moved;
                    break;

                case TouchPhase.Ended:
                    _touchState = NetworkInputData.TouchState.Ended;
                    _isDragging = false;
                    break;

                default:
                    break;
            }
        }
        else
        {
            _isDragging = false;
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkInputData
        {
            touchPos = _touchPosition,
            touchState = _touchState,
            IsDragging = _isDragging
        };

        input.Set(data);
    }


    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        throw new NotImplementedException();
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        throw new NotImplementedException();
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        throw new NotImplementedException();
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        throw new NotImplementedException();
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        throw new NotImplementedException();
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        throw new NotImplementedException();
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        throw new NotImplementedException();
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        throw new NotImplementedException();
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        throw new NotImplementedException();
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
        throw new NotImplementedException();
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
        throw new NotImplementedException();
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        
    }
}