using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public static bool isPlayBGM = false;

    public Sound[] sounds;
    public bool isRandomBGM = true;
    public int BGMcount = 3;
    public string backgroundMusicName;
    public string BossBGM = "BossBGM";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void onSceneChange()
    {
        if (isBossLevel())
        {
            stopBGM();
            backgroundMusicName = BossBGM;
            playBGM();
        }
        else
        {
            if (!isPlayBGM)
            {
                randomBGM();
                playBGM();
            }
        }
    }

    public void playBGM()
    {
        // Make sure there are BGM to play.
        if (backgroundMusicName == "")
            randomBGM();

        Play(backgroundMusicName);

        isPlayBGM = true;
    }

    public void stopBGM()
    {
        Stop(backgroundMusicName);
        isPlayBGM = false;
    }

    private void randomBGM()
    {
        System.Random rand = new System.Random();
        int randNum = (rand.Next(1, 100) % BGMcount) + 1;
        backgroundMusicName = "BGM" + randNum.ToString();
    }

    public void Play(String name)
    {
        // Find sound.
        Sound s = Array.Find(sounds, sound => sound.name == name);

        // Sound not found.
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + " not found!");
            return;
        }

        // Play sound.
        s.source.Play();
    }

    public void Stop(string name)
    {
        // Find sound.
        Sound s = Array.Find(sounds, sound => sound.name == name);

        // Sound not found.
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + " not found!");
            return;
        }

        // Stop sound.
        s.source.Stop();
    }

    public bool isBossLevel()
    {
        if (GameObject.Find("BossLevelManager") == null)
            return false;
        return true;
    }
}
