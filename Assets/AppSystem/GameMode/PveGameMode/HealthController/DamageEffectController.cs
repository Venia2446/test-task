using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffectController : MonoBehaviour
{
    public float hideDelaySec = 0.2f; 
    public GameObject hitEffect;
    public SpriteRenderer deathEffect;
    public SpriteRenderer normalState;

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void ShowHitEffect()
    {
        if (isHitShown)
        {
            return;
        }

        hitEffect.SetActive(true);
        StartCoroutine(HideEffect());
    }

    public void StartEnemyDeathEffect(float hideDelay)
    {
        StartCoroutine(StartDeathEffect(hideDelay));
    }

    private IEnumerator StartDeathEffect(float fadetime)
    {
        hitEffect.SetActive(false);
        normalState.enabled = false;
        deathEffect.enabled = true;

        var color = deathEffect.color;
        var deltatime = (fadetime / 0.1f);
        var delta = 1 / deltatime;

        while (color.a >= 0)
        {
            color.a -= delta;
            deathEffect.color = color;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator HideEffect()
    {
        yield return new WaitForSeconds(hideDelaySec);
        hitEffect.SetActive(false);
        isHitShown = false;
    }

    private bool isHitShown;

}
