using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject wordPanel;
    public Text wordPanelText;

    private Animator wordPanelAnim;
    private AudioManager audioManager;

    void Start()
    {
        wordPanelAnim = wordPanel.GetComponent<Animator>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

    }

    public void showWordPanel(string word)
    {
        // Stop BGM.
        audioManager.stopBGM();

        // Play sound.
        audioManager.Play("LevelWin");

        wordPanelText.text = word;
        wordPanelAnim.SetTrigger("ShowWordPanel");
    }
}
