using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class BulletTriggersLib : MonoBehaviour
{
    //выпилить
    [SerializeField] public BulletTriggerData[] bulletTriggersDatas;

    private Dictionary<BulletType, BulletTriggerData> triggersData = new Dictionary<BulletType, BulletTriggerData>();

    public void Init()
    {
        foreach (var data in bulletTriggersDatas)
        {
            triggersData.Add(data.buletType, data);
        }
    }

    public BulletTriggerData GetPreset(BulletType type)
    {
        return triggersData[type];
    }

    [System.Serializable]
    public class BulletTriggerData
    {
        public BulletType buletType;
        [SerializeField] BulleTTrigger[] bulleTTriggers;
    }

    [System.Serializable]
    public class BulleTTrigger
    {
        public string collisionTag;
        public BulletAction action = BulletAction.DESTROY;
    }
}
