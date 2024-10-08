using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private GameObject resetButton;
    [SerializeField] private GameObject levelSelectionButton;
    [SerializeField] private TMP_Text timerGameOverTime;
    [SerializeField] private List<GameObject> stars;
    [SerializeField] private Timer timer;
    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.GameOver += EventManagerOnGameOver;
        EventManager.PlayerWin += EventManagerOnGameOverPlayerWin;
        EventManager.GetTime += EventManagerOnGetTime;
    }

    private void OnDisable()
    {
        EventManager.GameOver -= EventManagerOnGameOver;
        EventManager.PlayerWin -= EventManagerOnGameOverPlayerWin;
        EventManager.GetTime += EventManagerOnGetTime;
    }
    private void EventManagerOnGetTime(string value) => timer.timerText.text += value;

    private void EventManagerOnGameOver()
    {
        gameOver = true;
        gameOverScreen.SetActive(true);
        settingsButton.SetActive(false);
        //EventManager.OnGetTime();
        timerGameOverTime.text = timer.timerText.text;
    } 

    private void EventManagerOnGameOverPlayerWin(bool data)
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex > SceneManager.sceneCount)
        {
            nextLevelButton.SetActive(false);
        }
    }

    public void NextLevel()
    {
        if (gameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


}
