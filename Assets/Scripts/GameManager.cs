
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [Header("Time Setup")]
    [SerializeField] private float setTimeOnLevel;
    [SerializeField] private Stats saveOnLevel;
    private GameState gameState;

    private void Awake()
    {
        SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        gameState = GameState.Instance;
    }

    void Start()
    {
        EventManager.OnTimerUpdate(setTimeOnLevel);
        EventManager.OnTimerStart();
    }

    private void OnEnable()
    {
        EventManager.GameOver += SaveData;
    }

    private void OnDisable()
    {
        EventManager.GameOver -= SaveData;
    }

    private void SaveData()
    {
        
    }
}
