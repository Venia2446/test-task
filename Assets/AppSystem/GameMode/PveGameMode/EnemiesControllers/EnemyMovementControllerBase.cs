using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemiesStatsLib;
using static Utils;

public class EnemyMovementControllerBase : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public Transform localTransform;

    public void Init(GameObject inTarget, EnemyStatsPreset enemyStatsPreset)
    {
        Speed = enemyStatsPreset.speed;
        KeepingDistance = enemyStatsPreset.keepDistance;
        Acceletation = enemyStatsPreset.acceleration;
        MaxSpeed = enemyStatsPreset.maxSpeed;
        TargetTransform = inTarget.GetComponent<Transform>();
    }

    private void Update()
    {
        if (IsMoving())
        {
            StartMoving();
        }
        else
        {
            StopMoving();
        }
    }

    private void StartMoving()
    {
        Speed = Mathf.MoveTowards(Speed, MaxSpeed, Acceletation);
        var angle = localTransform.rotation = CalculateRotationAngle(TargetTransform.position, localTransform.position);;
        rigidbody.velocity = angle * Vector2.right * Speed;
    }

    private void StopMoving()
    {
        rigidbody.velocity = new Vector2();
    }
    private bool IsMoving()
    {
        float distance = Vector3.Distance(localTransform.position, TargetTransform.position);
        return (Mathf.Approximately(distance, KeepingDistance) || distance > KeepingDistance);
    }

    private Transform TargetTransform { get; set; }

    private float Speed { get; set; }
    private float KeepingDistance { get; set; }
    private float Acceletation { get; set; }
    private float MaxSpeed { get; set; }
}
