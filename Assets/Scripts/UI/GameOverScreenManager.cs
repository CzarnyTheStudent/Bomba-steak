using System.Collections.Generic;
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
    public Timer timer;
   
    void Start()
    {
        gameOverScreen.SetActive(false);
        GameMediator.Instance.RegisterGameOverManager(this);
    }

    private void OnEnable()
    {
        EventManager.GameOver += EventManagerOnGameOver;
    }

    private void OnDisable()
    {
        EventManager.GameOver -= EventManagerOnGameOver;
    }

    private void EventManagerOnGameOver()
    {
        gameOverScreen.SetActive(true);
        GameMediator.Instance.SendTimeData();
        timerGameOverTime.text = timer.timerText.text;
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
