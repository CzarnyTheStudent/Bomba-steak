using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class DisconnectManager : NetworkBehaviour
{
    public void LeaveGame()
    {
        var runner = FindObjectOfType<NetworkRunner>();
        runner.Shutdown();
    }
}