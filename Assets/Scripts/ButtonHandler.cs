using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public BulletFire bulletFire;
    public PlayerController playerController;

    public void setFire()
    {
        bulletFire.Fire();
    }

    public void setUp()
    {
        playerController.isMoveUp = true;
    }

    public void setDown()
    {
        playerController.isMoveDown = true;
    }

    public void setRight()
    {
        playerController.isMoveRight = true;
    }

    public void setLeft()
    {
        playerController.isMoveLeft = true;
    }

    public void setStillUp()
    {
        playerController.isMoveUp = false;
    }

    public void setStillDown()
    {
        playerController.isMoveDown = false;
    }

    public void setStillRight()
    {
        playerController.isMoveRight = false;
    }

    public void setStillLeft()
    {
        playerController.isMoveLeft = false;
    }
}
