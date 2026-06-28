using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;
using static BulletsStructLib;

public class BulletControllerBase : MonoBehaviour
{
    public float speed;

    public virtual void Init(BulletDataBase bulletData)
    {
        bulletStruct = bulletData.bulletStruct;
        damage = bulletData.damage;
        rigBody = gameObject.GetComponent<Rigidbody2D>();
        rigBody.velocity = bulletData.angle * Vector2.right * speed;

        actions.Add(BulletAction.DESTROY, Destroy);
        actions.Add(BulletAction.HIT_AND_DESTROY, RegisterAndDestroy);
        actions.Add(BulletAction.HIT, RegisterDamage);
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

    private void Destroy(Collider2D collision)
    {
        Object.Destroy(gameObject);
    }

    private void RegisterAndDestroy(Collider2D collision)
    {
        RegisterDamage(collision);
        Destroy(collision);
    }

    private BulletAction DetermineAction(Collider2D collision) 
    {
       return bulletStruct.GetTriggerAction(collision.gameObject.tag.ToString());
    }

    protected float damage;
    protected BulletStruct bulletStruct;

    private new Dictionary<BulletAction, Action> actions = new Dictionary<BulletAction, Action>();
    private delegate void Action(Collider2D collision);
    private Rigidbody2D rigBody;
}