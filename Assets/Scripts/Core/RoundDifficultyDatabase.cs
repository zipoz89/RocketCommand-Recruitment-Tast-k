using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this scriptable object holds variables that dictates game difficulty on specific rounds
/// </summary>
[CreateAssetMenu(fileName = "RoundDifficultyDatabase",menuName = "Database/Difficulty")]
public class RoundDifficultyDatabase : ScriptableObject
{
    public RoundDifficulty[] roundsDifficulty;
}

[System.Serializable]
public class RoundDifficulty 
{
    public int nukesCount;
    public float speedMultiplier;
    public int maxNukesAtOnce;
}
