using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{

    // connected to game controller

    public static int _score;
    public Text scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SetScoreText();
    }

    // Method to set score
    private void SetScoreText()
    {
        scoreText.text = "Score: " + _score.ToString();

    }

    // Method to increase score
    public void incrementScore(int num)
    {
        _score += num;
        SetScoreText();

    }

    // Method to get current score
    public int GetCurrentScore() { 
        return _score; // Returns the current score
    }
}
