using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static Globals;
using static DifficultyPresetsLib;
using static EnemiesStatsLib;

public class EnemyManagerBase : MonoBehaviour
{
    public EnemyType enemyType = EnemyType.NORMAL;
    public EnemyAttackDataBase attackData;

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

    public virtual void Init(GameObject inTarget)
    {
        CacheSystems();
        FillAttackData(inTarget);

        enemyMovementController.Init(inTarget, EnemyStatsPreset);
        enemyAttackControllerBase.Init(attackData);

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

    private void HandleOnDeath()
    {
        ScoreManager.IncreaseScore(EnemyStatsPreset.scoreValue);
    }

    protected PveGameMode GameMode { get; set; }

    private EnemyStatsPreset EnemyStatsPreset { get; set; }
    private ClientHealthController ClientHealthController { get; set; }
    private DiffcultyPreset DiffPreset { get; set; }
    private ScoreManager ScoreManager { get; set; }

}
