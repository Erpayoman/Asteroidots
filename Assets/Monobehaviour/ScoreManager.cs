using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int currentScore = 0;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        instance = this;
    }

    public void AddScore(int increasingScore)
    {
        currentScore += increasingScore;
        scoreText.text = currentScore.ToString();
    }
}
