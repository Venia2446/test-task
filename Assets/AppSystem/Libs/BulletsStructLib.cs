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
            bulletsStructs.Add(data.Type, data);
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
    public BulletType Type { get; set; }
    public GameObject gameObj { get; set; }
    public BulleTTrigger[] bulleTTriggersRaw;

    public BulletAction GetTriggerAction(string tag)
    {
        return (triggers.TryGetValue(tag, out BulletAction action)) ? action : BulletAction.NONE;
    }

    public void CreateTriggersData()
    {
        foreach (var tirggetData in bulleTTriggersRaw)
        {
            triggers.Add(tirggetData.CollisionTag, tirggetData.Action);
        }
    }

    private Dictionary<string, BulletAction> triggers = new Dictionary<string, BulletAction>();

}

[System.Serializable]
public class BulleTTrigger
{

    public string CollisionTag { get; set; }
    public BulletAction Action { get; set; } = BulletAction.DESTROY;

}
