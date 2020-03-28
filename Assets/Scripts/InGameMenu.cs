using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject inGameMenu;
    public GameObject levelTitle;
    public SaveSystem saveSystem;
    public LevelManager levelManager;

    private AudioManager audioManager;
    private bool menuActive = false;

    void Start()
    {
        levelTitle.GetComponent<Text>().text = SceneManager.GetActiveScene().name;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void toggleInGameMenu()
    {
        // Play sound.
        audioManager.Play("ButtonClick");

        menuActive = !menuActive;
        inGameMenu.SetActive(menuActive);

        // Stop time when open in-game menu.
        if (menuActive)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void mainMenuBtnHandler()
    {
        // Play sound.
        audioManager.Play("ButtonClick");

        Time.timeScale = 1f;

        // Go to main menu.
        levelManager.loadLevel("Main Menu");
    }

    public void quitBtnHandler()
    {
        // Play sound.
        audioManager.Play("ButtonClick");

        Time.timeScale = 1f;

        // Quit.
        Application.Quit();
    }

    public void continueHandler()
    {
        // Play sound.
        audioManager.Play("ButtonClick");

        levelManager.loadNextLevel();
    }
}
