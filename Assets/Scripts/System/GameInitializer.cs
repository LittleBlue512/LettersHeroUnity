using System.IO;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private string savePath;
    private string configPath;
    private string saveFileName = ProjectConst.saveFileName;
    private string wordFileName = ProjectConst.wordFileName;

    void Awake()
    {
        initializeProjectConst();
        initializePath();
        initializeFile();
    }

    private void initializeProjectConst()
    {
        ProjectConst.savePath = Application.persistentDataPath + ProjectConst.saveFolderName;
        ProjectConst.configPath = Application.persistentDataPath + ProjectConst.configFolderName;

        Debug.Log(ProjectConst.savePath);
        Debug.Log(ProjectConst.configPath);
    }

    private void initializePath()
    {
        savePath = ProjectConst.savePath;
        configPath = ProjectConst.configPath;

        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);

        if (!Directory.Exists(configPath))
            Directory.CreateDirectory(configPath);
    }

    private void initializeFile()
    {
        // Check if words file exists.
        if (File.Exists(configPath + wordFileName))
        {
            // File exists.
            Debug.Log("File exists.");
            // Validate "words.txt" file.
            Debug.Log("Validating the file.");
            if (validateFile())
            {
                Debug.Log("File is valid.");
            }
            else
            {
                Debug.Log("File is not valid.");
                Debug.Log("Updating the file.");

                // Overwrite the old file.
                createWordsFile();
            }
        }
        else
        {
            // File does not exist.
            Debug.Log("File does not exists.");
            // Create a new one.
            Debug.Log("Creating the file.");
            createWordsFile();
        }
    }

    public void createWordsFile()
    {
        // Create new "words.txt" file.
        WordsObject wordsObject = new WordsObject();
        wordsObject.words = ProjectConst.defaultWords;
        string jsonString = JsonUtility.ToJson(wordsObject);
        File.WriteAllText(configPath + wordFileName, jsonString);
    }

    private bool validateFile()
    {
        // Get amount of words.
        int wordsCnt = ProjectConst.maxLevel;

        // Get array of words from the file.
        string jsonString = File.ReadAllText(configPath + wordFileName);
        WordsObject wordsObject = JsonUtility.FromJson<WordsObject>(jsonString);

        // Get length of the array.
        int arrLength = wordsObject.words.Length;

        // Validate the file.
        if (wordsCnt == arrLength)
            return true;
        return false;
    }
}
