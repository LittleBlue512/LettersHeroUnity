/*
    This script attaches to the player.
*/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HealthBarManager : MonoBehaviour
{
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public LevelManager levelManager;
    public bool isAlive = true;
    public int defaultHealth = 3;

    [HideInInspector]
    public int playerHealth;

    private Animator playerAnimator;
    private PlayerController playerController;
    private AudioManager audioManager;

    void Start()
    {
        initialize();
    }

    private void initialize()
    {
        // Get components.
        playerAnimator = this.GetComponent<Animator>();
        playerController = this.GetComponent<PlayerController>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        // Initialize default data.
        isAlive = true;
        playerHealth = defaultHealth;
        heart1.SetActive(true);
        heart2.SetActive(true);
        heart3.SetActive(true);
    }

    public void playerGetHit(int damage)
    {
        // Animation: Player Get Hit.
        playerAnimator.SetTrigger("GetHit");

        // Decrease player' HP.
        playerHealth -= damage;
        UpdatePlayerHealth();
    }

    private void playerDie()
    {
        isAlive = false;

        // Play sound.
        audioManager.Play("PlayerDie");

        // Animation: Player Die.
        playerAnimator.SetTrigger("Die");

        // Disable player movement.
        playerController.isMovementEnable = false;

        StartCoroutine(PlayerDieFade(2));
    }

    IEnumerator PlayerDieFade(int time_seconds)
    {
        yield return new WaitForSeconds(time_seconds);

        // Restart level.
        levelManager.loadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdatePlayerHealth()
    {
        if (playerHealth < 0)
        {
            playerHealth = 0;
            return;
        }

        switch (playerHealth)
        {
            case 0:
                // PLAYER DIED. GAEM OVER!
                heart1.SetActive(false);
                heart2.SetActive(false);
                heart3.SetActive(false);

                playerDie();

                break;
            case 1:
                heart1.SetActive(true);
                heart2.SetActive(false);
                heart3.SetActive(false);
                break;
            case 2:
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(false);
                break;
            case 3:
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(true);
                break;
            default:
                break;
        }
    }
}
