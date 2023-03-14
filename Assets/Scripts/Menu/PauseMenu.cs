using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [Header("Levels To Load")]
    public string _easyLevel;
    public string _mediumLevel;
    public string _hardLevel;
    private string levelToLoad;
    public string _mainMenu;

    public static bool gameIsPaused;

    private void Awake()
    {
        GameManager.OnStateChange += GameManagerOnStateChange;
    }
    private void OnDestroy()
    {
        GameManager.OnStateChange += GameManagerOnStateChange;
    }
    public void GameManagerOnStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                Time.timeScale = 1;
                break;
            case GameState.Play:
                Time.timeScale = 1;
                break;
            case GameState.End:
                Time.timeScale = 0f;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;

        pauseMenu.SetActive(gameIsPaused);

        if (gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void NewEasyGameDialog()
    {
        SceneManager.LoadScene(_easyLevel);
        // Changing game state to Play
        GameManager.Instance.UpdateGameState(GameState.Play);
    }
    public void NewMediumGameDialog()
    {
        SceneManager.LoadScene(_mediumLevel);
        // Changing game state to Play
        GameManager.Instance.UpdateGameState(GameState.Play);
    }
    public void NewHardGameDialog()
    {
        SceneManager.LoadScene(_hardLevel);
        // Changing game state to Play
        GameManager.Instance.UpdateGameState(GameState.Play);
    }

    public void ShowInstructions()
    {

    }
    public void ExitButton()
    {
        SceneManager.LoadScene(_mainMenu);
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
    }
}
