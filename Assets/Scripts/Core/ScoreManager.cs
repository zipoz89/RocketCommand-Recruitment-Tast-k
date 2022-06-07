using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this class counts and display player score
/// </summary>
public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    private int score = 0;

    public int GetScore { get => score; }

    public void AddScore(int score) 
    {
        this.score += score;
        UpdateText();
    }

    public void ResetScore() 
    {
        score = 0;
        UpdateText();
    }

    private void UpdateText() 
    {
        scoreText.text = "Score: " + score;
    }
}
