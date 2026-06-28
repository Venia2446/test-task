using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEvents;
using static AbilitiesLib;

public class ClientAbilityControllerBase : MonoBehaviour
{
    public delegate void HandleOnAbilityActivated();
    public event HandleOnAbilityActivated OnAbilityActivated;

    public delegate void HanldeOnAbilityReady();
    public event HanldeOnAbilityReady OnAbilityReady;

    public delegate void HandleOnCooldownStarted();
    public event HandleOnCooldownStarted OnCooldownStarted;

    public delegate void HandleOnCooldownUpdated(float cooldown);
    public event HandleOnCooldownUpdated OnCooldownUpdated;

    public virtual void Init(Ability inAbility)
    {

        audioSystem = ((PveGameMode)AppSystemClient.Instance.GameMode).audioSystem;

        ability = inAbility;
        data = ability.data;
        canActivate = true;
    }

    public void Terminate()
    {
        StopAllCoroutines();
    }

    private void OnDestroy()
    {
        Terminate();
    }

    public virtual void TryActivate()
    {
        if (!CanAtivate)
        {
            return;
        }

        Activate();
    }

    protected virtual void Activate()
    {
        canActivate = false;
        StartCooldown();
        FireAbilityActivated();
    }

    protected void FireAbilityActivated()
    {
        audioSystem.PlayOneShot(Globals.AudioClipType.PLAYER_SHOOTING);
        OnAbilityActivated?.Invoke();
    }

    protected void StartCooldown()
    {
        cooldown = data.cooldown;
        isCooldownStatred = true;
        StartCoroutine(StopCooldown(data.cooldown));
        OnCooldownStarted?.Invoke();
    }

    private IEnumerator StopCooldown(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSystem.PlayOneShot(Globals.AudioClipType.ABILITY);
        cooldown = 0;
        canActivate = true;
        isCooldownStatred = false;
        OnCooldownUpdated?.Invoke(cooldown);
        OnAbilityReady?.Invoke();
    }

    private void Update()
    {
        UpdateInner();
    }

    protected virtual void UpdateInner()
    {
        if (isCooldownStatred)
        {
            cooldown -= Time.deltaTime;
            OnCooldownUpdated?.Invoke(cooldown);
        }
    }

    protected virtual AbilityDataBase GetData()
    {
        return data;
    }

    public bool CanAtivate
    {
        get {return canActivate; }
    }

    public float Cooldown
    {
        get { return cooldown; }
    }

    protected bool isCooldownStatred;
    protected bool canActivate;
    protected Ability ability;
    protected AbilityDataBase data;
    
    private float cooldown;

    private AudioSystem audioSystem;
    
}
