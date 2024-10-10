using System;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public string totalTime;
    public int dragCount = 0;
    public bool playerWon = false;

    public void UpdateTotalTime(string additionalTime)
    {
        if (string.IsNullOrEmpty(totalTime))
        {
            totalTime = additionalTime;
            Debug.Log($"Total time initialized to: {totalTime}");
            return;
        }

        if (!TimeSpan.TryParseExact(additionalTime, @"mm\:ss\:ff", null, out TimeSpan additionalTimeSpan) ||
            !TimeSpan.TryParseExact(totalTime, @"mm\:ss\:ff", null, out TimeSpan totalTimeSpan)) return;

        if (additionalTimeSpan > totalTimeSpan)
        {
            totalTime = additionalTime;
            Debug.Log($"Total time updated to: {totalTime}");
        }
    }

    public void UpdateDragCount(int newDragCount)
    {
        if (dragCount != 0 && newDragCount > dragCount) return;
        dragCount = newDragCount;
    }

    public void UpdateLevelComplete(bool won)
    {
        if (!won) return;
        playerWon = won;
        Debug.Log($"Player won status updated to: {playerWon}");
    }
}