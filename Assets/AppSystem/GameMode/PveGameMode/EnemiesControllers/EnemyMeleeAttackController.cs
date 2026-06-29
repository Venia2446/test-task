using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackController : EnemyAttackControllerBase
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsAttackCollided(collision))
        {
            StartAttack();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (IsAttackCollided(collision))
        {
            StopAttack();
        }
    }

    protected override void Attack()
    {
        base.Attack();

        ClientHealthController.RegisterTakingDamage(Damage);
    }

    private bool IsAttackCollided(Collision2D collision)
    {
        return collision.gameObject.CompareTag(Globals.playerTag);
    }

}
