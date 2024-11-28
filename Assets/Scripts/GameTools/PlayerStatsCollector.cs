using UnityEngine;

[System.Serializable]
public class PlayerStatsCollector
{
    private static string _totalTime;
    private static int _dragCount = 0;
    private static bool _playerWon = false;

    public void UpdateTotalTime(string time)
    {
        _totalTime = time;
        Debug.Log($"Total Time Updated: {_totalTime}");
    }

    public void UpdateDragCount(int count)
    {
        _dragCount = count;
        Debug.Log($"Drag Count Updated: {_dragCount}");
    }

    public void UpdateLevelComplete(bool won)
    {
        _playerWon = won;
        Debug.Log($"Player Won Updated: {_playerWon}");
    }

    // Getters
    public static string GetTotalTime() => _totalTime;
    public static int GetDragCount() => _dragCount;
    public static bool HasPlayerWon() => _playerWon;
}