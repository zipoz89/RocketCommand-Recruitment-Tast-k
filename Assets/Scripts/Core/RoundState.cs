using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State that is responsible for main loop of the game
/// </summary>
public class RoundState : State
{
    public override void InitializeState()
    {
        GameManager.Instance.GlobalReference.RoundManager.Initialize();
    }

    public override void UpdateState()
    {
        GameManager.Instance.GlobalReference.RoundManager.CustomUpdate();
    }

    public override void DeinitializeState()
    {
        GameManager.Instance.GlobalReference.RoundManager.Deinitialzie();
    }
}
