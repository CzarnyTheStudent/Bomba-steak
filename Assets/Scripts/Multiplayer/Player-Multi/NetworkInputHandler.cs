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
        Debug.Log($"Gracz {player} dołączył do gry.");
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
        Debug.Log($"Gracz {player} opuścił grę.");
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
        Debug.Log($"Serwer zamknięty z powodu: {shutdownReason}");
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("Połączono z serwerem.");
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        Debug.Log($"Rozłączono z serwerem: {reason}");
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        Debug.Log("Otrzymano żądanie połączenia.");
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        Debug.LogError($"Nie udało się połączyć z serwerem: {reason}");
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        Debug.Log("Otrzymano wiadomość symulacji.");
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        Debug.Log($"Zaktualizowano listę sesji. Liczba sesji: {sessionList.Count}");
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        Debug.Log("Otrzymano odpowiedź uwierzytelniania.");
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        Debug.Log("Migracja hosta.");
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
        Debug.Log($"Otrzymano dane od gracza {player}.");
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
        Debug.Log($"Postęp odbierania danych od gracza {player}: {progress * 100}%");
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        Debug.Log("Załadowano scenę.");
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        Debug.Log("Rozpoczęto ładowanie sceny.");
    }
}