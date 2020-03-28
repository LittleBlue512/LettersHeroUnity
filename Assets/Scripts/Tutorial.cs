using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public LevelManager levelManager;

    private AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void continueHandler()
    {
        // Play sound.
        audioManager.Play("ButtonClick");

        levelManager.loadNextLevel();
    }
}
