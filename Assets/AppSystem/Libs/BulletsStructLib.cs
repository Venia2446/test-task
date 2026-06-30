using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class BulletsStructLib : MonoBehaviour
{
    public BulletStruct[] bulletsStructsRaw;

    public void Init()
    {
        foreach (var data in bulletsStructsRaw)
        {
            data.CreateTriggersData();
            bulletsStructs.Add(data.type, data);
        }
    }
    
    public BulletStruct GetBulletStruct(BulletType type)
    {
        return bulletsStructs[type];
    }

    private Dictionary<BulletType, BulletStruct> bulletsStructs = new Dictionary<BulletType, BulletStruct>();

}

[System.Serializable]
public class BulletStruct
{
    public BulletType type;
    public GameObject gameObj;
    public BulleTTrigger[] bulleTTriggersRaw;

    public BulletAction GetTriggerAction(string tag)
    {
        return (triggers.TryGetValue(tag, out BulletAction action)) ? action : BulletAction.NONE;
    }

    public void CreateTriggersData()
    {
        foreach (var tirggetData in bulleTTriggersRaw)
        {
            triggers.Add(tirggetData.collisionTag, tirggetData.action);
        }
    }

    private Dictionary<string, BulletAction> triggers = new Dictionary<string, BulletAction>();

}

[System.Serializable]
public class BulleTTrigger
{

    public string collisionTag;
    public BulletAction action = BulletAction.DESTROY;

}
