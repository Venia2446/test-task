using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class BulletControllerBase : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidbody;

    public void Init(BulletDataBase bulletData)
    {
        if (IsInited)
        {
            CacheData(bulletData);
            StartMovement(bulletData.Angle);
            return;
        }

        IsInited = true;
        InitInner(bulletData);
    }

    public virtual void InitInner(BulletDataBase bulletData)
    {
        GameMode = (PveGameMode)AppSystemClient.Instance.GameMode;
        actions.Add(BulletAction.DESTROY, Destroy);
        actions.Add(BulletAction.HIT_AND_DESTROY, RegisterAndDestroy);
        actions.Add(BulletAction.HIT, RegisterDamage);

        CacheData(bulletData);
        StartMovement(bulletData.Angle);
    }

    protected virtual void CacheData(BulletDataBase bulletData)
    {
        BulletsPool = GameMode.bulletsPool;
        BulletStruct = bulletData.BulletStruct;
        Damage = bulletData.Damage;
    }

    protected virtual void StartMovement(Quaternion angle)
    {
        rigidbody.velocity = angle * Vector2.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var action = DetermineAction(collision);
        if (action == BulletAction.NONE)
        {
            return;
        }

        if (!actions.TryGetValue(action, out Action outAction))
        {
            return;
        }

        actions[action].Invoke(collision);
    }

    protected virtual void RegisterDamage(Collider2D collision)
    {
        
    }

    protected virtual void Destroy(Collider2D collision)
    {
        BulletsPool.ReturnToPool(BulletStruct.type, gameObject);
    }

    private void RegisterAndDestroy(Collider2D collision)
    {
        RegisterDamage(collision);
        Destroy(collision);
    }

    private BulletAction DetermineAction(Collider2D collision) 
    {
       return BulletStruct.GetTriggerAction(collision.gameObject.tag.ToString());
    }

    protected float Damage { get; set; }

    private delegate void Action(Collider2D collision);
    private new Dictionary<BulletAction, Action> actions = new Dictionary<BulletAction, Action>();
    private BulletStruct BulletStruct { get; set; }
    private BulletsPool BulletsPool { get; set; }

    private PveGameMode GameMode { get; set; }
    private bool IsInited { get; set; }
    

}