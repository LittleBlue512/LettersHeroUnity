using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public LetterManager letterManager;
    public float attackDelay = 1f;
    public float rotateSpeed = 10f;
    public float minFacingAngle = 5f;
    public float closeRange = 1.5f;
    public float detectRange = 5.0f;
    public int enemyDamage = 0;
    public int enemyHealth = 0;
    public bool isAlive = true;

    private NavMeshAgent navMeshAgent;
    private HealthBarManager healthBarManager;
    private AudioManager audioManager;
    private float timeToAttack = 0f;

    void Start()
    {
        initialize();
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            // Diable enemies if level is completed.
            if (letterManager.isCompleted)
            {
                enemyStop();
                return;
            }

            // Detect the player.
            detectPlayer();

            // Moving animation.
            if (isMoving())
                setMovingAnim(true);
            else
                setMovingAnim(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isAlive)
        {
            if (collision.gameObject.tag == "Bullet")
            {
                // The only "Bullet" now is "FireBall". ***
                int bulletDamage = collision.gameObject.GetComponent<FireBall>().damage;
                enemyGetHit(bulletDamage);
            }
        }
    }

    protected virtual void initialize()
    {
        isAlive = true;
        navMeshAgent = GetComponent<NavMeshAgent>();
        healthBarManager = player.GetComponent<HealthBarManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    protected void detectPlayer()
    {
        if (isPlayerInSight())
        {
            // Player is in sight.

            // If enemy is close to player, don't move.
            if (isCloseToPlayer())
            {
                // Stop
                enemyStop();

                // Make sure the enemy is facing the player.
                if (!isfacingPlayer())
                {
                    facePlayer();
                    return;
                }

                // Check if the enemy can attack.
                if (isReadyToAttack())
                    // Attack player.
                    attackPlayer(enemyDamage);
            }
            else
            {
                // Approach player.
                approachPlayer();
            }
        }
    }

    protected virtual void updateEnemyHealth()
    {
        // Play sound. (Enemy get hit sound)
        audioManager.Play("EnemyDie");

        if (enemyHealth <= 0)
        {
            // Disable collider when enemy is dead.
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    protected virtual void enemyGetHit(int damage)
    {
        enemyHealth -= damage;
        updateEnemyHealth();
    }

    protected bool isMoving()
    {
        if (navMeshAgent.velocity.magnitude > 0)
            return true;
        return false;
    }

    protected virtual void setMovingAnim(bool val)
    {

    }

    protected bool isReadyToAttack()
    {
        // If player is alive and it's time to attack.
        if (healthBarManager.isAlive && Time.time > timeToAttack)
        {
            // Add delay.
            timeToAttack = Time.time + attackDelay;
            return true;
        }
        return false;
    }

    protected bool isfacingPlayer()
    {
        Vector3 enemyToPlayer = player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, enemyToPlayer);
        if (angle <= minFacingAngle)
            return true;
        return false;
    }

    protected void facePlayer()
    {
        // Get relative position vector.
        Vector3 enemyToPlayer = player.transform.position - transform.position;

        // Get delta angle.
        float angle = Vector3.Angle(transform.forward, enemyToPlayer);

        // Get target rotation.
        Quaternion targetRotation = Quaternion.LookRotation(enemyToPlayer);

        // Rotate it.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    protected virtual void attackPlayer(int damage)
    {
        // Play sound.
        audioManager.Play("EnemyAttack");

        // Attack player, if level is not completed.
        if (!letterManager.isCompleted)
            healthBarManager.playerGetHit(damage);
    }

    protected void enemyStop()
    {
        navMeshAgent.SetDestination(transform.position);
    }

    protected void approachPlayer()
    {
        // Move to player's position.
        navMeshAgent.SetDestination(player.transform.position);
    }

    protected bool isCloseToPlayer()
    {
        // Get vector from enemy to player.
        Vector3 enemyToPlayer = player.transform.position - transform.position;

        // Get the magnitude of the vector.
        float distance = enemyToPlayer.magnitude;

        // Check if player is in close range.
        if (distance <= closeRange)
            return true;
        return false;
    }

    protected bool isPlayerInSight()
    {
        RaycastHit hit;

        // Get vector from enemy to player.
        Vector3 toPlayer = player.transform.position - transform.position;

        // Shoot ray and check if it hits player.
        if (Physics.Raycast(transform.position, toPlayer, out hit, detectRange))
            if (hit.transform.name == "Player")
                return true;
        return false;
    }
}
