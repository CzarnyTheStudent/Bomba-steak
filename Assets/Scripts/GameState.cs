using UnityEngine;
using UnityEngine.Events;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    public enum Mode {Singleplayer, Multiplayer}

    public bool gameOver;

    private Mode currentMode = Mode.Singleplayer;

    public event UnityAction<Mode> OnModeChanged;

    public Mode CurrentMode => currentMode;

    private void Awake()
    {
       
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

   
    public void SetGameMode(Mode mode)
    {
        if (currentMode != mode)
        {
            currentMode = mode;
            OnModeChanged?.Invoke(currentMode);  
        }
    }
    
    public void StopGame()
    {
        EventManager.OnTimerStop(); 
    }

   
    public void UpdateTimer(float time)
    {
        EventManager.OnTimerUpdate(time);
        EventManager.OnTimeCheck(time); 

        if (currentMode == Mode.Singleplayer)
        {
            // Add singleplayer-specific logic 
        }
        else if (currentMode == Mode.Multiplayer)
        {
            // Add multiplayer-specific logic
        }
    }
}
