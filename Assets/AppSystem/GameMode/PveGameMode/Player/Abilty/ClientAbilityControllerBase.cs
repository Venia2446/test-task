using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static Globals;
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
        AudioSystem = ((PveGameMode)AppSystemClient.Instance.GameMode).audioSystem;

        Ability = inAbility;
        Data = Ability.Data;
        CanActivate = true;
    }

    public void Terminate()
    {
        StopAllCoroutines();
    }

    public virtual void TryActivate()
    {
        if (!CanActivate)
        {
            return;
        }

        Activate();
    }

    protected virtual void Activate()
    {
        CanActivate = false;
        StartCooldown();
        FireAbilityActivated();
    }

    protected void FireAbilityActivated()
    {
        AudioSystem.PlayOneShot(AudioClipType.PLAYER_SHOOTING);
        OnAbilityActivated?.Invoke();
    }

    protected void StartCooldown()
    {
        Cooldown = Data.Cooldown;
        IsCooldownStatred = true;
        StartCoroutine(StopCooldown(Data.Cooldown));
        OnCooldownStarted?.Invoke();
    }

    private IEnumerator StopCooldown(float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioSystem.PlayOneShot(AudioClipType.ABILITY);
        Cooldown = 0;
        CanActivate = true;
        IsCooldownStatred = false;
        OnCooldownUpdated?.Invoke(Cooldown);
        OnAbilityReady?.Invoke();
    }

    private void Update()
    {
        UpdateInner();
    }

    protected virtual void UpdateInner()
    {
        if (IsCooldownStatred)
        {
            Cooldown -= Time.deltaTime;
            OnCooldownUpdated?.Invoke(Cooldown);
        }
    }

    public float Cooldown { get; private set; }

    protected AbilityDataBase Data { get; set; }
    protected bool CanActivate { get; set; }

    private Ability Ability { get; set; }
    private AudioSystem AudioSystem { get; set; }
    private bool IsCooldownStatred { get; set; }

}
