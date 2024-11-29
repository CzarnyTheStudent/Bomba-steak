using System.Collections;
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

        public void LoadNextLevel() => StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
        public void RestartLevel() => StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));

        private IEnumerator LoadScene(int sceneIndex)
        {
            if (loadingCanvas != null)
            {
                loadingCanvas.SetActive(true);
            }
            
            AsyncOperation asyncUILoad = SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
            while (!asyncUILoad.isDone) yield return null;
            Debug.Log("UI Scene Loaded");
            
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
            while (!asyncLoad.isDone) yield return null;
            Debug.Log($"Scene {sceneIndex} loaded.");
            
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