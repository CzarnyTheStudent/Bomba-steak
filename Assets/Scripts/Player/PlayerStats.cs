using GameTools;
using UnityEngine;
using Fusion;
using Static;

namespace Player
{
    public class PlayerStats : NetworkBehaviour
    {
        private int _dragEndCount;
        private bool _isWin;
        
        private void Start() => GameDataStatsReceiver.Instance.RegisterPlayer(Object.InputAuthority);

        private void OnDestroy() => GameDataStatsReceiver.Instance.UnregisterPlayer(Object.InputAuthority);


        private void OnEnable()
        {
            EventManager.GameOver += GetLocalPlayerData;
        }

        private void OnDisable()
        {
            EventManager.GameOver -= GetLocalPlayerData;
        }
        public void IncrementDragCount() => _dragEndCount++;
        public void SetToWin() => _isWin = true;

        private void GetLocalPlayerData()
        {
            GameDataStatsReceiver.Instance.UpdateDataFromPlayer(Object.InputAuthority, _dragEndCount, Timer.instance.GetCurrentTime(), _isWin);
        }
    }
}