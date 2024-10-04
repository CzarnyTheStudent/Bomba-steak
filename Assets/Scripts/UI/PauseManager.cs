using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel; // Panel, który pokaże się w czasie pauzy
    private bool isPaused = false; // Flaga, aby śledzić stan gry

    void Start()
    {
        // Upewnij się, że panel pauzy jest wyłączony na początku gry
        pausePanel.SetActive(false);
    }

    // Funkcja wywoływana przy kliknięciu przycisku pauzy
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

    // Pauzuje grę
    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Zatrzymuje czas gry
        pausePanel.SetActive(true); // Pokazuje panel pauzy
    }

    // Wznawia grę
    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Wznawia czas gry
        pausePanel.SetActive(false); // Ukrywa panel pauzy
    }
}