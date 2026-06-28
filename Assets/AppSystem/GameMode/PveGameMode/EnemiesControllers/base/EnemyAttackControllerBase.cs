using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;
using static BulletsStructLib;

public class EnemyAttackControllerBase : MonoBehaviour
{
    public virtual void Init(ClientHealthController inClientHealthSystem, float inDamage, float inAttackFrequency, GameObject inTarget, BulletStruct inBulletStruct)
    {
        attackFrequency = inAttackFrequency;
        target = inTarget;
        clientHealthController = inClientHealthSystem;
        damage = inDamage;
        bulletStruct = inBulletStruct;
    }

    private void OnDestroy()
    {
        Terminate();
    }

    public virtual void Terminate()
    { 
        StopAttack(); 
    }

    protected virtual void StartAttack()
    {
        StartCoroutine(AttackCoroutine(attackFrequency));
        isAttacking = true;
    }

    private IEnumerator AttackCoroutine(float frequency)
    {
        Attack();
        while (true)
        {
            yield return new WaitForSeconds(frequency);
            Attack();
        }
    }

    protected virtual void StopAttack()
    {
        StopAllCoroutines();
        isAttacking = false;
    }

    protected virtual void Attack() {}

    protected float damage;
    protected float attackFrequency;
    protected bool isAttacking;

    protected ClientHealthController clientHealthController;
    protected GameObject target;
    protected BulletStruct bulletStruct;
}
