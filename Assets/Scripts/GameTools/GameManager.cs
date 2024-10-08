using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool GameOver { get; private set; }
    public bool PlayerWon { get; private set; }

    public GameObject player;
    private GameState _gameState;
    private GameSetup _gameSetup;
    private string _timeValue;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }
        Instance = this;
        GameMediator.Instance.RegisterGameManager(Instance);
        DontDestroyOnLoad(gameObject);

        SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        _gameState = GameState.Instance;
        _gameSetup = GetComponent<GameSetup>();
    }

    void Start()
    {
        EventManager.OnTimerUpdate(_gameSetup.setTimeOnLevel);
        EventManager.OnTimerStart();
        _gameState.SetGameMode(GameState.Mode.Singleplayer);
    }

    private void OnEnable()
    {
        EventManager.GameOver += SaveData;
    }

    private void OnDisable()
    {
        EventManager.GameOver -= SaveData;
    }

    public void GetTime(string time)
    {
        _timeValue = time;
    }

    public void StopGame(bool playerWin)
    {
        EventManager.OnTimerStop();
        EventManager.OnGameOver();
        PlayerWon = playerWin;
    }

    private void SaveData()
    {
        _gameSetup.saveOnLevel.UpdateTotalTime(_timeValue);
        _gameSetup.saveOnLevel.UpdateDragCount(player.GetComponent<PlayerMovement>().GetRegisterDragEnd());
    }
}