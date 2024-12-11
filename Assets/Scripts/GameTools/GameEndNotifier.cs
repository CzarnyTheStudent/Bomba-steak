using Static;
using UnityEngine;

public class GameEndNotifier
{
    private static GameEndNotifier _instance;
    public static GameEndNotifier Instance => _instance ??= new GameEndNotifier();
    
    public void NotifyGameEnd()
    {
        EventManager.OnGameOver();
        EventManager.OnTimerStop();
    }
}
