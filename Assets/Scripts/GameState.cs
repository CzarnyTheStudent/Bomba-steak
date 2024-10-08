using UnityEngine;
using UnityEngine.Events;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    public enum Mode
    {
        None,
        Singleplayer,
        Multiplayer
    }

    private Mode currentMode = Mode.None;

    // Events to notify when the game state changes
    public event UnityAction<Mode> OnModeChanged;

    public Mode CurrentMode => currentMode;

    private void Awake()
    {
        // Singleton pattern enforcement
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure the instance persists across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent multiple instances
        }
    }

    // Method to set the game mode
    public void SetGameMode(Mode mode)
    {
        if (currentMode != mode)
        {
            currentMode = mode;
            OnModeChanged?.Invoke(currentMode);  // Trigger mode change event
        }
    }

    // Method to start Singleplayer mode
    public void StartSingleplayer()
    {
        SetGameMode(Mode.Singleplayer);
        EventManager.OnTimerStart(); // Start the timer in singleplayer
    }

    // Method to start Multiplayer mode
    public void StartMultiplayer()
    {
        SetGameMode(Mode.Multiplayer);
        EventManager.OnTimerStart(); // Start the timer in multiplayer
    }
    
    public void StopGame()
    {
        if (currentMode != Mode.None)
        {
            EventManager.OnTimerStop(); // Stop the timer
            OnModeChanged?.Invoke(Mode.None);  // Reset game state
            currentMode = Mode.None;
        }
    }

    // Check the game mode and update the timer
    public void UpdateTimer(float time)
    {
        EventManager.OnTimerUpdate(time);
        EventManager.OnTimeCheck(time); // Trigger the time check event

        if (currentMode == Mode.Singleplayer)
        {
            // Add singleplayer-specific logic if needed
        }
        else if (currentMode == Mode.Multiplayer)
        {
            // Add multiplayer-specific logic if needed
        }
    }
}
