/*
    This script attaches to the "Boss" object.
*/

using UnityEngine;
using System.Collections;

public class Boss : EnemyController
{
    public string getHitAnim = "GetHit";
    public string attackAnim = "Attack";
    public string isDeadAnim = "isDead";
    public int destroyDelay = 2;
    public float attackAnimDelay = 0.2f;

    private Animator bossAnim;

    void Start()
    {
        initialize();
    }

    protected override void initialize()
    {
        base.initialize();

        // Get components.
        bossAnim = this.GetComponent<Animator>();

        // Initialize Boss values.
        // enemyDamage = 1;
        // enemyHealth = 1;
        // closeRange = 1.5f;
    }

    protected override void updateEnemyHealth()
    {
        base.updateEnemyHealth();

        if (enemyHealth <= 0)
        {
            // Boss die.
            isAlive = false;

            StartCoroutine(destroyBoss(destroyDelay));
        }
    }

    protected override void enemyGetHit(int damage)
    {
        // Trigger animation.
        bossAnim.SetTrigger(getHitAnim);

        base.enemyGetHit(damage);
    }

    protected override void attackPlayer(int damage)
    {
        StartCoroutine(attackPlayerDelay(attackAnimDelay, damage));
    }

    protected override void setMovingAnim(bool val)
    {
        bossAnim.SetBool("isMoving", val);
    }

    IEnumerator destroyBoss(int waitTimeSecond)
    {
        // Diable enemy movement.
        enemyStop();

        // Trigger animation.
        bossAnim.SetBool(isDeadAnim, true);

        // Wait.
        yield return new WaitForSeconds(waitTimeSecond);

        // Kill it.
        Destroy(this.gameObject);
    }

    IEnumerator attackPlayerDelay(float waitTimeSecond, int damage)
    {
        // Trigger animation.
        bossAnim.SetTrigger(attackAnim);

        // Wait.
        yield return new WaitForSeconds(waitTimeSecond);

        // Deal damage.
        base.attackPlayer(damage);
    }
}
