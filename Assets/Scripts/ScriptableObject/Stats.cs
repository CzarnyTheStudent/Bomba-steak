using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "Stats/PlayerStats")]
public class Stats : ScriptableObject
{
    public string totalTime;
    public int dragCount = 0;

    public void UpdateTotalTime(string additionalTime)
    {
        totalTime = additionalTime;
    }

    public string GetTotalTime()
    {
        return totalTime;
    }
   
    public void UpdateDragCount(int newDragCount)
    {
        dragCount = newDragCount;
    }

    public int GetDragCount()
    {
        return dragCount;
    }
}
