using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //Singleton
    public static ScoreManager Instance { get; private set; }
    public Text scoreText;
    private int score = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void SetScoreText(Text newText)
    {
        scoreText = newText;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
