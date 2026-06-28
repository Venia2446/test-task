using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemiesStatsLib;
using static Utils;

public class EnemyMovementControllerBase : MonoBehaviour
{
    public void Init(GameObject inTarget, EnemyStatsPreset enemyStatsPreset)
    {
        speed = enemyStatsPreset.speed;
        keepingDistance = enemyStatsPreset.keepDistance;
        acceletation = enemyStatsPreset.acceleration;
        maxSpeed = enemyStatsPreset.maxSpeed;
        rb = gameObject.GetComponent<Rigidbody2D>();
        localTransform = gameObject.GetComponent<Transform>();
        targetTransform = inTarget.GetComponent<Transform>();
    }

    private void Update()
    {
        if (IsMoving())
        {
            Start();
        }
        else
        {
            Stop();
        }
    }

    private void Start()
    {
        speed = Mathf.MoveTowards(speed, maxSpeed, acceletation);
        var angle = localTransform.rotation = CalculateRotationAngle(targetTransform.position, localTransform.position);;
        rb.velocity = angle * Vector2.right * speed;
    }

    private void Stop()
    {
        rb.velocity = new Vector2();
    }
    private bool IsMoving()
    {
        float distance = Vector3.Distance(localTransform.position, targetTransform.position);
        return (distance >= keepingDistance);
    }

    protected Transform targetTransform;
    protected Transform localTransform;
    protected Rigidbody2D rb;
    protected float speed;
    protected float keepingDistance;
    protected float acceletation;
    protected float maxSpeed;
}
