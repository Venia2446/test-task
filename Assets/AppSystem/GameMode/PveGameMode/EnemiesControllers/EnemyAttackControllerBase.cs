using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BulletsStructLib;

public class EnemyAttackControllerBase : MonoBehaviour
{
    public virtual void Init(EnemyAttackDataBase enemyAttackDataBase)
    {
        AttackFrequency = enemyAttackDataBase.AttackFrequency;
        Target = enemyAttackDataBase.Target;
        ClientHealthController = enemyAttackDataBase.ClientHealthSystem;
        Damage = enemyAttackDataBase.Damage;

        AttackDelay = new WaitForSeconds(AttackFrequency);
    }

    public virtual void Terminate()
    { 
        StopAttack(); 
    }

    protected virtual void StartAttack()
    {
        StartCoroutine(AttackCoroutine());
        IsAttacking = true;
    }

    private IEnumerator AttackCoroutine()
    {
        Attack();
        while (true)
        {
            yield return AttackDelay;
            Attack();
        }
    }

    protected virtual void StopAttack()
    {
        StopAllCoroutines();
        IsAttacking = false;
    }

    protected virtual void Attack() {}

    protected ClientHealthController ClientHealthController { get; set; }
    protected GameObject Target { get; set; }
    private WaitForSeconds AttackDelay { get; set; }

    protected float Damage { get; set; }
    protected float AttackFrequency { get; set; }
    protected bool IsAttacking { get; set; }

}
