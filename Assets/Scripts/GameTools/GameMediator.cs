public class GameMediator
{
    private static GameMediator _instance;
    public static GameMediator Instance => _instance ??= new GameMediator();

    private GameManager _gameManager;
    private GameOverScreenManager _gameOverScreenManager;
    public void RegisterGameManager(GameManager gameManager) => _gameManager = gameManager;

    public void RegisterGameOverManager(GameOverScreenManager gameOverScreenManager) => _gameOverScreenManager = gameOverScreenManager;

    public void NotifyGameEnd(bool playerWin)
    {
        EventManager.OnGameOver();
        _gameManager.StopGame(playerWin);
    }

    public void SendTimeData()
    {
        _gameManager.GetTime(_gameOverScreenManager.timer.timerText.text);
    }
}