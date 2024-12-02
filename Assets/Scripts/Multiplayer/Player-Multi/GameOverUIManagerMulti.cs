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
            if (playerId >= 1 && playerId < playerTimes.Count)
            {
                if (!won)
                {
                    playerTimes[playerId - 1].text = "Not completed";
                }
                else
                {
                    playerTimes[playerId - 1].text = time;
                }
                playerDrags[playerId - 1].text = drags.ToString();
                playerResults[playerId - 1].text = won ? "Won" : "Lost";
            }
        }
    }
}