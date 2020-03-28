using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// TODO:
// Input word's length must not over 8 letters.

public class EditWord : MonoBehaviour
{
    public LevelManager levelManager;
    public InputField word1, word2, word3, word4, word5, word6, word7, word8, word9, word10;
    public GameObject dialogPanel;
    public Text dialogText;

    private AudioManager audioManager;
    private string configPath;
    private string savePath;
    private string wordFileName;
    private string saveFileName;

    void Start()
    {
        savePath = ProjectConst.savePath;
        configPath = ProjectConst.configPath;
        saveFileName = ProjectConst.saveFileName;
        wordFileName = ProjectConst.wordFileName;

        // Get components.
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void backHandler()
    {
        // Play sound.
        audioManager.Play("ButtonClick");

        // Go to main menu.
        int buildIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Main Menu.unity");
        levelManager.loadLevel(buildIndex);
    }

    public void defaultHandler()
    {
        // Play sound.
        audioManager.Play("ButtonClick");

        // Get default words.
        string[] words = ProjectConst.defaultWords;

        // set words.
        word1.text = words[0];
        word2.text = words[1];
        word3.text = words[2];
        word4.text = words[3];
        word5.text = words[4];
        word6.text = words[5];
        word7.text = words[6];
        word8.text = words[7];
        word9.text = words[8];
        word10.text = words[9];
    }

    public void saveHandler()
    {
        // Play sound.
        audioManager.Play("ButtonClick");

        // Get words.
        string[] words = { word1.text, word2.text, word3.text, word4.text, word5.text, word6.text, word7.text, word8.text, word9.text, word10.text };

        // Check words.
        if (!checkWords(words))
            return;

        // Delete old save.
        File.Delete(savePath + saveFileName);

        // Create object.
        WordsObject wordsObject = new WordsObject();
        wordsObject.words = words;
        string jsonString = JsonUtility.ToJson(wordsObject);

        // Save file.
        File.WriteAllText(configPath + wordFileName, jsonString);

        // Go to main menu.
        int buildIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Main Menu.unity");
        levelManager.loadLevel(buildIndex);
    }

    private bool checkWords(string[] words)
    {
        int maxWord = ProjectConst.maxLevel;
        int maxWordLength = ProjectConst.maxWordLength;

        // Check input fields.
        for (int i = 0; i < maxWord; i++)
        {
            if (words[i] == "")
            {
                // Invalid input: All fields must be filled..
                showDialogPanel("Can not save. All fields must be filled.");
                return false;
            }
        }

        // Check text's length.
        for (int i = 0; i < maxWord; i++)
        {
            if (words[i].Length > maxWordLength)
            {
                // Invalid input: Word's length must be lesser than the maxWordLength.
                showDialogPanel("Can not save. Word's length must be less than or equal to " + maxWordLength.ToString() + " characters.");
                return false;
            }
        }

        // Check if all character is alphabet.
        for (int i = 0; i < maxWord; i++)
        {
            string word = words[i];
            int length = word.Length;

            for (int j = 0; j < length; j++)
            {
                if (!isCharValid(word[j]))
                {
                    showDialogPanel("Can not save. Words must only consist of characters A-Z.");
                    return false;
                }
            }
        }

        return true;
    }

    private bool isCharValid(char letter)
    {
        // Check if letter is in range ['A', 'Z'] or ['a', 'z'].
        int letterCode = (int)letter;
        int code_A = (int)('A');
        int code_Z = (int)('Z');
        int code_a = (int)('a');
        int code_z = (int)('z');

        // If letter is out of scope, reture false.
        if ((letterCode >= code_A && letterCode <= code_Z) || (letterCode >= code_a && letterCode <= code_z))
            return true;

        return false;
    }

    public void closeDialogPanel()
    {
        // Play sound.
        audioManager.Play("ButtonClick");

        dialogPanel.SetActive(false);
    }

    private void showDialogPanel(string message)
    {
        // Play sound.
        audioManager.Play("Notify");

        dialogPanel.SetActive(true);
        dialogText.text = message;
    }
}
