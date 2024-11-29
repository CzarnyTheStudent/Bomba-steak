using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameOverUIManagerMulti : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private List<TMP_Text> playerTimes;
        [SerializeField] private List<TMP_Text> playerDrags;
        [SerializeField] private List<TMP_Text> playerResults;

        private void Start()
        {
            gameOverScreen.SetActive(false);
        }

        public void DisplayGameOverScreen()
        {
            gameOverScreen.SetActive(true);
        }

        public void SetToShowPlayerStats(int playerId, string time, int drags, bool won)
        {
            if (playerId >= 0 && playerId < playerTimes.Count)
            {
                if (!won)
                {
                    playerTimes[playerId].text = "Not completed";
                }
                else
                {
                    playerTimes[playerId].text = time;
                }
                playerDrags[playerId].text = drags.ToString();
                playerResults[playerId].text = won ? "Won" : "Lost";
            }
        }
    }
}