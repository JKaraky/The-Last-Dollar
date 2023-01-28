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

    // Universal Apply method, just specify the menu type in editor
    public void Apply()
    {
        if (SimplifyMenuName(currentMenu) == "Audio")
        {
            PlayerPrefs.SetFloat("volume", AudioListener.volume);

            shouldApply = false;
        }
        StartCoroutine(ConfirmationBox());
    }

    // Universal Back method, specify menu type in editor
    public void Back (string MenuType)
    {
        if (MenuType == "Audio")
        {
            if (shouldApply)
            {
                saveSettingsPrompt.SetActive(true);
                currentMenu = "Back Confirmation Dialog";

                menuStack.Push(FindByName(currentMenu));
                print("Top of Stack: " + menuStack.Peek());
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
            }

            // Going back once and turning the menu off
            menuStack.Pop().SetActive(false);
            print("Top of Stack: " + menuStack.Peek());

            // Turning the menu at the top of the Stack on and storing its name in currentMenu
            menuStack.Peek().SetActive(true);
            print("Top of Stack: " + menuStack.Peek());
            currentMenu = menuStack.Peek().name;

            // Popping once so we know what the previous menu is
            prevMenu = menuStack.Pop().name;
            
            if (menuStack.Count > 0)
                print("Previous of Stack: " + menuStack.Peek());

            // Adding the menu again because we popped it off earlier (only way to get previous element)
            menuStack.Push(FindByName(currentMenu));
            print("Top of Stack: " + menuStack.Peek());
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
    }

    public void SetCurMenu (string menu)
    {
        // We need to add the first menu option ONCE only to the start of the Stack
        if (runOnce)
        {
            menuStack.Push(menus[0]);
            print("Top of Stack: " + menuStack.Peek());
            runOnce = false;
        }
        currentMenu = menu;

        // Adding this menu to the Stack
        menuStack.Push(FindByName(currentMenu));
        print("Top of Stack: " + menuStack.Peek());

        print("Current Menu: " + currentMenu);
    }
    public void SetPrevMenu(string menu)
    {
        prevMenu = menu;
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
