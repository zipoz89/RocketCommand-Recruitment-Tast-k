using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this is state that happens betwen two rounds
/// </summary>
public class RoundBreakState : State
{
    public override void InitializeState()
    {
        GameManager.Instance.GlobalReference.RoundBreakManager.Initialize();
    }

    public override void UpdateState()
    {
        GameManager.Instance.GlobalReference.RoundBreakManager.CustomUpdate();
    }

    public override void DeinitializeState()
    {
        GameManager.Instance.GlobalReference.RoundBreakManager.Deinitialzie();
    }
}
