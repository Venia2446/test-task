using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class EnemiesStructLib : MonoBehaviour
{
    public GameObject[] enemies;

    public void Init()
    {
        foreach (var obj in enemies)
        {
            var enemyBase = obj.GetComponent<EnemyManagerBase>();
            enemiesInfo.Add(enemyBase.enemyType, new EnemyData(obj, enemyBase));
        }
    }

    public GameObject GetGameObj(EnemyType type)
    {
        return enemiesInfo[type].GameObj;
    }
    public EnemyManagerBase GetManager(EnemyType type)
    {
        return enemiesInfo[type].Manager;
    }

    public Dictionary<EnemyType, EnemyData> GetEnemiesDatas()
    {
        return enemiesInfo;
    }

    private Dictionary<EnemyType, EnemyData> enemiesInfo = new Dictionary<EnemyType, EnemyData>();

}

public class EnemyData
{
    public GameObject GameObj { get; private set; }
    public EnemyManagerBase Manager { get; private set; }

    public EnemyData(GameObject inEnemyObj, EnemyManagerBase inEnemyManager)
    {
        GameObj = inEnemyObj;
        Manager = inEnemyManager;
    }

}
