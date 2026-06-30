using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class BulletControllerBase : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidbody;

    public virtual void Init(BulletDataBase bulletData)
    {
        BulletStruct = bulletData.BulletStruct;
        Damage = bulletData.Damage;
        rigidbody.velocity = bulletData.Angle * Vector2.right * speed;

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
       return BulletStruct.GetTriggerAction(collision.gameObject.tag.ToString());
    }

    protected float Damage { get; set; }

    private delegate void Action(Collider2D collision);
    private new Dictionary<BulletAction, Action> actions = new Dictionary<BulletAction, Action>();
    private BulletStruct BulletStruct { get; set; }

}