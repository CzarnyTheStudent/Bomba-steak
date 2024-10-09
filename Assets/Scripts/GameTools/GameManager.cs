using System;
using GameTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool GameOver { get; private set; }
    
    public GameObject player;
    private GameState _gameState;
    private GameSetup _gameSetup;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        _gameState = GameState.Instance;
        _gameSetup = GetComponent<GameSetup>();
    }

    void Start()
    {
        GameInitialize();
        _gameSetup.saveOnLevel.Load();
    }

    private void GameInitialize()
    {
        EventManager.OnTimerUpdate(_gameSetup.setTimeOnLevel);
        EventManager.OnTimerStart();
        _gameState.SetGameMode(GameState.Mode.Singleplayer);
    }

    private void OnEnable()
    {
        EventManager.GameOver += EM_OnGameOverSaveData;
    }

    private void OnDisable()
    {
        EventManager.GameOver -= EM_OnGameOverSaveData;
    }

    private void EM_OnGameOverSaveData()
    {
        _gameSetup.saveOnLevel.UpdateTotalTime(GameDataStatsReceiver.Instance.GetGameTime());
        _gameSetup.saveOnLevel.UpdateDragCount(GameDataStatsReceiver.Instance.GetDragEndCount());
        _gameSetup.saveOnLevel.UpdateLevelComplete(GameDataStatsReceiver.Instance.GetPlayerWon());
        _gameSetup.saveOnLevel.Save();
    }
}