using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameTools
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance { get; private set; }

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
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
            while (!asyncLoad.isDone) yield return null;

            Debug.Log($"Scene {sceneIndex} loaded.");
        }
    }
}