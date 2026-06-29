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
    }

    public virtual void Terminate()
    { 
        StopAttack(); 
    }

    protected virtual void StartAttack()
    {
        StartCoroutine(AttackCoroutine(AttackFrequency));
        IsAttacking = true;
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
        IsAttacking = false;
    }

    protected virtual void Attack() {}

    protected ClientHealthController ClientHealthController { get; set; }
    protected GameObject Target { get; set; }
    protected float Damage { get; set; }
    protected float AttackFrequency { get; set; }
    protected bool IsAttacking { get; set; }

}
