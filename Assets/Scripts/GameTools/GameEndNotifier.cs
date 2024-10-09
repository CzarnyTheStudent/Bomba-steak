namespace GameTools
{
    public class GameEndNotifier
    {
        private static GameEndNotifier _instance;
        public static GameEndNotifier Instance => _instance ??= new GameEndNotifier();

        public void NotifyGameEnd(bool playerWin)
        {
            GameDataStatsReceiver.Instance.ReceivePlayerWon(playerWin);
            EventManager.OnGameOver();
            EventManager.OnTimerStop();
        }
    }
}