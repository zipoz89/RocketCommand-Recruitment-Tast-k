using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class holds references for main game managers
/// </summary>
public class GlobalReference : MonoBehaviour
{
    [field: SerializeField]
    public RoundManager RoundManager { get; private set; }
    [field: SerializeField]
    public RoundBreakManager RoundBreakManager { get; private set; }
    [field: SerializeField]
    public GameOverManager GameOverManager { get; private set; }
}
