using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;


    public TextMeshProUGUI scoreText,highScoreText;

    private int currentScore = 0;
    private int HighScore;

    

    private void Awake()
    {
        instance = this;
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = HighScore.ToString();
    }

    public void AddScore(int increasingScore)
    {
        currentScore += increasingScore;
        scoreText.text = currentScore.ToString();
    }
    public void ResetScore()
    {
        currentScore = 0;
        scoreText.text = currentScore.ToString();
    }
    public void CheckHighScore()
    {
        if(HighScore < currentScore)
        {
            HighScore = currentScore;
            PlayerPrefs.SetInt("HighScore", HighScore);
            highScoreText.text = HighScore.ToString();
        }
    }
}
