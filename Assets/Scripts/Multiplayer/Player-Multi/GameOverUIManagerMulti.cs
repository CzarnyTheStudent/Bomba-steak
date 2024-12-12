using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Fusion;

public class GameOverUIManagerMulti : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TabStatsData _playerOverviewEntryPrefab;
    private Dictionary<PlayerRef, TabStatsData> _playerListEntries = new Dictionary<PlayerRef, TabStatsData>();
    
    private Dictionary<PlayerRef, string> _playerNickNames = new Dictionary<PlayerRef, string>();
    private Dictionary<PlayerRef, string> _playerTimes = new Dictionary<PlayerRef, string>();
    private Dictionary<PlayerRef, int> _playerDragCounts = new Dictionary<PlayerRef, int>();
    private Dictionary<PlayerRef, bool> _playerWon = new Dictionary<PlayerRef, bool>();
    
    public void AddEntry(PlayerRef playerRef, PlayerDataNetworked playerDataNetworked)
    {
        if (_playerListEntries.ContainsKey(playerRef)) return;
        if (playerDataNetworked == null) return;

        var entry = Instantiate(_playerOverviewEntryPrefab, this.transform);
        entry.transform.localScale = Vector3.one;

        string nickName = String.Empty;
        string time = "";
        int drags = 0;
        bool won = false;

        _playerNickNames.Add(playerRef, nickName);
        _playerTimes.Add(playerRef, time);
        _playerDragCounts.Add(playerRef, drags);
        _playerWon.Add(playerRef, won);

        _playerListEntries.Add(playerRef, entry);

        UpdateEntry(playerRef, entry);
    }
    
    public void UpdateWon(PlayerRef player, bool won)
    {
        if (_playerListEntries.TryGetValue(player, out var entry) == false) return;

        _playerWon[player] = won;
        UpdateEntry(player, entry);
    }
    
    public void UpdateDragCount(PlayerRef player, int lives)
    {
        if (_playerListEntries.TryGetValue(player, out var entry) == false) return;

        _playerDragCounts[player] = lives;
        UpdateEntry(player, entry);
    }

    public void UpdateTime(PlayerRef player, string score)
    {
        if (_playerListEntries.TryGetValue(player, out var entry) == false) return;

        _playerTimes[player] = score;
        UpdateEntry(player, entry);
    }

    public void UpdateNickName(PlayerRef player, string nickName)
    {
        if (_playerListEntries.TryGetValue(player, out var entry) == false) return;

        _playerNickNames[player] = nickName;
        UpdateEntry(player, entry);
    }

    private void UpdateEntry(PlayerRef player, TabStatsData entry)
    {
        var nickName = _playerNickNames[player];
        var time = _playerTimes[player];
        var drags = _playerDragCounts[player];
        var win = _playerWon[player];

        entry.playerName.text = nickName;
        if (win)
        {
            entry.playerTimes.text = time;
        }
        else
        {
            entry.playerTimes.text = "Loser. No time show for u";
        }
        entry.playerDrags.text = drags.ToString();
        entry.playerResults.text = win ? "Won" : "Lost";
    }
    
    public void RemoveEntry(PlayerRef playerRef)
    {
        if (_playerListEntries.TryGetValue(playerRef, out var entry) == false) return;

        if (entry != null)
        {
            Destroy(entry.gameObject);
        }

        _playerNickNames.Remove(playerRef);
        _playerTimes.Remove(playerRef);
        _playerDragCounts.Remove(playerRef);

        _playerListEntries.Remove(playerRef);
    }
}
