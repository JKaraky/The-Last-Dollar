using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    [Header("Menu Audio")]
    [SerializeField] private AudioClip clickAudio;
    [SerializeField] private AudioClip hoverAudio;
    [SerializeField] private AudioSource audioSource;

    [Header("Volume Settings")]
    [SerializeField] private TMP_Text masterVolumeTextValue = null;
    [SerializeField] private Slider masterVolumeSlider = null;
    [SerializeField] private float defaultMasterVolume = 0.5f;
    private float _masterVolume;

    [SerializeField] private TMP_Text musicVolumeTextValue = null;
    [SerializeField] private Slider musicVolumeSlider = null;
    [SerializeField] private float defaultMusicVolume = 0.5f;
    private float _musicVolume;

    [SerializeField] private TMP_Text sfxVolumeTextValue = null;
    [SerializeField] private Slider sfxVolumeSlider = null;
    [SerializeField] private float defaultSfxVolume = 0.5f;
    private float _SfxVolume;

    [SerializeField] private AudioMixer audioMixer;

    [Header("Levels To Load")]
    public string _normalLevel;
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

    public void NewNormalGameDialog()
    {
        SceneManager.LoadScene(_normalLevel);
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

    public void ClickSound()
    {
        audioSource.PlayOneShot(clickAudio);
    }

    public void HoverSound()
    {
        audioSource.PlayOneShot(hoverAudio);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        masterVolumeTextValue.text = volume.ToString("0.0");
        _masterVolume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        musicVolumeTextValue.text = volume.ToString("0.0");
        _musicVolume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        audioMixer.SetFloat("SfxVolume", Mathf.Log10(volume) * 20);
        sfxVolumeTextValue.text = volume.ToString("0.0");
        _SfxVolume = volume;
    }
}
