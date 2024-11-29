using System.Collections;
using Static;
using UnityEngine;

namespace GameTools
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject starGameCanvas;
        [SerializeField] private GameSetup currentGameSetup;
        private NetGameManager netManager;
        private bool pinPulled;
        public bool GameReady { get; private set; }

        private void Start()
        {
            netManager = GetComponent<NetGameManager>();
            StartCoroutine(GameInitialization());
        }

        private IEnumerator GameInitialization()
        {
            if (netManager != null)
            {
                GameModeManager.CurrentGameMode = GameModeManager.GameMode.Multiplayer;
            }
            else
            {
                GameModeManager.CurrentGameMode = GameModeManager.GameMode.SinglePlayer;
            }
            Debug.Log("Game Set to: " + GameModeManager.CurrentGameMode);
            pinPulled = false;
            starGameCanvas.SetActive(true);
            
            while (!pinPulled) yield return null;
            
            starGameCanvas.SetActive(false);
            InitializeGame();
        }

        private void InitializeGame()
        {
            if (currentGameSetup != null &&  GameModeManager.CurrentGameMode == GameModeManager.GameMode.SinglePlayer)
            {
                EventManager.OnTimerUpdate(currentGameSetup.setTimeOnLevel);
                EventManager.OnTimerStart();
            }
            else
            {
                Timer.instance.SetToStopwatch();
                EventManager.OnTimerStart();
            }

            SetUpCoordinator.RegisterGameSetup(currentGameSetup);
            GameReady = true;
            Debug.Log("Game Initialized");
            EventManager.OnGameStart();
        }

        public void PinPulled() => pinPulled = true;
    }
}