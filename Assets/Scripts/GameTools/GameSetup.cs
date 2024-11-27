using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSetup", menuName = "GameTools/GameSetup")]
public class GameSetup : ScriptableObject
{
    [Header("Time Limit Setup")]
    public float setTimeOnLevel;

    [Header("Challenges")]
    public Challenges setChallenges;
}