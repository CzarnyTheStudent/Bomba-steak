using GameTools;
using Multiplayer.Player_Multi;
using UnityEngine;
using UnityEngine.Events;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameModeManager.CurrentGameMode == GameModeManager.GameMode.SinglePlayer)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<PlayerStats>().SetPlayerWon(true);
                GameEndNotifier.Instance.NotifyGameEnd();
            }
        }
        else
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<PlayerDataNetworked>().SetPlayerWon();
                other.GetComponent<PlayerDataNetworked>().SetTime();
                GameEndNotifier.Instance.NotifyGameEnd();
            }
        }
    }
}
