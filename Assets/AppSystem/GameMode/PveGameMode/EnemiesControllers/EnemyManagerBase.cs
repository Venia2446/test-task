using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static Globals;
using static EnemiesStatsLib;

public class EnemyManagerBase : MonoBehaviour
{
    public EnemyType enemyType = EnemyType.NORMAL;
    public EnemyAttackDataBase attackData;
    public Collider2D collider;
    public DamageEffectController hitEffectController;

    public EnemyMovementControllerBase enemyMovementController;
    public EnemyAttackControllerBase enemyAttackControllerBase;
    public EnemyHealthController enemyHealthController;

    public void Awake()
    {
        var appSystem = AppSystemClient.Instance;
        EnemyStatsPreset = appSystem.enemyStatsLib.GetPreset(enemyType);
        DiffPreset = appSystem.GameMode.DiffcultyPreset;

        GameMode = (PveGameMode)(appSystem.GameMode);

        ClientHealthController = GameMode.playerController.clientHealthController;
        ScoreManager = GameMode.scoreManager;
    }

    private void OnDestroy()
    {
        Terminate();
    }

    public virtual void Init(GameObject inTarget)
    {
        if (IsInited)
        {
            return;
        }

        IsInited = true;
        InitInner(inTarget);
    }

    public void Terminate()
    {
        IsInited = false;

        if (enemyHealthController != null)
        {
            enemyHealthController.OnRequestReturnToPool -= ReturnToPool;
            enemyHealthController.OnDeath -= HandleOnDeath;
            enemyHealthController.OnReceivedHealthDamage -= HandleOnReceivedHealthDamage;
            enemyHealthController.Terminate();
        }

        enemyAttackControllerBase?.Terminate();
    }

    protected void InitInner(GameObject inTarget)
    {
        CacheSystems();
        FillAttackData(inTarget);

        enemyMovementController.Init(inTarget, EnemyStatsPreset);
        enemyAttackControllerBase.Init(attackData);

        enemyHealthController.OnRequestReturnToPool += ReturnToPool;
        enemyHealthController.OnReceivedHealthDamage += HandleOnReceivedHealthDamage;
        enemyHealthController.OnDeath += HandleOnDeath;
        enemyHealthController.Init(CreateHealthData(CalculateParamScale(DiffPreset.EnemyHealthScale, EnemyStatsPreset.hp)));
    }

    protected virtual void CacheSystems()
    {
        var appSystem = AppSystemClient.Instance;
        EnemyStatsPreset = appSystem.enemyStatsLib.GetPreset(enemyType);
        DiffPreset = appSystem.GameMode.DiffcultyPreset;

        GameMode = (PveGameMode)(appSystem.GameMode);

        ClientHealthController = GameMode.playerController.clientHealthController;
        ScoreManager = GameMode.scoreManager;
        Pool = GameMode.enemiesPool;
    }

    protected virtual void FillAttackData(GameObject inTarget)
    {
        attackData.ClientHealthSystem = ClientHealthController;
        attackData.Damage = CalculateParamScale(DiffPreset.EnemyDamageScale, EnemyStatsPreset.damage);
        attackData.AttackFrequency = EnemyStatsPreset.attackFrequency;
        attackData.Target = inTarget;
    }

    private float CalculateParamScale(float paramScale, float baseParam)
    {
        return paramScale * baseParam;
    }

    private HealthDataBase CreateHealthData(float inMaxHealth)
    {
        var healthData = new HealthDataBase();
        healthData.MaxHealth = inMaxHealth;
        return healthData;
    }

    private void HandleOnReceivedHealthDamage(float damage)
    {
        hitEffectController.ShowHitEffect();
    }

    private void HandleOnDeath()
    {
        collider.enabled = false;
        enemyMovementController.enabled = false;
        enemyAttackControllerBase.enabled = false;
        hitEffectController.StartEnemyDeathEffect();

        ScoreManager.IncreaseScore(EnemyStatsPreset.scoreValue);
    }

    private void ReturnToPool()
    {
        Pool.ReturnToPool(enemyType, gameObject);

        hitEffectController.ResetEffects();
        enemyHealthController.ResetController();
        collider.enabled = true;
        enemyMovementController.enabled = true;
        enemyAttackControllerBase.enabled = true;
    }

    protected PveGameMode GameMode { get; set; }

    private EnemyStatsPreset EnemyStatsPreset { get; set; }
    private ClientHealthController ClientHealthController { get; set; }
    private DiffcultyPreset DiffPreset { get; set; }
    private ScoreManager ScoreManager { get; set; }
    private EnemiesPool Pool { get; set; }
    private bool IsInited { get; set; }

}
