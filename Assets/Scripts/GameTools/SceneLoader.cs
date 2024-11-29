using System.Collections;
using Fusion;
using Static;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameTools
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance { get; private set; }

        [SerializeField] private GameObject loadingCanvas;

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

        private void OnEnable()
        {
            EventManager.NextLvl += () => LoadNextLevel();
            EventManager.Restart += () => RestartLevel();
        }

        private void OnDisable()
        {
            EventManager.NextLvl -= () => LoadNextLevel();
            EventManager.Restart -= () => RestartLevel();
        }

        public void LoadSelectedLevel(int levelIndex) => StartCoroutine(LoadScene(levelIndex));

        public void LoadMultiplayerHost(string sceneName) => StartCoroutine(LoadMultiplayer(sceneName, GameMode.Host));
        public void LoadMultiplayerJoin(string sceneName) => StartCoroutine(LoadMultiplayer(sceneName, GameMode.Client));

        private void LoadNextLevel() => StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
        private void RestartLevel() => StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));

        private IEnumerator LoadScene(int sceneIndex)
        {
            if (loadingCanvas != null)
            {
                loadingCanvas.SetActive(true);
            }

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
            while (!asyncLoad.isDone) yield return null;
            Debug.Log($"Scene {sceneIndex} loaded.");

            AsyncOperation asyncUILoad = SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
            while (!asyncUILoad.isDone) yield return null;
            Debug.Log("UI Scene Loaded");

            if (loadingCanvas != null)
            {
                loadingCanvas.SetActive(false);
            }
        }

        private IEnumerator LoadMultiplayer(string sceneName, GameMode mode)
        {
            if (loadingCanvas != null)
            {
                loadingCanvas.SetActive(true);
            }

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncLoad.isDone) yield return null;
            Debug.Log($"Multiplayer Scene '{sceneName}' loaded.");
            
            AsyncOperation asyncUILoad = SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
            while (!asyncUILoad.isDone) yield return null;
            Debug.Log("UI Scene Loaded");

            var netGameManager = FindObjectOfType<NetGameManager>();
            if (netGameManager != null)
            {
                netGameManager.InitializeMultiplayer(mode);
            }

            if (loadingCanvas != null)
            {
                loadingCanvas.SetActive(false);
            }
        }

        [ContextMenu("ForceToLoadUI")]
        public void LoadGameUi()
        {
            SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
            Debug.Log("UI Scene Loaded");
        }
    }
}
