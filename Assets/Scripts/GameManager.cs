using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [Header("Time Setup")]
    [SerializeField]private float setTimeOnLevel;

    private void Awake()
    {
        SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
    }

    void Start()
    {
        EventManager.OnTimerUpdate(setTimeOnLevel);
        EventManager.OnTimerStart();
    }
}
