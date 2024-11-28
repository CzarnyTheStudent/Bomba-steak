using System.Collections;
using Static;
using UnityEngine;

namespace GameTools
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject starGameCanvas;
        [SerializeField] private GameSetup currentGameSetup;
        private bool pinPulled;
        public bool GameReady { get; private set; }

        private void Start()
        {
            StartCoroutine(GameInitialization());
        }

        private IEnumerator GameInitialization()
        {
            pinPulled = false;
            starGameCanvas.SetActive(true);
            
            while (!pinPulled) yield return null;
            
            starGameCanvas.SetActive(false);
            InitializeGame();
        }

        private void InitializeGame()
        {
            if (currentGameSetup != null)
            {
                EventManager.OnTimerUpdate(currentGameSetup.setTimeOnLevel);
                EventManager.OnTimerStart();
            }

            SetUpCoordinator.RegisterGameSetup(currentGameSetup);
            GameReady = true;
            Debug.Log("Game Initialized");
            EventManager.OnGameStart();
        }

        public void PinPulled() => pinPulled = true;

        private void OnEnable()
        {
            EventManager.NextLvl += () => SceneLoader.Instance.LoadNextLevel();
            EventManager.Restart += () => SceneLoader.Instance.RestartLevel();
        }

        private void OnDisable()
        {
            EventManager.NextLvl -= () => SceneLoader.Instance.LoadNextLevel();
            EventManager.Restart -= () => SceneLoader.Instance.RestartLevel();
        }
    }
}