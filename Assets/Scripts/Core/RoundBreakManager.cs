using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this manager controlls stuff that happen between rounds
/// </summary>
public class RoundBreakManager : CustomController
{
    [SerializeField] private ScoreManager score;
    [SerializeField] private PlayerManager player;

    [SerializeField] private Text roundCompletedText;

    [SerializeField] private float waitTime = 3f;
    private float elapsedWaitTime = 0;

    public override void Initialize()
    {
        elapsedWaitTime = 0;

        int currentRound = GameManager.Instance.GlobalReference.RoundManager.CurrentRound;

        int thisRoundScore = player.AliveCities * (50 * currentRound);

        score.AddScore(thisRoundScore);

        roundCompletedText.gameObject.SetActive(true);
        roundCompletedText.text = "Round " + currentRound + " completed with score " + thisRoundScore;
    }

    public override void CustomUpdate()
    {
        if (elapsedWaitTime <= waitTime)
        {
            elapsedWaitTime += Time.deltaTime;
        }
        else
        {
            GameManager.Instance.ChangeState(new RoundState());
        }
    }

    public override void Deinitialzie()
    {
        roundCompletedText.gameObject.SetActive(false);
    }
}
