using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BulletsStructLib;
using static Utils;

public class EnemyRangeAttackController : EnemyAttackControllerBase
{
    public override void Init(EnemyAttackDataBase enemyAttackDataBase)
    {
        base.Init(enemyAttackDataBase);

        var castedData = (EnemyRangeAttackData)enemyAttackDataBase;
        BulletStruct = castedData.BulletStruct;
        LocalTransform = gameObject.GetComponent<Transform>();
        TargetTransform = Target.GetComponent<Transform>();
        StartAttack();
    }

    protected override void Attack()
    {
        base.Attack();

        var angle = CalculateRotationAngle(TargetTransform.position, LocalTransform.position);
        LocalTransform.rotation = angle;

        BuildBullet(BulletStruct, LocalTransform.transform.position, angle);
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

    private Transform LocalTransform { get; set; }
    private Transform TargetTransform { get; set; }
    protected BulletStruct BulletStruct { get; set; }

}
