using System.Collections.Generic;
using GameTools;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameOverScreenManager : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private List<GameObject> stars;
        [Header("Buttons")]
        [SerializeField] private GameObject nextLevelButton;
        [SerializeField] private GameObject resetButton;
        [SerializeField] private GameObject levelSelectionButton;
        [Header("Timer")]
        [SerializeField] private TMP_Text timerGameOverTime;
        [SerializeField] private TMP_Text dragCount;
        public Timer timer;
   
        void Start()
        {
            gameOverScreen.SetActive(false);
        }

        private void OnEnable()
        {
            EventManager.GameOver += EM_OnGameOver;
        }

        private void OnDisable()
        {
            EventManager.GameOver -= EM_OnGameOver;
        }

        private void EM_OnGameOver()
        {
            gameOverScreen.SetActive(true);
            timerGameOverTime.text = timer.timerText.text;
            dragCount.text = GameDataStatsReceiver.Instance.GetDragEndCount().ToString();
        } 

        public void NextLevel()
        {
            GameManager.Instance.LoadNextLevel(); 
        }

        public void Restart()
        {
            GameManager.Instance.RestartLevel(); 
        }
    }
}