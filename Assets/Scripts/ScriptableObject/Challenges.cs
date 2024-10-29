using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Challenges", menuName = "GameSetup/Challenges")]
public class Challenges : ScriptableObject
{
    public string timeForStar = "00:00:00";
    public int dragsForStar = 99;
}
