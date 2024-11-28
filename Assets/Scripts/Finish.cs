using GameTools;
using Fusion;
using Player;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance.CurrentGameMode == GameManager.GameMode.SinglePlayer)
            {
                GameEndNotifier.Instance.NotifyGameEnd();
                other.GetComponent<PlayerStats>().SetToWin();
            }
            else
            {
                //GameManager.Instance.NotifyPlayerFinish(NetworkRunner.GetPlayer(other.gameObject));
            }
        }
    }
}