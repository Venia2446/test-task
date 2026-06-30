using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class EnemyHealthController : HealthControllerBase
{

    public Collider2D collider;
    public EnemyMovementControllerBase movementController;
    public EnemyAttackControllerBase attackControllerBase;


    public override void Init(HealthDataBase data)
    {
        base.Init(data);

        OnDeath += Destroy;

        DestroyDelay = new WaitForSeconds(destoyObjDelay);
    }

    public override void Terminate()
    {
        OnDeath -= Destroy;

        base.Terminate();
    }

    protected override void LocalHandleOnReceiveDamage()
    {
        base.LocalHandleOnReceiveDamage();

        AudioSystem.PlayOneShot(AudioClipType.CLIENT_HIT);
    }

    protected void Destroy()
    {
        AudioSystem.PlayOneShot(AudioClipType.ENEMY_DEAD);

        collider.enabled = false;
        movementController.enabled = false;
        attackControllerBase.Terminate();

        hitEffectController.StartEnemyDeathEffect();
        StartCoroutine(DelayedDestroy());
    }

    private IEnumerator DelayedDestroy()
    {
        yield return DestroyDelay;
        Object.Destroy(gameObject);
    }

    private WaitForSeconds DestroyDelay { get; set; }

}
