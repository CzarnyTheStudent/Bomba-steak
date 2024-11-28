using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using GameTools;
using Static;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameOverScreenManager : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private List<GameObject> stars;
        [SerializeField] private List<GameObject> starDone;
        [Header("Buttons")]
        [SerializeField] private GameObject nextLevelButton;
        [SerializeField] private GameObject resetButton;
        [SerializeField] private GameObject levelSelectionButton;
        [Header("Timer")]
        [SerializeField] private TMP_Text timerGameOverTime;
        [SerializeField] private TMP_Text dragCount;
        [Header("Results")]
        [SerializeField] private TMP_Text playerTimeText;
        [SerializeField] private TMP_Text playerDragText;
        
        private string timeForStar;
        private int dragForStar;
   
        void Start()
        {
            gameOverScreen.SetActive(false);
        }

        private void OnEnable()
        {
            EventManager.TimeForStar += EventManagerOnTimeForStar;
            EventManager.DragForStar += EventManagerOnDragForStar;
            EventManager.GameOver += OnGameOver;
        }

        private void OnDisable()
        {
            EventManager.TimeForStar -= EventManagerOnTimeForStar;
            EventManager.DragForStar -= EventManagerOnDragForStar;
            EventManager.GameOver -= OnGameOver;
        }

        private void EventManagerOnTimeForStar(string data) => timeForStar += data;

        private void EventManagerOnDragForStar(int value) => dragForStar += value;

        private void OnGameOver()
        {
            gameOverScreen.SetActive(true);
            if (GameManager.Instance.CurrentGameMode == GameManager.GameMode.SinglePlayer)
            {
                HandleSinglePlayerData();
            }
            else
            {
                HandleMultiplayerData();
            }
            
        }
        
        
        private void HandleSinglePlayerData()
        {
            var playerData = GameDataStatsReceiver.Instance.GetPlayerData(new PlayerRef()); 
            if (playerData != null)
            {
                playerTimeText.text = playerData.GameTime;
                playerDragText.text = playerData.DragEndCount.ToString();
                dragCount.text = playerData.DragEndCount.ToString();
                GameSetup gameSetup = SetUpCoordinator.GetGameSetup();
                StartCoroutine(GetStats(gameSetup));
            }
            else
            {
                Debug.LogError("Player data not found for SinglePlayer.");
            }
        }

        private void HandleMultiplayerData()
        {
            var localPlayer = NetworkRunner.GetPlayerObject(NetworkRunner.LocalPlayer);
            var playerData = GameDataStatsReceiver.Instance.GetPlayerData(localPlayer);

            if (playerData != null)
            {
                playerTimeText.text = $"Time: {playerData.GameTime}";
                playerDragText.text = $"Drags: {playerData.DragEndCount}";
                dragCount.text = playerData.DragEndCount.ToString();
            }
            else
            {
                Debug.LogError($"Player data not found for player {localPlayer}.");
            }
        }

        IEnumerator GetStats(GameSetup gameSetup)
        {
            var localPlayer = NetworkRunner.Instance.LocalPlayer;
            var playerData = GameDataStatsReceiver.Instance.GetPlayerData(localPlayer);

            if (playerData != null && playerData.Completed)
            {
                yield return new WaitForSeconds(1f);
                starDone[0].SetActive(true);
            }

            TimeSpan.TryParseExact(timeForStar, @"mm\:ss\:ff", null, out TimeSpan additionalTimeSpan);
            TimeSpan.TryParseExact(gameSetup.setChallenges.timeForStar, @"mm\:ss\:ff", null, out TimeSpan totalTimeSpan);

            yield return new WaitForSeconds(1f);
            if (additionalTimeSpan >= totalTimeSpan)
            {
                starDone[1].SetActive(true);
            }

            yield return new WaitForSeconds(1f);
            if (dragForStar <= gameSetup.setChallenges.dragsForStar)
            {
                starDone[2].SetActive(true);
            }

            yield return null;
        }
        

        public void NextLevel() => EventManager.OnNextLevel();

        public void Restart() => EventManager.OnRestart();

        public void BackToMainMenu() => EventManager.TriggerBackToMenu();
    }
}