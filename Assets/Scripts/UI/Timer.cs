using System;
using GameTools;
using Static;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public TMP_Text timerText;
    enum TimerType {Countdown, Stopwatch}
    [SerializeField] private TimerType timerType;
    private float timeToDisplay = 0.0f;
    private bool _isRunning;

    private void Awake() => instance = this;

    private void OnEnable()
    {
        EventManager.TimerStart += StartTimer;
        EventManager.TimerStop += StopTimer;
        EventManager.TimerUpdate += UpdateDisplayTime;
    }

    private void OnDisable()
    {
        EventManager.TimerStart -= StartTimer;
        EventManager.TimerStop -= StopTimer;
        EventManager.TimerUpdate -= UpdateDisplayTime;
    }

    private void StartTimer() => _isRunning = true;

    private void StopTimer() => _isRunning = false;

    private void UpdateDisplayTime(float value) => timeToDisplay += value;
    
    public void SyncTime(float serverTime)
    {
        timeToDisplay = serverTime;
        UpdateDisplayTime(serverTime);
    }
    public string GetCurrentTime() => timerText.text;
    
    
    private void Update()
    {
        if (!_isRunning) return;
        if (timerType == TimerType.Countdown && timeToDisplay < 0.0f)
        {
            GameEndNotifier.Instance.NotifyGameEnd();
            return;
        }
        
        timeToDisplay += timerType == TimerType.Countdown ? -Time.deltaTime : Time.deltaTime;

        TimeSpan timeSpan = TimeSpan.FromSeconds(timeToDisplay);
        timerText.text = timeSpan.ToString(@"mm\:ss\:ff");
    }
}