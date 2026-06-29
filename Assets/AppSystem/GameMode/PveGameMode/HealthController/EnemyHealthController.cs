using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class EnemyHealthController : HealthControllerBase
{
    public override void Init(HealthDataBase data)
    {
        base.Init(data);

        OnDeath += Destroy;
    }

    public override void Terminate()
    {
        OnDeath -= Destroy;

        base.Terminate();
    }

    protected override void LocalHandleOnOnReceiveDamage()
    {
        base.LocalHandleOnOnReceiveDamage();

        AudioSystem.PlayOneShot(AudioClipType.CLIENT_HIT);
    }

    protected void Destroy()
    {
        AudioSystem.PlayOneShot(AudioClipType.ENEMY_DEAD);

        gameObject.GetComponent<Collider2D>().enabled = false;

        gameObject.GetComponent<EnemyMovementControllerBase>().enabled = false;
        gameObject.GetComponent<EnemyAttackControllerBase>().Terminate();

        hitEffectController.StartEnemyDeathEffect(destoyObjDelay);
        StartCoroutine(DelayedDestroy());
    }

    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(destoyObjDelay);
        Object.Destroy(gameObject);
    }

    private const float destoyObjDelay = 1.5f;
}
