using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class controlls all the system that creates main game loop
/// </summary>
public class RoundManager : CustomController
{
    [SerializeField] private RoundDifficultyDatabase database;
    [SerializeField] private EnemyDirector enemyDirector;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private ScoreManager score;

    private int currentRound = 0;
    public int CurrentRound { get => currentRound; }

    public override void Initialize()
    {
        if (currentRound < database.roundsDifficulty.Length)
        {
            currentRound++;
        }
        else 
        {
            currentRound = 1;
        }

        enemyDirector.Initialize();
        playerManager.Initialize();

        var data = database.roundsDifficulty[currentRound-1];

        enemyDirector.SetRoundParams(data.nukesCount, data.speedMultiplier, data.maxNukesAtOnce);
        enemyDirector.OnAllNukesSend += RoundCompleted;
        playerManager.OnAllCitiesDied += RoundCompleted;
    }

    private void RoundCompleted()
    {
        if (playerManager.IsAlive)
        {
            GameManager.Instance.ChangeState(new RoundBreakState());
        }
        else 
        {
            GameManager.Instance.ChangeState(new GameOverState());
        }
    }

    public override void CustomUpdate()
    {
        enemyDirector.CustomUpdate();
        playerManager.CustomUpdate();
    }

    public override void Deinitialzie()
    {
        enemyDirector.OnAllNukesSend -= RoundCompleted;
        playerManager.OnAllCitiesDied -= RoundCompleted;
        enemyDirector.Deinitialzie();
        playerManager.Deinitialzie();
    }


    public void RestartGame() 
    {
        currentRound = 0;
        score.ResetScore();
    }

    public int GetScore() 
    {
        return score.GetScore;
    }
}
