using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    StateMachine stateMachine = new StateMachine();

    private static GameManager instance = null;

    [field: SerializeField]
    public GlobalReference GlobalReference { get; private set; }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (GameManager)FindObjectOfType(typeof(GameManager));
            }
            return instance;
        }
    }

    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        stateMachine.Initialize(new RoundState());
    }

    private void Update()
    {
        stateMachine.UpdateStates();
    }


    public void ChangeState(State state)
    {
        stateMachine.ChangeState(state);
    }
}
