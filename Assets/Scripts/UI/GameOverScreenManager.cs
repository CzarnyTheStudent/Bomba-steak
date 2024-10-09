using System.Collections.Generic;
using GameTools;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        GameDataStatsReceiver.Instance.ReceiveTimeData(timer.timerText.text);
        timerGameOverTime.text = timer.timerText.text;
        dragCount.text = GameDataStatsReceiver.Instance.GetDragEndCount().ToString();
        if (SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.sceneCount - 1) {nextLevelButton.SetActive(false);}
    } 

    public void NextLevel()
    {
        if (GameManager.Instance.GameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
