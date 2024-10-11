using UnityEngine.Events;

namespace Static
{
    public static class EventManager
    {
        //Timer events
        public static event UnityAction TimerStart;
        public static event UnityAction TimerStop;
        public static event UnityAction<float> TimerUpdate;
    
        //GameManager events
        public static event UnityAction GameStart;
        public static event UnityAction NextLvl;
        public static event UnityAction Restart;
        public static event UnityAction GameOver;
    
        public static void OnTimerStart() => TimerStart?.Invoke();
        public static void OnTimerStop() => TimerStop?.Invoke();
        public static void OnTimerUpdate(float value) => TimerUpdate?.Invoke(value);

        public static void OnGameStart() => GameStart?.Invoke();
        public static void OnNextLevel() => NextLvl?.Invoke();
        public static void OnRestart() => Restart?.Invoke();
        public static void OnGameOver() => GameOver?.Invoke();
    }
}