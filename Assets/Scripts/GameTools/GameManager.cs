using System.Collections;
using Static;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameTools
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        public PlayerStatsCollector playerStatsCollector;
        [SerializeField] private GameObject StarGameCanvas;
        private bool pinPulled;
        public bool GameReady { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject); 
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
        }

        private void Start()
        {
            StartCoroutine(LoadGameSetupAndInitialize());
        }

        public void PinPulled()
        {
            pinPulled = true; 
        }

        private IEnumerator LoadLevel(int levelIndex)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelIndex);
            while (!asyncLoad.isDone) { yield return null; }

            StartCoroutine(LoadGameSetupAndInitialize());
        }

        private IEnumerator LoadGameSetupAndInitialize()
        {
            playerStatsCollector = SaveManager.Load<PlayerStatsCollector>(SceneManager.GetActiveScene().name);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
            pinPulled = false;
            StarGameCanvas.SetActive(true);

            while (!asyncLoad.isDone) { yield return null; }

            Debug.Log("UI Scene Loaded");
            
            while (!Coordinator.Instance.IsGameSetupReady()) { yield return null; }

            Debug.Log("GameSetup is Ready");
            GameReady = false;

            while (!pinPulled)
            {
                yield return null;
            }

            StarGameCanvas.SetActive(false);
            InitializeGame();
        }
        
        private void InitializeGame()
        {
            GameSetup gameSetup = Coordinator.Instance.GetGameSetup();

            if (gameSetup != null)
            {
                EventManager.OnTimerUpdate(gameSetup.setTimeOnLevel);
                EventManager.OnTimerStart();
            }

            Debug.Log("Game Initialized");
            EventManager.OnGameStart();
            GameReady = true;
        }

        private void LoadNextLevel() => StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

        private void RestartLevel() => StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));


        private void OnEnable()
        {
            EventManager.NextLvl += LoadNextLevel;
            EventManager.Restart += RestartLevel;
            EventManager.GameOver += GameOver;
        }

        private void OnDisable()
        {
            EventManager.NextLvl -= LoadNextLevel;
            EventManager.Restart -= RestartLevel;
            EventManager.GameOver -= GameOver;
        }

        private void GameOver()
        {
            EventManager.OnTimeForStar(GameDataStatsReceiver.Instance.GetGameTime());
            EventManager.OnDragForStar(GameDataStatsReceiver.Instance.GetDragEndCount());
            OnGameOverSaveData();
        }
        

        private void OnGameOverSaveData()
        {
            playerStatsCollector.UpdateTotalTime(GameDataStatsReceiver.Instance.GetGameTime());
            playerStatsCollector.UpdateDragCount(GameDataStatsReceiver.Instance.GetDragEndCount());
            playerStatsCollector.UpdateLevelComplete(GameDataStatsReceiver.Instance.GetPlayerWon());
            SaveManager.Save(playerStatsCollector, SceneManager.GetActiveScene().name);
        }
    }
}
