using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int _dragEndCount = 0;
    private string _currentTime;
    private bool _playerWon = false;

    public void IncrementDragEndCount()
    {
        _dragEndCount++;
        Debug.Log($"Drag End Count: {_dragEndCount}");
    }

    public void UpdateCurrentTime()
    {
        _currentTime = Timer.instance.GetCurrentTime();
        Debug.Log($"Current Time: {_currentTime}");
    }

    public void SetPlayerWon(bool won)
    {
        _playerWon = won;
        Debug.Log($"Player Won: {_playerWon}");
    }

    // Getters
    public int GetDragEndCount() => _dragEndCount;
    public string GetCurrentTime() => _currentTime;
    public bool HasPlayerWon() => _playerWon;
}