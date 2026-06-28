using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BulletsStructLib;
using static Utils;

public class EnemyRangeAttackController : EnemyAttackControllerBase
{
    public override void Init(ClientHealthController inClientHealthSystem, float inDamage, float inAttackFrequency, GameObject inTarget, BulletStruct inBulletStruct)
    {
        base.Init(inClientHealthSystem, inDamage, inAttackFrequency, inTarget, inBulletStruct);
        
        localTransform = gameObject.GetComponent<Transform>();
        targetTransform = target.GetComponent<Transform>();
        StartAttack();
    }

    protected override void Attack()
    {
        base.Attack();

        var angle = CalculateRotationAngle(targetTransform.position, localTransform.position);
        localTransform.rotation = angle;

        StartCoroutine(BuildBullet(bulletStruct, localTransform.transform.position, angle));
    }

    private IEnumerator BuildBullet(BulletStruct bulletStructure, Vector3 position, Quaternion angle)
    {
        var obj = Instantiate(bulletStructure.stuct, position, angle);
        var bulletData = new EnemyBulletData();
        bulletData.angle = angle;
        bulletData.damage = damage;
        bulletData.bulletStruct = bulletStruct;
        bulletData.clientHealthController = clientHealthController;

        var bulletController = obj.GetComponent<EnemyBulletController>();
        bulletController.Init(bulletData);

        yield return null;
    }

    private Transform localTransform;
    private Transform targetTransform;
}
