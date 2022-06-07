using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : CustomController
{
    [SerializeField] private GameObject canvasParent;
    [SerializeField] private Text fionalScoreText;


    public override void Initialize()
    {
        canvasParent.SetActive(true);

        fionalScoreText.text = "FINAL SCORE: " + GameManager.Instance.GlobalReference.RoundManager.GetScore();
    }

    public override void CustomUpdate()
    {
        
    }

    public override void Deinitialzie()
    {
        
    }

    public void NewGame() 
    {
        canvasParent.SetActive(false);
        GameManager.Instance.GlobalReference.RoundManager.RestartGame();
        GameManager.Instance.ChangeState(new RoundState());
    }
}
