using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : BulletControllerBase
{
    public override void InitInner(BulletDataBase bulletData)
    {
        base.InitInner(bulletData);

        var castedData = (EnemyBulletData)bulletData;
        ClientHealthController = castedData.ClientHealthController;
    }

    protected override void RegisterDamage(Collider2D collision)
    {
        base.RegisterDamage(collision);

        ClientHealthController.RegisterTakingDamage(Damage);
    }

    private ClientHealthController ClientHealthController { get; set; }

}
