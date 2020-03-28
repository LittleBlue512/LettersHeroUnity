using UnityEngine;

public class Credit : MonoBehaviour
{
    public LevelManager levelManager;

    private AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void mainMenuHandler()
    {
        // Play sound.
        audioManager.Play("ButtonClick");

        // Load main menu.
        levelManager.loadLevel("Main Menu");
    }
}
