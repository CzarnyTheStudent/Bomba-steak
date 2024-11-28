using Static;
using UnityEngine;

namespace GameTools
{
    public class GameEndNotifier
    {
        private static GameEndNotifier _instance;
        public static GameEndNotifier Instance => _instance ??= new GameEndNotifier();

        public void NotifyGameEnd()
        {
            if (GameManager.Instance.CurrentGameMode == GameManager.GameMode.SinglePlayer)
            {
                EventManager.OnGameOver();
                EventManager.OnTimerStop();
            }
            else
            {
               
            }
        }
    }
}