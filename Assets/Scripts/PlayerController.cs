/*
    This script attaches to the player.
*/

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ProjectConst.Direction playerDirection = ProjectConst.Direction.UP;
    public bool isMovementEnable;
    public float movementSpeed = 2000.0f;
    public bool isMoveUp = false;
    public bool isMoveDown = false;
    public bool isMoveRight = false;
    public bool isMoveLeft = false;

    private Animator animator;
    private Rigidbody playerRb;
    private Vector3 movement = new Vector3(0, 0, 0);

    void Start()
    {
        isMovementEnable = true;

        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isMovementEnable)
        {
            setStill();
            return;
        }

        GetMovementInputs();
        playerTurn();

        // Get player's moving state.
        if (isMoving())
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);
    }

    void FixedUpdate()
    {
        playerRb.AddForce(movement * movementSpeed * Time.deltaTime);
    }

    private bool isMoving()
    {
        if (isMoveUp || isMoveDown || isMoveRight || isMoveLeft)
            return true;
        return false;
    }

    private void setStill()
    {
        animator.SetBool("isMoving", false);
        movement = new Vector3(0, 0, 0);
        isMoveUp = false;
        isMoveDown = false;
        isMoveRight = false;
        isMoveLeft = false;
    }

    private void GetMovementInputs()
    {
        if (isMoveUp || isMoveDown)
        {
            if (isMoveUp)
            {
                movement.z = 1.0f;
                playerDirection = ProjectConst.Direction.UP;
            }
            if (isMoveDown)
            {
                movement.z = -1.0f;
                playerDirection = ProjectConst.Direction.DOWN;
            }
        }
        else
            movement.z = 0;

        if (isMoveRight || isMoveLeft)
        {
            if (isMoveRight)
            {
                movement.x = 1.0f;
                playerDirection = ProjectConst.Direction.RIGHT;
            }
            if (isMoveLeft)
            {
                movement.x = -1.0f;
                playerDirection = ProjectConst.Direction.LEFT;
            }
        }
        else
            movement.x = 0;
    }

    private void playerTurn()
    {
        switch (playerDirection)
        {
            case ProjectConst.Direction.UP:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case ProjectConst.Direction.DOWN:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case ProjectConst.Direction.RIGHT:
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case ProjectConst.Direction.LEFT:
                transform.rotation = Quaternion.Euler(0, 270, 0);
                break;
            default:
                break;
        }
    }
}
