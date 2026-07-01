using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public Transform spawnerTransform;

    public void Init(EnemiesPool inEnemiesPool)
    {
        enemiesPool = inEnemiesPool;
    }

    public void SpawnEnemy(EnemyData inEnemyData, GameObject target)
    {
        var obj = enemiesPool.GetFromPool(inEnemyData.manager.enemyType, spawnerTransform);
        obj.GetComponent<EnemyManagerBase>().Init(target);
    }

    private EnemiesPool enemiesPool;

}