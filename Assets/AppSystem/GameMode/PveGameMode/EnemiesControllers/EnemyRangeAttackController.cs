using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BulletsStructLib;
using static Utils;

public class EnemyRangeAttackController : EnemyAttackControllerBase
{

    public Transform localTranform;

    public override void Init(EnemyAttackDataBase enemyAttackDataBase)
    {
        base.Init(enemyAttackDataBase);

        var gameMode = (PveGameMode)AppSystemClient.Instance.GameMode;
        BulletsPool = gameMode.bulletsPool;

        var castedData = (EnemyRangeAttackData)enemyAttackDataBase;
        BulletStruct = castedData.BulletStruct;
        TargetTransform = Target.GetComponent<Transform>();
    }

    private void OnEnable()
    {
        StartAttack();
    }

    private void OnDisable()
    {
        StopAttack();
    }

    protected override void Attack()
    {
        base.Attack();

        if (TargetTransform == null)
        {
            return;
        }

        var angle = CalculateRotationAngle(TargetTransform.position, localTranform.position);
        localTranform.rotation = angle;

        BuildBullet(localTranform.transform.position, angle);
    }

    private void BuildBullet(Vector3 position, Quaternion angle)
    {
        var bulletData = new EnemyBulletData();
        bulletData.Angle = angle;
        bulletData.Damage = Damage;
        bulletData.BulletStruct = BulletStruct;
        bulletData.ClientHealthController = ClientHealthController;

        var obj = BulletsPool.GetFromPool(BulletStruct.type, position, angle);
        var bulletController = obj.GetComponent<EnemyBulletController>();
        bulletController.Init(bulletData);
    }

    protected BulletStruct BulletStruct { get; set; }
    
    private Transform TargetTransform { get; set; }
    private BulletsPool BulletsPool { get; set; }

}
