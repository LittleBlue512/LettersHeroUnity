using System.IO;

public static class ProjectConst
{
    public enum Direction
    {
        UP = 0,
        RIGHT = 1,
        DOWN = 2,
        LEFT = 3
    };

    // Default Values.
    public static string[] defaultWords = { "HERO", "JOURNEY", "ACCIDENT", "RETURN", "ESCAPE", "CASTLE", "INSIDE", "SILENT", "ATTACK", "EVIL" };
    public static int maxLevel = 10; // Also determine the amount of words in the arrays.
    public static int maxWordLength = 8;


    // File Management.
    public static string dataFolder = "/LettersHero";
    public static string savePath = "";
    public static string configPath = "";
    public static string saveFolderName = "/saves";
    public static string configFolderName = "/configs";
    public static string saveFileName = "/save.txt";
    public static string wordFileName = "/words.txt";
}