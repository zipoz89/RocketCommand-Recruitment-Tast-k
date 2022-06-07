using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class is state machine processor
/// </summary>
public class StateMachine
{
    private State currentState;
    public State CurrentState { get => currentState; }

    public void Initialize(State state)
    {
        currentState = state;
        currentState.InitializeState();
    }

    public void UpdateStates()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }
    }

    public void ChangeState(State state)
    {
        currentState.DeinitializeState();
        currentState = state;
        currentState.InitializeState();
    }
}