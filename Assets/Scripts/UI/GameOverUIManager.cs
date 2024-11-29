using System.Collections.Generic;
using Static;
using TMPro;
using UnityEngine;


namespace UI
{
    public class GameOverUIManager : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private List<GameObject> stars;
        [SerializeField] private TMP_Text timerGameOverTime;
        [SerializeField] private TMP_Text dragCount;
       
        
        private void Start()
        {
            gameOverScreen.SetActive(false);
        }

        public void DisplayGameOverScreen(string timeText, string dragCountText)
        {
            gameOverScreen.SetActive(true);
            timerGameOverTime.text = timeText;
            dragCount.text = dragCountText;
        }

        public void SetStarActive(int index)
        {
            if (index >= 0 && index < stars.Count)
            {
                stars[index].SetActive(true);
            }
        }

        public void HandleNextLevel() => EventManager.OnNextLevel();

        public void HandleRestart() => EventManager.OnRestart();

        public void HandleBackToMenu() => EventManager.TriggerBackToMenu();
    }
}