using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShotBulletController : ClientBulletController
{

    public float scale = 15;
    public Transform transform;
    public Vector3 defaultSclate;

    protected override void CacheData(BulletDataBase bulletData)
    {
        base.CacheData(bulletData);

        Data = (PowerShotBulletData)bulletData;
    }

    protected override void StartMovement(Quaternion angle)
    {
        base.StartMovement(angle);

        var newScale = Data.Charge / scale;
        transform.localScale += new Vector3(newScale, newScale, newScale);
    }

    protected override void Destroy(Collider2D collision)
    {
        transform.localScale = defaultSclate;

        base.Destroy(collision);
    }

    private PowerShotBulletData Data { get; set; }

}
