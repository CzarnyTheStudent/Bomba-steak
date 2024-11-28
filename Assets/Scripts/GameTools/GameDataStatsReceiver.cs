using System.Collections.Generic;
using Fusion;

namespace GameTools
{
    public class GameDataStatsReceiver
    {
        private static GameDataStatsReceiver _instance;
        public static GameDataStatsReceiver Instance => _instance ??= new GameDataStatsReceiver();
        
        private readonly Dictionary<PlayerRef, PlayerData> _playerStats = new();
   
        public void RegisterPlayer(PlayerRef player)
        {
            if (!_playerStats.ContainsKey(player))
            {
                _playerStats[player] = new PlayerData();
            }
        }
        
        public void UnregisterPlayer(PlayerRef player)
        {
            if (_playerStats.ContainsKey(player))
            {
                _playerStats.Remove(player);
            }
        }

        public void UpdateDataFromPlayer(PlayerRef player, int count, string time, bool win)
        {
            if (_playerStats.ContainsKey(player))
            {
                _playerStats[player].DragEndCount = count;
                _playerStats[player].GameTime = time;
                _playerStats[player].Completed = win;
            }
        }
        

        public PlayerData GetPlayerData(PlayerRef player)
        {
            if (_playerStats.TryGetValue(player, out var data))
            {
                return data;
            }

            return null;
        }
    }

    public class PlayerData
    {
        public int DragEndCount { get; set; }
        public string GameTime { get; set; }
        public bool Completed { get; set; }
    }
}