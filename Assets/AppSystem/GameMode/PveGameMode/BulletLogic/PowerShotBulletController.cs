using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShotBulletController : ClientBulletController
{

    public float scale = 15;
    public Transform transform;

    public override void Init(BulletDataBase bulletData)
    {
        base.Init(bulletData);

        var castedData = (PowerShotBulletData)bulletData;
        var newScale = castedData.Charge / scale;
        transform.localScale += new Vector3(newScale, newScale, newScale);
    }

}
