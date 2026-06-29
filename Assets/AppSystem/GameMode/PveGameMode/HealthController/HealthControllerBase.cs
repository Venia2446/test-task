using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class HealthControllerBase : MonoBehaviour
{
    public HitEffectController hitEffectController;

    public delegate void HandleOnReceivedDamage(StatType statType);
    public delegate void HandleOnReceivedHealthDamage(float currentHealt);
    public delegate void HandleOnDeath();

    public event HandleOnReceivedDamage OnReceiveDamage;
    public event HandleOnReceivedHealthDamage OnReceivedHealthDamage;
    public event HandleOnDeath OnDeath;

    public virtual void Init(HealthDataBase data)
    {
        audioSystem = ((PveGameMode)AppSystemClient.Instance.GameMode).audioSystem;

        MaxHealth = data.maxHealth;
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

     protected bool TryRegisterHealthDamage(float damage)
    {
        if (IsDead)
        {
            return IsDead;
        }

        var newHealth = Health - damage;
        if (newHealth == Health)
        {
            return IsDead;
        }

        var newDeadState = (newHealth <= MinHealth);
        Health = Mathf.Clamp(newHealth, MinHealth, MaxHealth);

        OnReceivedHealthDamage?.Invoke(Health);
        FireRecieveDamage(StatType.HEALTH);
        return newDeadState;
    }

    protected void TryUpdateDeadState(bool newState)
    {
        if (newState != IsDead)
        {
            IsDead = newState;
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

    public bool IsDead
    {
        set { isDead = value; }
        get { return isDead; }
    }

    public float Health
    {
        set { health = value; }
        get { return health; }
    }

    public float MaxHealth
    {
        set { maxHealth = value; }
        get { return maxHealth; }
    }

    public float MinHealth
    {
        get { return minHealth; }
    }

    protected const float minHealth = 0;

    protected bool isDead;
    protected float health;
    protected float maxHealth;

    protected AudioSystem audioSystem;
}
