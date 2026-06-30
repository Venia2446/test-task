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

        var castedData = (EnemyRangeAttackData)enemyAttackDataBase;
        BulletStruct = castedData.BulletStruct;
        TargetTransform = Target.GetComponent<Transform>();
        StartAttack();
    }

    protected override void Attack()
    {
        base.Attack();

        var angle = CalculateRotationAngle(TargetTransform.position, localTranform.position);
        localTranform.rotation = angle;

        BuildBullet(BulletStruct, localTranform.transform.position, angle);
    }

    private void BuildBullet(BulletStruct bulletStructure, Vector3 position, Quaternion angle)
    {
        var obj = Instantiate(bulletStructure.gameObj, position, angle);
        var bulletData = new EnemyBulletData();
        bulletData.Angle = angle;
        bulletData.Damage = Damage;
        bulletData.BulletStruct = BulletStruct;
        bulletData.ClientHealthController = ClientHealthController;

        var bulletController = obj.GetComponent<EnemyBulletController>();
        bulletController.Init(bulletData);
    }

    private Transform TargetTransform { get; set; }
    protected BulletStruct BulletStruct { get; set; }

}
