using UnityEngine;
using UnityEngine.Events;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    public enum Mode {Singleplayer, Multiplayer}
    private Mode _currentMode = Mode.Singleplayer;
    public event UnityAction<Mode> OnModeChanged;
    public Mode CurrentMode => _currentMode;

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
        if (_currentMode != mode)
        {
            _currentMode = mode;
            OnModeChanged?.Invoke(_currentMode);  
        }
    }
}
