using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class DisconnectManager : NetworkBehaviour
{
    [SerializeField] private GameStateController stateController;
    public void LeaveGame()
    {
        stateController.GameHasEnded();
    }
}