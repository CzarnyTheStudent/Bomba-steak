using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false; 
    void Start()
    {
        pausePanel.SetActive(false);
    }

   
    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
    
    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; 
        pausePanel.SetActive(true); 
    }
    
    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; 
        pausePanel.SetActive(false); 
    }
}