using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour 
{
    public static ScoreTracker Instance;

    public Text ScoreText;

    private int Score;

    public int GetScore
    {
        get
        {
            return Score;
        }
        set
        {
            Score = value;
        }
    }

    void Awake()
    {
        Instance = this;

        ScoreText.text = "Score: " + Score;
    }

    public void AddScore(int ScoreToAdd)
    {
        Score += ScoreToAdd;

        ScoreText.text =  "Score: " + Score;
    }
}
