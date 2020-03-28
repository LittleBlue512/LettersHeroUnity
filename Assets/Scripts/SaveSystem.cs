using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public GameObject player;

    private string savePath;
    private string saveFileName;

    void Start()
    {
        // Get save path.
        savePath = ProjectConst.savePath;
        saveFileName = ProjectConst.saveFileName;
    }

    public void saveGame(int currBuildIndex)
    {
        // Save game.
        SaveObject saveObject = new SaveObject();

        // Set values.
        saveObject.currLvlBuildIndex = currBuildIndex;

        // Save file.
        string jsonString = JsonUtility.ToJson(saveObject);
        File.WriteAllText(savePath + saveFileName, jsonString);
    }

    public void loadGame()
    {
        // Load game.
        Debug.Log("Load game");
    }
}
