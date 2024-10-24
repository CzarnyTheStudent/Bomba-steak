using System;
using System.Collections;
using System.Collections.Generic;
using GameTools;
using Static;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        public Timer timer;
       
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
            EventManager.GameOver += EM_OnGameOver;
        }

        private void OnDisable()
        {
            EventManager.TimeForStar -= EventManagerOnTimeForStar;
            EventManager.DragForStar -= EventManagerOnDragForStar;
            EventManager.GameOver -= EM_OnGameOver;
        }

        private void EventManagerOnTimeForStar(string data) => timeForStar += data;

        private void EventManagerOnDragForStar(int value) => dragForStar += value;

        private void EM_OnGameOver()
        {
            gameOverScreen.SetActive(true);
            timerGameOverTime.text = timer.timerText.text;
            dragCount.text = GameDataStatsReceiver.Instance.GetDragEndCount().ToString();
            GameSetup gameSetup = Coordinator.Instance.GetGameSetup();
            StartCoroutine(GetStats(gameSetup));
        } 

        IEnumerator GetStats(GameSetup gameSetup)
        {
            bool win = GameDataStatsReceiver.Instance.GetPlayerWon();
            yield return new WaitForSeconds(1f);
            if (win)
            {
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

        public void NextLevel()
        {
            EventManager.OnNextLevel();
        }

        public void Restart()
        {
            EventManager.OnRestart();
        }
    }
}