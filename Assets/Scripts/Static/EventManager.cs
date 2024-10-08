using UnityEngine.Events;

public static class EventManager
{
    //Timer events
    public static event UnityAction TimerStart;
    public static event UnityAction TimerStop;
    public static event UnityAction<float> TimerUpdate;
    public static event UnityAction<string> GetTime;

    //GameOver events
    public static event UnityAction GameOver;
    public static event UnityAction<bool> PlayerWin;
    
    public static void OnTimerStart() => TimerStart?.Invoke();
    public static void OnTimerStop() => TimerStop?.Invoke();
    public static void OnTimerUpdate(float value) => TimerUpdate?.Invoke(value);
    public static void OnGetTime(string value) => GetTime?.Invoke(value);


    public static void OnGameOver() => GameOver?.Invoke();
    public static void OnGameOverPlayerWin(bool data) => PlayerWin?.Invoke(data);
}