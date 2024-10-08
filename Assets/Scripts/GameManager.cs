using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [Header("Time Setup")]
    [SerializeField]private float setTimeOnLevel;
    [SerializeField]private GameState.Mode mode;
    private GameState gameState;

    private void Awake()
    {
        SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        gameState = GameState.Instance;
        gameState.SetGameMode(mode);
    }

    void Start()
    {
        EventManager.OnTimerUpdate(setTimeOnLevel);
        EventManager.OnTimerStart();
    }
}
