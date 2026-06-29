using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class HealthControllerBase : MonoBehaviour
{
    public DamageEffectController hitEffectController;

    public delegate void HandleOnReceivedDamage(StatType statType);
    public delegate void HandleOnReceivedHealthDamage(float currentHealt);
    public delegate void HandleOnDeath();

    public event HandleOnReceivedDamage OnReceiveDamage;
    public event HandleOnReceivedHealthDamage OnReceivedHealthDamage;
    public event HandleOnDeath OnDeath;

    public virtual void Init(HealthDataBase data)
    {
        AudioSystem = ((PveGameMode)AppSystemClient.Instance.GameMode).audioSystem;

        MaxHealth = data.MaxHealth;
        Health = MaxHealth;

        OnReceiveDamage += (_) => LocalHandleOnOnReceiveDamage();
    }

    public virtual void Terminate()
    {
        OnReceiveDamage -= (_) => LocalHandleOnOnReceiveDamage();
    }

    private void OnDestroy()
    {
        Terminate();
    }

    public virtual void RegisterTakingDamage(float damage)
    {
        TryUpdateDeadState(TryRegisterHealthDamage(damage));
    }

     protected float TryRegisterHealthDamage(float damage)
    {
        if (IsDead)
        {
            return Health;
        }

        var newHealth = Mathf.Clamp(Health - damage, MinHealth, MaxHealth);

        if (newHealth == Health)
        {
            return Health;
        }

        Health = newHealth;
        OnReceivedHealthDamage?.Invoke(Health);
        FireRecieveDamage(StatType.HEALTH);
        return Health;
    }

    protected void TryUpdateDeadState(float newHealth)
    {
        var newDeadState = (newHealth <= MinHealth);
        if (newDeadState != IsDead)
        {
            IsDead = newDeadState;
            OnDeath?.Invoke();
        }
    }
    protected virtual void LocalHandleOnOnReceiveDamage()
    {
        hitEffectController.ShowHitEffect();
    }

    protected void FireRecieveDamage(StatType type)
    {
        OnReceiveDamage?.Invoke(type);
    }

    public float MinHealth
    {
        get { return minHealth; }
    }

    public bool IsDead { get; protected set; }
    public float Health { get; protected set; }
    public float MaxHealth { get; protected set; }

    protected const float minHealth = 0;
    protected AudioSystem AudioSystem { get; set; }
}
