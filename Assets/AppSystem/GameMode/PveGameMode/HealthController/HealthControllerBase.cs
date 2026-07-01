using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static Globals;

public class HealthControllerBase : MonoBehaviour
{

    public event Action<StatType> OnRecieveDamage;
    public event Action<float> OnReceivedHealthDamage;
    public event Action OnDeath;

    public virtual void Init(HealthDataBase data)
    {
        GameMode = (PveGameMode)AppSystemClient.Instance.GameMode;
        AudioSystem = GameMode.audioSystem;

        MaxHealth = data.MaxHealth;
        Health = MaxHealth;

        OnRecieveDamage += (_) => LocalHandleOnReceiveDamage();
    }

    public virtual void Terminate()
    {
        OnRecieveDamage -= (_) => LocalHandleOnReceiveDamage();
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

        if (Mathf.Approximately(newHealth, Health))
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
        var newDeadState = Mathf.Approximately(newHealth, MinHealth);
        if (newDeadState != IsDead)
        {
            IsDead = newDeadState;
            OnDeath?.Invoke();
        }
    }

    protected virtual void LocalHandleOnReceiveDamage()
    {
    }

    protected void FireRecieveDamage(StatType type)
    {
        OnRecieveDamage?.Invoke(type);
    }

    public float MinHealth { get { return minHealth; } }
    public bool IsDead { get; protected set; }
    public float Health { get; protected set; }
    public float MaxHealth { get; protected set; }

    protected const float minHealth = 0;
    protected AudioSystem AudioSystem { get; set; }
    protected PveGameMode GameMode { get; set; }
}
