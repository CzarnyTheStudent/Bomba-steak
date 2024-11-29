using System;
using System.Collections;
using System.Collections.Generic;
using GameTools;
using Static;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameOverManager : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private GameOverUIManager gameOverUiSingle;
        [SerializeField] private GameOverUIManagerMulti gameOverUiMulti;

        [Header("Timer")]
        [SerializeField] private TMP_Text timerGameOverTime;
        [SerializeField] private TMP_Text dragCount;

        private string timeForStar;
        private int dragForStar;

        private void OnEnable()
        {
            EventManager.TimeForStar += OnTimeForStar;
            EventManager.DragForStar += OnDragForStar;
            EventManager.GameOver += OnGameOver;
        }

        private void OnDisable()
        {
            EventManager.TimeForStar -= OnTimeForStar;
            EventManager.DragForStar -= OnDragForStar;
            EventManager.GameOver -= OnGameOver;
        }

        private void OnTimeForStar(string data) => timeForStar += data;

        private void OnDragForStar(int value) => dragForStar += value;

        private void OnGameOver()
        {
            if (GameModeManager.CurrentGameMode == GameModeManager.GameMode.SinglePlayer)
            {
                timerGameOverTime.text = Timer.instance.GetCurrentTime();
                dragCount.text = PlayerStatsCollector.GetDragCount().ToString();
                gameOverUiSingle.DisplayGameOverScreen(timerGameOverTime.text, dragCount.text);
                StartCoroutine(GetStatsSingle());
            }
            else
            {
                gameOverUiMulti.DisplayGameOverScreen();
                StartCoroutine(GetStatsMulti());
            }
        }

        private IEnumerator GetStatsSingle()
        {
            GameSetup gameSetup = SetUpCoordinator.GetGameSetup();
            bool win = PlayerStatsCollector.HasPlayerWon();

            yield return new WaitForSeconds(1f);
            if (win)
            {
                gameOverUiSingle.SetStarActive(0);
            }

            TimeSpan.TryParseExact(timeForStar, @"mm\:ss\:ff", null, out TimeSpan additionalTimeSpan);
            TimeSpan.TryParseExact(gameSetup.setChallenges.timeForStar, @"mm\:ss\:ff", null, out TimeSpan totalTimeSpan);

            yield return new WaitForSeconds(1f);
            if (additionalTimeSpan >= totalTimeSpan)
            {
                gameOverUiSingle.SetStarActive(1);
            }

            yield return new WaitForSeconds(1f);
            if (dragForStar <= gameSetup.setChallenges.dragsForStar)
            {
                gameOverUiSingle.SetStarActive(2);
            }
        }

        private IEnumerator GetStatsMulti()
        {
            yield return new WaitForSeconds(1f);
            PlayerStatsCollectorMulti statsCollector = FindObjectOfType<PlayerStatsCollectorMulti>();

            foreach (var playerId in statsCollector.GetAllPlayerIds())
            {
                var time = statsCollector.GetCurrentTime(playerId);
                var drags = statsCollector.GetDragEndCount(playerId);
                var won = statsCollector.HasPlayerWon(playerId);

                gameOverUiMulti.SetToShowPlayerStats(playerId, time, drags, won);
            }
        }
    }
}
