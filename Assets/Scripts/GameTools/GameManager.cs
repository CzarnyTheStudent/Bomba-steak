using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameTools
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        public PlayerStats playerStats;
        public float initializationDelay = 2f; 

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

        private IEnumerator LoadGameSetupAndInitialize()
        {
            playerStats = SaveManager.Load<PlayerStats>(SceneManager.GetActiveScene().name);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
            
            while (!asyncLoad.isDone) { yield return null; }

            Debug.Log("UI Scene Loaded");
            
            while (!Coordinator.Instance.IsGameSetupReady()) { yield return null; }

            Debug.Log("GameSetup is Ready");
            
            yield return new WaitForSeconds(initializationDelay);
            
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
        }

        public void LoadNextLevel() => StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

        public void RestartLevel() => StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));

        private IEnumerator LoadLevel(int levelIndex)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelIndex);
            while (!asyncLoad.isDone) { yield return null; }

            StartCoroutine(LoadGameSetupAndInitialize());
        }

        private void OnEnable()
        {
            EventManager.NextLvl += EM_OnLevelChange;
            EventManager.Restart += EM_OnLevelChange;
            EventManager.GameOver += EM_OnGameOverSaveData;
        }

        private void OnDisable()
        {
            EventManager.NextLvl -= EM_OnLevelChange;
            EventManager.Restart -= EM_OnLevelChange;
            EventManager.GameOver -= EM_OnGameOverSaveData;
        }

        private void EM_OnLevelChange() => StartCoroutine(LoadGameSetupAndInitialize());

        private void EM_OnGameOverSaveData()
        {
            playerStats.UpdateTotalTime(GameDataStatsReceiver.Instance.GetGameTime());
            playerStats.UpdateDragCount(GameDataStatsReceiver.Instance.GetDragEndCount());
            playerStats.UpdateLevelComplete(GameDataStatsReceiver.Instance.GetPlayerWon());
            SaveManager.Save(playerStats, SceneManager.GetActiveScene().name);
        }
    }
}
