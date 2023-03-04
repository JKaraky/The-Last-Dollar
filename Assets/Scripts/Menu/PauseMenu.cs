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

    public static bool gameIsPaused;

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
    }
    public void NewMediumGameDialog()
    {
        SceneManager.LoadScene(_mediumLevel);
    }
    public void NewHardGameDialog()
    {
        SceneManager.LoadScene(_hardLevel);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
