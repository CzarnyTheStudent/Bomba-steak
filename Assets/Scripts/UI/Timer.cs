using System;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    #region Variables

    public TMP_Text timerText;
    enum TimerType {Countdown, Stopwatch}
    [SerializeField] private TimerType timerType;
    private float timeToDisplay = 0.0f;
    [SerializeField] private bool _isRunning;

    #endregion

    private void OnEnable()
    {
        EventManager.TimerStart += EventManagerOnTimerStart;
        EventManager.TimerStop += EventManagerOnTimerStop;
        EventManager.TimerUpdate += EventManagerOnTimerUpdate;
    }

    private void OnDisable()
    {
        EventManager.TimerStart -= EventManagerOnTimerStart;
        EventManager.TimerStop -= EventManagerOnTimerStop;
        EventManager.TimerUpdate -= EventManagerOnTimerUpdate;
    }

    private void EventManagerOnTimerStart() => _isRunning = true;

    private void EventManagerOnTimerStop()
    {
        _isRunning = false;
    }

    private void EventManagerOnTimerUpdate(float value) => timeToDisplay += value;
    
    private void Update()
    {
        if (!_isRunning) return;
        if (timerType == TimerType.Countdown && timeToDisplay < 0.0f)
        {
            GameMediator.Instance.NotifyGameEnd(false);
            return;
        }
        
        timeToDisplay += timerType == TimerType.Countdown ? -Time.deltaTime : Time.deltaTime;

        TimeSpan timeSpan = TimeSpan.FromSeconds(timeToDisplay);
        timerText.text = timeSpan.ToString(@"mm\:ss\:ff");
    }
}