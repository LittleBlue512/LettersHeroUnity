using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LetterManager : MonoBehaviour
{
    public GameObject[] characters;
    public Vector3[] charactersPos;
    public GameObject letterBox;
    public SaveSystem saveSystem;
    public LevelManager levelManager;
    public PlayerController playerController;
    public PanelManager panelManager;
    public string word;
    public int currentLevel;
    public bool isCompleted;

    private AudioManager audioManager;
    private char[] wordCharArr;
    private char[] letterLeft;
    private int currLetterCnt;
    private int maxWordLength;
    private int wordLength;
    private string characterPath;
    private string savePath;
    private string configPath;
    private string wordFileName;

    void Start()
    {
        configPath = ProjectConst.configPath;
        wordFileName = ProjectConst.wordFileName;
        characterPath = "Characters/";
        isCompleted = false;

        initialize();
        getCharacterPosition();
        getCharacterGameObject();
        InstantiateLetter();
    }

    private void initialize()
    {
        // Save game.
        int currBuilIndex = SceneManager.GetActiveScene().buildIndex;
        saveSystem.saveGame(currBuilIndex);

        // Load word.
        loadWords();

        // Get word's length.
        wordLength = word.Length;

        // Initialize arrays.
        characters = new GameObject[wordLength];
        charactersPos = new Vector3[wordLength];
        wordCharArr = new char[wordLength];

        currLetterCnt = 0;

        // Initialize spaces.
        for (int i = 0; i < wordLength; i++)
            wordCharArr[i] = ' ';

        // Show word in UI.
        displayWord();

        letterLeft = stringToCharArr(word);

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void loadWords()
    {
        string jsonString = File.ReadAllText(configPath + wordFileName);
        WordsObject wordObject = JsonUtility.FromJson<WordsObject>(jsonString);
        word = wordObject.words[currentLevel - 1].ToUpper();
    }

    private void displayWord()
    {
        letterBox.GetComponent<Text>().text = new string(wordCharArr);
    }

    private void getCharacterPosition()
    {
        for (int i = 0; true; i++)
        {
            GameObject letterHolder = GameObject.Find(string.Format("letterHolder ({0})", i + 1));

            if (letterHolder == null)
            {
                // Get max word length from looping through all letterHolders.
                maxWordLength = i;
                break;
            }

            if (i < wordLength)
                charactersPos[i] = letterHolder.transform.position;

            Destroy(letterHolder.gameObject);
        }
    }

    private void getCharacterGameObject()
    {
        for (int i = 0; i < wordLength; i++)
        {
            characters[i] = Resources.Load<GameObject>(characterPath + word[i]);
        }
    }

    private void InstantiateLetter()
    {
        for (int i = 0; i < wordLength; i++)
        {
            GameObject charactersClone = Instantiate(characters[i], charactersPos[i], Quaternion.Euler(0, 180, 0));
        }
    }

    public void addLetter(char letter)
    {
        // Play sound.
        audioManager.Play("CoinCollect");

        int letterIndex = findCharArr(letterLeft, letter);
        letterLeft[letterIndex] = ' ';
        wordCharArr[letterIndex] = letter;
        currLetterCnt += 1;

        // Check if all letters are collected.
        if (currLetterCnt == wordLength)
            levelComplete();

        displayWord();
    }

    private void levelComplete()
    {
        isCompleted = true;

        // Disable player movement.
        playerController.isMovementEnable = false;

        // Show word panel.
        panelManager.showWordPanel(word);
    }

    private int findCharArr(char[] arr, char target)
    {
        int length = arr.Length;
        for (int i = 0; i < length; i++)
            if (arr[i] == target)
                return i;
        return -1;
    }

    private char[] stringToCharArr(string str)
    {
        int length = str.Length;
        char[] output = new char[length];
        for (int i = 0; i < length; i++)
            output[i] = str[i];
        return output;
    }
}
