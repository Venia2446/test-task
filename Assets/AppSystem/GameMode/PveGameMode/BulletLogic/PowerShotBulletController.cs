using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShotBulletController : ClientBulletController
{

    public float scale = 15;

    public override void Init(BulletDataBase bulletData)
    {
        base.Init(bulletData);

        var castedData = (PowerShotBulletData)bulletData;
        var newScale = castedData.charge / scale;
        gameObject.GetComponent<Transform>().localScale += new Vector3(newScale, newScale, newScale);
    }

}
