using System;
using UnityEngine;

public class Stats : MonoBehaviour
{
    private float totalTime = 0f; // Czas całkowity

    private void OnEnable()
    {
        EventManager.TimerStop += EventManagerOnTimerStop;
        EventManager.TimeCheck += OnTimeCheck;
    }

    private void OnDisable()
    {
        EventManager.TimerStop -= EventManagerOnTimerStop;
        EventManager.TimeCheck -= OnTimeCheck;
    }
    
    private void OnTimeCheck(float time)
    {
        totalTime = time; // Ustawiamy całkowity czas na podstawie eventu
    }

    private void EventManagerOnTimerStop()
    {
        int dragCount = FindObjectOfType<PlayerMovement>().GetRegisterDragEnd();
        Debug.Log("Pozostały czas gry: " + totalTime);
        Debug.Log("Łączna liczba przeciągnięć: " + dragCount);
    }

}