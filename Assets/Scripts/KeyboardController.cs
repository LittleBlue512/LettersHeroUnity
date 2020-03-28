using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    public GameObject gameManager;
    public PlayerController playerController;
    public ButtonHandler buttonHandler;

    void Update() {
        if (Input.GetKeyDown(KeyCode.W))
            playerController.isMoveUp = true;
        else if (Input.GetKeyUp(KeyCode.W))
            playerController.isMoveUp = false;

        if (Input.GetKeyDown(KeyCode.A))
            playerController.isMoveLeft = true;
        else if (Input.GetKeyUp(KeyCode.A))
            playerController.isMoveLeft = false;

        if (Input.GetKeyDown(KeyCode.S))
            playerController.isMoveDown = true;
        else if (Input.GetKeyUp(KeyCode.S))
            playerController.isMoveDown = false;

        if (Input.GetKeyDown(KeyCode.D))
            playerController.isMoveRight = true;
        else if (Input.GetKeyUp(KeyCode.D))
            playerController.isMoveRight = false;

        if (Input.GetKeyUp(KeyCode.K))
            buttonHandler.setFire();
    }
}
