namespace GameTools
{
    public class GameDataStatsReceiver
    {
        private static GameDataStatsReceiver _instance;
        public static GameDataStatsReceiver Instance => _instance ??= new GameDataStatsReceiver();

        private int _timePlayed;
        private int _dragEndCount;
        private string _gameTime;
        private bool _completed;

        public void ReceiveTimePlayed(int count)
        {
            _timePlayed = count;
        }
    
        public void ReceiveDragEndCount(int count)
        {
            _dragEndCount = count;
        }

        public void ReceiveTimeData(string time)
        {
            _gameTime = time;
        }
    
        public void ReceivePlayerWon(bool won)
        {
            _completed = won;
        }

        public int GetTimePlayed() => _timePlayed;
        public int GetDragEndCount() => _dragEndCount;
        public string GetGameTime() => _gameTime;
        public bool GetPlayerWon() => _completed;
    }
}