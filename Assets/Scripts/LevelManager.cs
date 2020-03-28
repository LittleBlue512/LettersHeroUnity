using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject fadePanel;
    public Animator animator;
    public string FadeOutTrigger = "FadeOut";
    public int FadeDelay = 1;

    void Start()
    {
        StartCoroutine(FadeInDelay());

        // Notify the AudioManager that scnene have changed.
        notifyAuidoManager();
    }

    private void notifyAuidoManager()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().onSceneChange();
    }

    public void loadLevel(int buildIndex)
    {
        StartCoroutine(FadeOutDelay(buildIndex));
    }

    public void loadLevel(string name)
    {
        StartCoroutine(FadeOutDelay(name));
    }

    public void loadNextLevel()
    {
        StartCoroutine(FadeOutDelay(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void loadPreviousLevel()
    {
        StartCoroutine(FadeOutDelay(SceneManager.GetActiveScene().buildIndex - 1));
    }

    public void quitLevel()
    {
        StartCoroutine(FadeToQuit());
    }

    IEnumerator FadeInDelay()
    {
        // Wait.
        yield return new WaitForSeconds(FadeDelay);

        // Disable fade panel.
        fadePanel.SetActive(false);
    }

    IEnumerator FadeOutDelay(int buildIndex)
    {
        // Enable fade panel.
        fadePanel.SetActive(true);

        // Play fade out animation.
        animator.SetTrigger(FadeOutTrigger);

        // Wait.
        yield return new WaitForSeconds(FadeDelay);

        // Load next scene.
        SceneManager.LoadScene(buildIndex);
    }

    IEnumerator FadeOutDelay(string sceneName)
    {
        // Enable fade panel.
        fadePanel.SetActive(true);

        // Play fade out animation.
        animator.SetTrigger(FadeOutTrigger);

        // Wait.
        yield return new WaitForSeconds(FadeDelay);

        // Load next scene.
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator FadeToQuit()
    {
        // Enable fade panel.
        fadePanel.SetActive(true);

        // Play fade out animation.
        animator.SetTrigger(FadeOutTrigger);

        // Wait.
        yield return new WaitForSeconds(FadeDelay);

        // Quit.
        Application.Quit();
    }
}
