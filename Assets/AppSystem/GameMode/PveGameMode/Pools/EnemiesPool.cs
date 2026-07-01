using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class EnemiesPool : MonoBehaviour
{
    public EnemyPool[] EnemyPools;

    public void Init(EnemiesStructLib lib)
    {
        foreach (var poolData in EnemyPools)
        {
            pools.Add(poolData.type, poolData.pool);
            poolData.pool.Init(lib.GetGameObj(poolData.type));
        }
    }

    public GameObject GetFromPool(EnemyType type, Transform transform)
    {
        return pools[type].GetFromPool(transform);
    }

    public void ReturnToPool(EnemyType type, GameObject obj)
    {
        pools[type].ReturnToPool(obj);
    }

    private Dictionary<EnemyType, PoolBase> pools = new Dictionary<EnemyType, PoolBase>();

}

[System.Serializable]
public class EnemyPool
{
    public EnemyType type;
    public PoolBase pool;

}