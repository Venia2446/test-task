using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BulletsStructLib;
using static Globals;

public class RangeEnemyManager : EnemyManagerBase
{

    protected override void CacheSystems()
    {
        base.CacheSystems();

        BulletStruct = GameMode.bulletsStructLib.GetBulletStruct(BulletType.MEDIUM);
    }

    protected override void FillAttackData(GameObject inTarget)
    {
        base.FillAttackData(inTarget);

        var castedData = (EnemyRangeAttackData)attackData;
        castedData.BulletStruct = BulletStruct;
    }

    private BulletStruct BulletStruct { get; set; }

}
