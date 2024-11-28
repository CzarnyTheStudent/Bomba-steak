using GameTools;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;

    public void TogglePause()
    {
        if (GameManager.Instance.CurrentGameMode == GameManager.GameMode.SinglePlayer)
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
        else
        {
            ShowPausePanel();
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        ShowPausePanel();
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        HidePausePanel();
    }

    private void ShowPausePanel() => pausePanel.SetActive(true);

    private void HidePausePanel() => pausePanel.SetActive(false);
}