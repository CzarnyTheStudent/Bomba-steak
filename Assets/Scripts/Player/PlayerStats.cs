using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        private int _dragEndCount = 0;

        public void IncrementDragEndCount()
        {
            _dragEndCount++;
            GameDataStatsReceiver.Instance.ReceiveDragEndCount(_dragEndCount);
        }
    }
}