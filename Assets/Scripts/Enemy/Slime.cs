/*
    This script attaches to the "SlimeEnemy" object.
*/

using UnityEngine;
using System.Collections;

public class Slime : EnemyController
{
    public string getHitAnim = "GetHit";
    public string attackAnim = "Attack";
    public string isDeadAnim = "isDead";
    public int destroyDelay = 2;
    public float attackAnimDelay = 0.2f;

    private Animator slimeAnim;

    void Start()
    {
        initialize();
    }

    protected override void initialize()
    {
        base.initialize();

        // Get components.
        slimeAnim = this.GetComponent<Animator>();

        // Initialize Slime values.
        // enemyDamage = 1;
        // enemyHealth = 1;
        // closeRange = 1.5f;
    }

    protected override void updateEnemyHealth()
    {
        base.updateEnemyHealth();

        if (enemyHealth <= 0)
        {
            // Slime die.
            isAlive = false;

            StartCoroutine(destroySlime(destroyDelay));
        }
    }

    protected override void enemyGetHit(int damage)
    {
        // Trigger animation.
        slimeAnim.SetTrigger(getHitAnim);

        base.enemyGetHit(damage);
    }

    protected override void attackPlayer(int damage)
    {
        StartCoroutine(attackPlayerDelay(attackAnimDelay, damage));
    }

    protected override void setMovingAnim(bool val)
    {
        slimeAnim.SetBool("isMoving", val);
    }

    IEnumerator destroySlime(int waitTimeSecond)
    {
        // Diable enemy movement.
        enemyStop();

        // Trigger animation.
        slimeAnim.SetBool(isDeadAnim, true);

        // Wait.
        yield return new WaitForSeconds(waitTimeSecond);

        // Kill it.
        Destroy(this.gameObject);
    }

    IEnumerator attackPlayerDelay(float waitTimeSecond, int damage)
    {
        // Trigger animation.
        slimeAnim.SetTrigger(attackAnim);

        // Wait.
        yield return new WaitForSeconds(waitTimeSecond);

        // Deal damage.
        base.attackPlayer(damage);
    }
}
