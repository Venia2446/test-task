using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class BulletsPool : MonoBehaviour
{
    public BulletPool[] bulletsPools;

    public void Init(BulletsStructLib lib)
    {
        foreach (var poolData in bulletsPools)
        {
            pools.Add(poolData.type, poolData.pool);
            poolData.pool.Init(lib.GetBulletStruct(poolData.type).gameObj);
        }
    }

    public GameObject GetFromPool(BulletType type, Vector3 position, Quaternion rotation)
    {
        return pools[type].GetFromPool(position, rotation);
    }

    public void ReturnToPool(BulletType type, GameObject obj)
    {
        pools[type].ReturnToPool(obj);
    }

    private Dictionary<BulletType, PoolBase> pools = new Dictionary<BulletType, PoolBase>();

}

[System.Serializable]
public class BulletPool
{
    public BulletType type;
    public PoolBase pool;

}
