using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;
    public static event Action<GameState> OnStateChange;
    public static event Action OnEnemyKilled;
    private int playerScore = 0;
    private int scoreToBeat = 0;
    private string playerName = "";
    private List<Scorer> scoreList = new List<Scorer>();

    [SerializeField] GameObject highscoreNameMenu = null;
    [SerializeField] TMP_InputField nameField = null;
    [SerializeField] GameObject warningText = null;
    [SerializeField] GameObject gameOverMenu = null;
    private void Awake()
    {
        Instance= this;
    }

    private void Start()
    {
        
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;
        Debug.Log ("New Game State: " + state.ToString());

        switch (state)
        {
            case GameState.MainMenu:
                break;
            case GameState.Play:
                break;
            case GameState.End:
                if (CheckHighScore())
                {
                    highscoreNameMenu.SetActive(true);
                }
                else
                {
                    gameOverMenu.SetActive(true);
                }
                break;
        }
        OnStateChange(state);
    }

    public void TriggerEnemyKill()
    {
        OnEnemyKilled();
    }

    public void SetScore(int score)
    {
        playerScore = score;
    }

    public Scorer GetScorer()
    {
        return new Scorer(playerName, playerScore);
    }
    // When the player loses, this gets called after the game state is set to End
    // It Checks if the player achieved a new high score then triggers an event if they did
    private bool CheckHighScore()
    {

        scoreList.Clear();
        if (PlayerPrefs.HasKey("FirstScore"))
        {
            scoreList.Add(new Scorer(PlayerPrefs.GetString("FirstScoreName"), PlayerPrefs.GetInt("FirstScore")));
        }
        if (PlayerPrefs.HasKey("SecondScore"))
        {
            scoreList.Add(new Scorer(PlayerPrefs.GetString("SecondScoreName"), PlayerPrefs.GetInt("SecondScore")));
        }
        if (PlayerPrefs.HasKey("ThirdScore"))
        {
            scoreList.Add(new Scorer(PlayerPrefs.GetString("ThirdScoreName"), PlayerPrefs.GetInt("ThirdScore")));
        }
        if (scoreList.Count == 3)
        {
            scoreToBeat = scoreList[2].score;
            Debug.Log("Update Score To Beat: " + scoreToBeat);
        }
        else
            scoreToBeat = 0;

        if (playerScore > scoreToBeat)
        {
            return true;
        }
        return false;
    }

    public void HighScoreContinueButton()
    {
        if (!string.IsNullOrWhiteSpace(nameField.text))
        {
            playerName = nameField.text;
            Debug.Log("Player Name: " + playerName);
            highscoreNameMenu.SetActive(false);
            gameOverMenu.SetActive(true);

            scoreList.Add(new Scorer(playerName, playerScore));
            // Sorting by score
            scoreList = scoreList.OrderByDescending(x => x.score).ToList();
            // Keeping the list of scores a total of 3
            if (scoreList.Count > 3)
            {
                scoreList.RemoveAt(3);
            }

            FillScoresPrefs();
        }
        else
        {
            warningText.SetActive(true);
        }
    }
    public void FillScoresPrefs()
    {
        // Setting the score text fields
        if (scoreList.Count > 0)
        {
            PlayerPrefs.SetInt("FirstScore", scoreList[0].score);
            PlayerPrefs.SetString("FirstScoreName", scoreList[0].name);
        }
        if (scoreList.Count > 1)
        {
            PlayerPrefs.SetInt("SecondScore", scoreList[1].score);
            PlayerPrefs.SetString("SecondScoreName", scoreList[1].name);
        }
        if (scoreList.Count > 2)
        {
            PlayerPrefs.SetInt("ThirdScore", scoreList[2].score);
            PlayerPrefs.SetString("ThirdScoreName", scoreList[2].name);
        }
    }
}

public enum GameState
{ 
    MainMenu, Play, End
}
public struct Scorer
{
    public string name;
    public int score;

    public Scorer(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}
