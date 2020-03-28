using UnityEngine;
using System.Collections;

public class BulletFire : MonoBehaviour
{
    public GameObject player;
    public GameObject gameManager;
    public GameObject bulletPrefab;
    public PlayerController playerController;
    public float fireDelay = 1f;
    public float bulletForce = 500f;
    public float timeToFire = 0f;
    public float bulletFirOffset = 1f;
    public float bulletFirHeight = 1f;

    private GameObject bulletClone;
    private Vector3 bulletDirection;
    private ProjectConst.Direction playerDirection;

    public void Fire()
    {
        // Fire a bullet.
        if (Time.time > timeToFire)
        {
            // Fire delay.
            timeToFire = Time.time + fireDelay;
            StartCoroutine(PlayerFire(0.25f));
        }
    }

    public void getDirection()
    {
        // Get bullet's direction.
        bulletDirection.x = 0f;
        bulletDirection.z = 0f;

        playerDirection = playerController.playerDirection;

        switch (playerDirection)
        {
            case ProjectConst.Direction.UP:
                bulletDirection.z = 1;
                break;
            case ProjectConst.Direction.DOWN:
                bulletDirection.z = -1;
                break;
            case ProjectConst.Direction.RIGHT:
                bulletDirection.x = 1;
                break;
            case ProjectConst.Direction.LEFT:
                bulletDirection.x = -1;
                break;
            default:
                break;
        }
    }

    IEnumerator PlayerFire(float time_seconds)
    {
        // Animation: Attack
        player.GetComponent<Animator>().SetTrigger("Attack");

        // Wait
        yield return new WaitForSeconds(time_seconds);

        getDirection();

        // Get bullet instantiate position.
        Vector3 bulletPosition = player.transform.position + bulletDirection * bulletFirOffset;
        bulletPosition.y = 1.5f;

        // Create a bullet.
        bulletClone = Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);

        // Add force to the bullet.
        bulletClone.GetComponent<Rigidbody>().AddForce(bulletDirection * bulletForce);
    }
}
