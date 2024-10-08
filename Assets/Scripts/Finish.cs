using UnityEngine;
using UnityEngine.Events;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameMediator.Instance.NotifyGameEnd(true);
        }
    }
}
