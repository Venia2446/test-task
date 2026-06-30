using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Globals;

public class DamageEffectController : MonoBehaviour
{
    public float hideDelaySec = 0.2f; 
    
    public GameObject hitEffect;
    public SpriteRenderer deathEffect;
    public SpriteRenderer normalState;

    private void Awake()
    {
        DeathEffectHideDelay = new WaitForSeconds(0.1f);
        HitEffectHideDelay = new WaitForSeconds(hideDelaySec);
    }

    public void ShowHitEffect()
    {
        if (IsHitShown)
        {
            return;
        }

        hitEffect.SetActive(true);
        StartCoroutine(HideEffect());
    }

    public void StartEnemyDeathEffect()
    {
        StartCoroutine(StartDeathEffect());
    }

    private IEnumerator StartDeathEffect()
    {
        hitEffect.SetActive(false);
        normalState.enabled = false;
        deathEffect.enabled = true;

        var color = deathEffect.color;
        var deltatime = (destoyObjDelay / 0.1f);
        var delta = 1 / deltatime;

        while (!Mathf.Approximately(color.a, 0))
        {
            color.a -= delta;
            deathEffect.color = color;
            yield return DeathEffectHideDelay;
        }
    }

    private IEnumerator HideEffect()
    {
        yield return HitEffectHideDelay;
        hitEffect.SetActive(false);
        IsHitShown = false;
    }

    private WaitForSeconds DeathEffectHideDelay { get; set; }
    private WaitForSeconds HitEffectHideDelay { get; set; }

    private bool IsHitShown { get; set; }

}
