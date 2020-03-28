/*
    This script attaches to the "TurtleShellEnemy" object.
*/

using UnityEngine;
using System.Collections;

public class TurtleShell : EnemyController
{
    public string getHitAnim = "GetHit";
    public string attackAnim = "Attack";
    public string isDeadAnim = "isDead";
    public int destroyDelay = 2;
    public float attackAnimDelay = 0.2f;

    private Animator turtleAnim;

    void Start()
    {
        initialize();
    }

    protected override void initialize()
    {
        base.initialize();

        // Get components.
        turtleAnim = this.GetComponent<Animator>();

        // Initialize Turtle values.
        // enemyDamage = 1;
        // enemyHealth = 1;
        // closeRange = 1.5f;
    }

    protected override void updateEnemyHealth()
    {
        base.updateEnemyHealth();

        if (enemyHealth <= 0)
        {
            // Turtle die.
            isAlive = false;

            StartCoroutine(destroyTurtle(destroyDelay));
        }
    }

    protected override void enemyGetHit(int damage)
    {
        // Trigger animation.
        turtleAnim.SetTrigger(getHitAnim);

        base.enemyGetHit(damage);
    }

    protected override void attackPlayer(int damage)
    {
        StartCoroutine(attackPlayerDelay(attackAnimDelay, damage));
    }

    protected override void setMovingAnim(bool val)
    {
        turtleAnim.SetBool("isMoving", val);
    }

    IEnumerator destroyTurtle(int waitTimeSecond)
    {
        // Diable enemy movement.
        enemyStop();

        // Trigger animation.
        turtleAnim.SetBool(isDeadAnim, true);

        // Wait.
        yield return new WaitForSeconds(waitTimeSecond);

        // Kill it.
        Destroy(this.gameObject);
    }

    IEnumerator attackPlayerDelay(float waitTimeSecond, int damage)
    {
        // Trigger animation.
        turtleAnim.SetTrigger(attackAnim);

        // Wait.
        yield return new WaitForSeconds(waitTimeSecond);

        // Deal damage.
        base.attackPlayer(damage);
    }
}
