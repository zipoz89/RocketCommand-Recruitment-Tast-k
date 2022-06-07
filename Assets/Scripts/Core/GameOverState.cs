using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : State
{
    public override void InitializeState()
    {
        GameManager.Instance.GlobalReference.GameOverManager.Initialize();
    }

    public override void UpdateState()
    {
        GameManager.Instance.GlobalReference.GameOverManager.CustomUpdate();
    }

    public override void DeinitializeState()
    {
        GameManager.Instance.GlobalReference.GameOverManager.Deinitialzie();
    }
}
