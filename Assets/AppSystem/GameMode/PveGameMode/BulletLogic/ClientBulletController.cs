using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientBulletController : BulletControllerBase
{
    protected override void RegisterDamage(Collider2D collision)
    {
        base.RegisterDamage(collision);

        if (collision.gameObject.TryGetComponent(out EnemyHealthController enemyHealthController))
        {
            enemyHealthController.RegisterTakingDamage(Damage);
        }
    }
}