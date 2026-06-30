using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static Globals;

public class ClientAbilityControllerBase : MonoBehaviour
{
    public event Action OnAbilityActivated;
    public event Action OnAbilityReady;
    public event Action OnCooldownStarted;
    public event Action<float> OnCooldownUpdated;

    public virtual void Init(Ability inAbility)
    {
        AudioSystem = ((PveGameMode)AppSystemClient.Instance.GameMode).audioSystem;

        Ability = inAbility;
        Data = Ability.data;
        CanActivate = true;

        CooldownDelay = new WaitForSeconds(Data.cooldown);
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
        Cooldown = Data.cooldown;
        IsCooldownStatred = true;
        StartCoroutine(StopCooldown());
        OnCooldownStarted?.Invoke();
    }

    private IEnumerator StopCooldown()
    {
        yield return CooldownDelay;
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
    private WaitForSeconds CooldownDelay { get; set; }
    private bool IsCooldownStatred { get; set; }

}
