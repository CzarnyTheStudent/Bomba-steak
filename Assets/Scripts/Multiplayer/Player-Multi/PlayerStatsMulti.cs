using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsMulti : MonoBehaviour
{
    private Dictionary<int, PlayerStatsCollector> _playerStats = new Dictionary<int, PlayerStatsCollector>();

    public void IncrementDragEndCount(int playerId)
    {
        EnsurePlayerStats(playerId);
        _playerStats[playerId].UpdateDragCount(PlayerStatsCollector.GetDragCount() + 1);
        Debug.Log($"Player {playerId} Drag End Count: {PlayerStatsCollector.GetDragCount()}");
    }

    public void UpdateCurrentTime(int playerId)
    {
        EnsurePlayerStats(playerId);
        string currentTime = Timer.instance.GetCurrentTime();
        _playerStats[playerId].UpdateTotalTime(currentTime);
        Debug.Log($"Player {playerId} Current Time: {currentTime}");
    }

    public void SetPlayerWon(int playerId, bool won)
    {
        EnsurePlayerStats(playerId);
        _playerStats[playerId].UpdateLevelComplete(won);
        Debug.Log($"Player {playerId} Won: {won}");
    }

    private void EnsurePlayerStats(int playerId)
    {
        if (!_playerStats.ContainsKey(playerId))
        {
            _playerStats[playerId] = new PlayerStatsCollector();
        }
    }

    // Getters
    public int GetDragEndCount(int playerId) => _playerStats.ContainsKey(playerId) ? PlayerStatsCollector.GetDragCount() : 0;
    public string GetCurrentTime(int playerId) => _playerStats.ContainsKey(playerId) ? PlayerStatsCollector.GetTotalTime() : "00:00:00";
    public bool HasPlayerWon(int playerId) => _playerStats.ContainsKey(playerId) && PlayerStatsCollector.HasPlayerWon();
}