using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static Globals;

public class EnemyHealthController : HealthControllerBase
{

    public Action OnRequestReturnToPool;

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
        StartCoroutine(DelayedDestroy());
    }

    private IEnumerator DelayedDestroy()
    {
        yield return DestroyDelay;
        OnRequestReturnToPool?.Invoke();
    }

    public void ResetController()
    {
        IsDead = false;
        Health = MaxHealth;
    }

    private WaitForSeconds DestroyDelay { get; set; }

}
