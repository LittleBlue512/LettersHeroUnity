using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public LevelManager levelManager;
    public Button continueBtn;

    private AudioManager audioManager;
    private string savePath;
    private string saveFileName;

    void Start()
    {
        // Get save path.
        savePath = ProjectConst.savePath;
        saveFileName = ProjectConst.saveFileName;

        // Disable continue button if there is no save.
        if (!File.Exists(savePath + saveFileName))
            continueBtn.interactable = false;

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void continueHandler()
    {
        // Play sound.
        audioManager.Play("ButtonClick");

        // Load file.
        SaveObject saveObject = new SaveObject();
        string jsonString = File.ReadAllText(savePath + saveFileName);
        saveObject = JsonUtility.FromJson<SaveObject>(jsonString);

        // Get values.
        int currLvlBuildIndex = saveObject.currLvlBuildIndex;

        // Load level.
        levelManager.loadLevel(currLvlBuildIndex);
    }

    public void newGameHandler()
    {
        // Play sound.
        audioManager.Play("ButtonClick");

        // Create new save.
        SaveObject saveObject = new SaveObject();

        // Set values. Starting at Level 1.
        int buildIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Level 1.unity");
        saveObject.currLvlBuildIndex = buildIndex;

        // Save file.
        string jsonString = JsonUtility.ToJson(saveObject);
        File.WriteAllText(savePath + saveFileName, jsonString);

        // Load next level.
        levelManager.loadNextLevel();
    }

    public void editHandler()
    {
        // Play sound.
        audioManager.Play("ButtonClick");

        // Go to edit scene.
        int buildIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Edit.unity");
        levelManager.loadLevel(buildIndex);
    }

    public void quitHandler()
    {
        // Play sound.
        audioManager.Play("ButtonClick");

        Application.Quit();
    }
}
