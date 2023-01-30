using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private bool shouldApply = false;
    private string currentMenu = "Main Menu Container";
    private string prevMenu = "";

    [Header("Volume Settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 0.5f;

    [Header("Gameplay Setting")]
    [SerializeField] private TMP_Text mouseSenTextValue = null;
    [SerializeField] private Slider mouseSenSlider = null;
    [SerializeField] private int defaultSen = 5;
    public int mainMouseSen = 5;

    [Header("Toggle Settings")]
    [SerializeField] private Toggle invertYToggle= null;

    [Header("Graphics Settings")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private float defaultBrightness = 1;

    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    private int _qualityLevel;
    private bool _isFullScreen;
    private float _brightnessLevel;

    [Header("Resolution Dropdowns")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private List<Resolution> newRes = new List<Resolution>();
    private float currentRefreshRate;
    int currentResolutionIndex = 0;

    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt = null;
    [SerializeField] private GameObject saveSettingsPrompt = null;

    [Header("Levels To Load")]
    public string _easyLevel;
    public string _mediumLevel;
    public string _hardLevel;
    private string levelToLoad;


    [Header("Menus")]
    public List<GameObject> menus = new List<GameObject>();

    // Queuing menu options for better back tracking
    Stack<GameObject> menuStack = new();
    private bool runOnce = true;

    private void Start()
    {
        // First, we populate the resolutions array with the values found in Screen.resolutions
        resolutions = Screen.resolutions;
        currentRefreshRate = Screen.currentResolution.refreshRate;

        _isFullScreen = Screen.fullScreen;
        fullscreenToggle.isOn = _isFullScreen;

        // Then we clear everything we have in the resolutions dropdown menu
        resolutionDropdown.ClearOptions();

        // We make a list to store the written values of the resolutions and start going through the resolutions in the array
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == currentRefreshRate)
            {
                // Add the resolutions with the refresh rate we want into the new array
                newRes.Add(resolutions[i]);

                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);

                // This to get the resolution of the current screen the player is using
                if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                {
                    currentResolutionIndex = i;
                }
            }
        }
        // Making the resolutions array to only include the refresh rates we want
        resolutions = newRes.ToArray();

        // We add the written values to the dropdown and show the current resolution
        resolutionDropdown.AddOptions(options);

        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Loading Prefs
        ResetToSaved(true);
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

    public void SetVolume (float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");

        shouldApply = true;
    }

    public void SetMouseSensitivity(float sensitivity)
    {
        mainMouseSen = Mathf.RoundToInt(sensitivity);
        mouseSenTextValue.text = sensitivity.ToString("0");

        shouldApply= true;
    }

    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");

        shouldApply = true;
    }

    public void SetFullScreen(bool isFullScreen)
    {
        _isFullScreen= isFullScreen;

        shouldApply = true;
    }

    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;

        shouldApply = true;
    }

    public void SetResolution(int resolutionIndex)
    {
        currentResolutionIndex= resolutionIndex;

        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, _isFullScreen);

        shouldApply = true;
    }

    // Universal Apply method
    public void Apply()
    {
        if (SimplifyMenuName(currentMenu) == "Audio")
        {
            PlayerPrefs.SetFloat("Volume", AudioListener.volume);
        }

        if (SimplifyMenuName(currentMenu) == "Gameplay")
        {
            if (invertYToggle.isOn)
            {
                PlayerPrefs.SetInt("InvertY", 1);

                // Need to actually invert Y axis
            }
            else
                PlayerPrefs.SetInt("InvertY", 0);
            PlayerPrefs.SetInt("Sensitivity", mainMouseSen);
        }

        if (SimplifyMenuName(currentMenu) == "Graphics")
        {
            PlayerPrefs.SetFloat("Brightness", _brightnessLevel);
            // Need to actually change brightness

            PlayerPrefs.SetInt("Quality", _qualityLevel);
            QualitySettings.SetQualityLevel(_qualityLevel);

            PlayerPrefs.SetInt("FulLScreen", _isFullScreen ? 1 : 0);
            Screen.fullScreen = _isFullScreen;

            PlayerPrefs.SetInt("ResolutionIndex", currentResolutionIndex);
        }

        shouldApply = false;

        StartCoroutine(ConfirmationBox());
    }

    public void ResetToSaved(bool resetAll)
    {
        if (resetAll || SimplifyMenuName(currentMenu) == "Audio")
        {
            if (PlayerPrefs.HasKey("Volume"))
            {
                volumeSlider.value = PlayerPrefs.GetFloat("Volume");
                volumeTextValue.text = PlayerPrefs.GetFloat("Volume").ToString("0.0");

                SetVolume(volumeSlider.value);
            }

        }
        if (resetAll || SimplifyMenuName(currentMenu) == "Gameplay")
        {
            if (PlayerPrefs.HasKey("InvertY"))
            {
                invertYToggle.isOn = PlayerPrefs.GetInt("InvertY") == 1 ? true : false;
            }
            if (PlayerPrefs.HasKey("Sensitivity"))
            {
                mouseSenTextValue.text = PlayerPrefs.GetInt("Sensitivity").ToString("0");
                mouseSenSlider.value = PlayerPrefs.GetInt("Sensitivity");
                mainMouseSen = PlayerPrefs.GetInt("Sensitivity");

                SetMouseSensitivity(mainMouseSen);
            }
        }
        if (resetAll || SimplifyMenuName(currentMenu) == "Graphics")
        {
            if (PlayerPrefs.HasKey("Brightness"))
            {
                brightnessSlider.value = PlayerPrefs.GetFloat("Brightness");
                brightnessTextValue.text = PlayerPrefs.GetFloat("Brightness").ToString("0.0");

                SetBrightness(brightnessSlider.value);
            }
            if (PlayerPrefs.HasKey("Quality"))
            {
                qualityDropdown.value = PlayerPrefs.GetInt("Quality");

                SetQuality(qualityDropdown.value);
            }
            if (PlayerPrefs.HasKey("FulLScreen"))
            {
                fullscreenToggle.isOn = PlayerPrefs.GetInt("FulLScreen") == 1 ? true : false;

                SetFullScreen(fullscreenToggle.isOn);
            }
            if (PlayerPrefs.HasKey("ResolutionIndex"))
            {
                currentResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex");
                resolutionDropdown.value = currentResolutionIndex;

                SetResolution(currentResolutionIndex);
            }
        }
    }

    // Universal Back method, specify menu type in editor
    public void Back (string MenuType)
    {
        if (MenuType != "Go Back")
        {
            if (shouldApply)
            {
                saveSettingsPrompt.SetActive(true);
                currentMenu = "Back Confirmation Dialog";

                menuStack.Push(FindByName(currentMenu));
                print("Current Menu: " + menuStack.Peek());

                GetPrevMenu();
            }
            else
                Back("Go Back");
        }

        else if (MenuType == "Go Back")
        {
            // If we're on the back confirm popup, we need go back an extra time
            if (currentMenu == "Back Confirmation Dialog")
            {
                menuStack.Pop().SetActive(false);
                print("Top of Stack: " + menuStack.Peek());

                ResetToSaved(false);

                shouldApply = false;
            }

            // Going back once and turning the menu off
            menuStack.Pop().SetActive(false);

            // Turning the menu at the top of the Stack on and storing its name in currentMenu
            menuStack.Peek().SetActive(true);

            currentMenu = menuStack.Peek().name;

            // Popping once so we know what the previous menu is
            GetPrevMenu();
        }
    }

    // Universal Reset method, specify menu type in editor
    public void Reset()
    {
        if (SimplifyMenuName(currentMenu) == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");

            shouldApply = true;
        }
        if (SimplifyMenuName(currentMenu) == "Gameplay")
        {
            mouseSenTextValue.text = defaultSen.ToString("0");
            mouseSenSlider.value = defaultSen;
            mainMouseSen = defaultSen;
            invertYToggle.isOn= false;

            shouldApply= true;
        }
        if (SimplifyMenuName(currentMenu) == "Graphics")
        {
            // Reset brightness
            brightnessSlider.value = defaultBrightness;
            brightnessTextValue.text = defaultBrightness.ToString("0.0");

            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);

            fullscreenToggle.isOn= true;
            Screen.fullScreen = true;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;
        }
    }

    public void SetCurMenu (string menu)
    {
        // We need to add the first menu option ONCE only to the start of the Stack
        if (runOnce)
        {
            menuStack.Push(menus[0]);
            runOnce = false;
        }
        // Turning off previous menu
        menuStack.Peek().SetActive(false);

        currentMenu = menu;

        // Adding this menu to the Stack
        menuStack.Push(FindByName(currentMenu));
        // Turning on this menu
        menuStack.Peek().SetActive(true);

        print("Current Menu: " + currentMenu);

        GetPrevMenu();
    }
    public void GetPrevMenu()
    {
        if (menuStack.Count > 1)
        {
            menuStack.Pop();
            prevMenu = menuStack.Peek().name;
            menuStack.Push(FindByName(currentMenu));
        }
        else
        {
            prevMenu = "";
        }
        print("Previous Menu: " + prevMenu);
    }

    // Finds game object of specified menu name in the list of menus
    public GameObject FindByName(string name)
    {
        foreach (GameObject menu in menus)
        {
            if (menu.name == name)
                return menu;
        }
        return null;
    }

    // Translates Menu Names into Audio/Graphics/etc
    public string SimplifyMenuName(string name)
    {
        if (name == "Back Confirmation Dialog")
            name = prevMenu;
        switch (name)
        {
            case "Sound Dialog":
                return "Audio";
            case "Gameplay Dialog":
                return "Gameplay";
            case "Graphics Dialog":
                return "Graphics";
        }
        return name;
    }

    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);

    }
} 
