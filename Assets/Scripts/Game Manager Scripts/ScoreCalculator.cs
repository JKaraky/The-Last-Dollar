using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreCalculator : MonoBehaviour
{
    [SerializeField] GameObject scoreContainer = null;
    [SerializeField] TMP_Text scoreText= null;

    private int currentScore = 0;
    public int scoreMultiplier = 1;

    private void Awake()
    {
        GameManager.OnStateChange += GameManagerOnStateChange;
        GameManager.OnEnemyKilled += GameManagerOnEnemyKilled;
    }
    private void OnDestroy()
    {
        GameManager.OnStateChange -= GameManagerOnStateChange;
        GameManager.OnEnemyKilled -= GameManagerOnEnemyKilled;
    }
    void Start()
    {
        scoreContainer.SetActive(true);
        scoreText.text = currentScore.ToString();
    }

    public void GameManagerOnStateChange (GameState state)
    {
        switch (state)
        {
            case GameState.Play:
                break;
        }
    }
    public void GameManagerOnEnemyKilled()
    {
        AddScore();
        scoreText.text = currentScore.ToString();

        GameManager.Instance.SetScore(currentScore);
    }
    

    public void AddScore ()
    {
        // Adding points per each kill. Currently adds 1 point times the score multiplier which should change based on difficulty
        currentScore += 1*scoreMultiplier;

        Debug.Log(scoreText.text);
    }

    
}
