using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class EnemiesStructLib : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;

    public void Init()
    {
        foreach (var obj in enemies)
        {
            var enemyBase = obj.GetComponent<EnemyBase>();
            enemiesInfo.Add(enemyBase.enemyType, new EnemyData(obj, enemyBase));
        }
    }

    public GameObject GetEnemyObj(enemyType type)
    {
        return enemiesInfo[type].GetObj();
    }
    public EnemyBase GetEnemyBase(enemyType type)
    {
        return enemiesInfo[type].GetBase();
    }

    public Dictionary<enemyType, EnemyData> GetEnemiesDatas()
    {
        return enemiesInfo;
    }

    private Dictionary<enemyType, EnemyData> enemiesInfo = new Dictionary<enemyType, EnemyData>();
}

public class EnemyData
{
    private GameObject enemyObj;
    private EnemyBase enemyBase;

    public EnemyData(GameObject inEnemyObj, EnemyBase inEnemyBase)
    {
        enemyObj = inEnemyObj;
        enemyBase = inEnemyBase;
    }

    public GameObject GetObj()
    {
        return enemyObj;
    }

    public EnemyBase GetBase()
    {
        return enemyBase;
    }
}
