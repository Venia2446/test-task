using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : BulletControllerBase
{
    public override void Init(BulletDataBase bulletData)
    {
        base.Init(bulletData);

        var castedData = (EnemyBulletData)bulletData;
        clientHealthController = castedData.clientHealthController;
    }

    protected override void RegisterDamage(Collider2D collision)
    {
        base.RegisterDamage(collision);

        clientHealthController.RegisterTakingDamage(damage);
    }

    private ClientHealthController clientHealthController;
}
