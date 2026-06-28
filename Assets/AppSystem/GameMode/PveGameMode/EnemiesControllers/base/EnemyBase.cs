using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;
using static DifficultyPresetsLib;
using static BulletsStructLib;
using static EnemiesStatsLib;
public class EnemyBase : MonoBehaviour
{
    public enemyType enemyType = enemyType.NORMAL;

    public EnemyMovementControllerBase enemyMovementController;
    public EnemyAttackControllerBase enemyAttackControllerBase;
    public EnemyHealthController enemyHealthController;

    public void Awake()
    {
        var appSystem = AppSystemClient.Instance;
        enemyStatsPreset = appSystem.enemyStatsLib.GetPreset(enemyType);
        diffPreset = appSystem.GameMode.DiffcultyPreset;

        var gameMode = (PveGameMode)(appSystem.GameMode);
        bulletStruct = gameMode.bulletsStructLib.GetBulletStruct(BulletType.MEDIUM);
        clientHealthController = gameMode.playerController.clientHealthController;
        scoreManager = gameMode.scoreManager;
    }

    public void Init(GameObject inTarget)
    {
        enemyMovementController.Init(inTarget, enemyStatsPreset);
        enemyAttackControllerBase.Init(clientHealthController, CalculateParamScale(diffPreset.enemyDamageScale, enemyStatsPreset.damage), enemyStatsPreset.attackFrequency, inTarget, bulletStruct);

        enemyHealthController.OnDeath += HandleOnDeath;
        enemyHealthController.Init(CreateHealthData(CalculateParamScale(diffPreset.enemyHealthScale, enemyStatsPreset.hp)));
    }

    private float CalculateParamScale(float paramScale, float baseParam)
    {
        return paramScale * baseParam;
    }

    private HealthDataBase CreateHealthData(float inMaxHealth)
    {
        var healthData = new HealthDataBase();
        healthData.maxHealth = inMaxHealth;
        return healthData;
    }

    private void HandleOnDeath()
    {
        scoreManager.IncreaseScore(enemyStatsPreset.scoreValue);
    }

    private EnemyStatsPreset enemyStatsPreset;
    private ClientHealthController clientHealthController;
    private DiffcultyPreset diffPreset;
    private BulletStruct bulletStruct;
    private ScoreManager scoreManager;

}
