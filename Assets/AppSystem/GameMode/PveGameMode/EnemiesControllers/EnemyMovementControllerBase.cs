using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemiesStatsLib;
using static Utils;

public class EnemyMovementControllerBase : MonoBehaviour
{
    public void Init(GameObject inTarget, EnemyStatsPreset enemyStatsPreset)
    {
        Speed = enemyStatsPreset.Speed;
        KeepingDistance = enemyStatsPreset.KeepDistance;
        Acceletation = enemyStatsPreset.Acceleration;
        MaxSpeed = enemyStatsPreset.MaxSpeed;
        RigBody = gameObject.GetComponent<Rigidbody2D>();
        LocalTransform = gameObject.GetComponent<Transform>();
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
        var angle = LocalTransform.rotation = CalculateRotationAngle(TargetTransform.position, LocalTransform.position);;
        RigBody.velocity = angle * Vector2.right * Speed;
    }

    private void StopMoving()
    {
        RigBody.velocity = new Vector2();
    }
    private bool IsMoving()
    {
        float distance = Vector3.Distance(LocalTransform.position, TargetTransform.position);
        return (distance >= KeepingDistance);
    }

    private Transform TargetTransform { get; set; }
    private Transform LocalTransform { get; set; }
    private Rigidbody2D RigBody { get; set; }
    private float Speed { get; set; }
    private float KeepingDistance { get; set; }
    private float Acceletation { get; set; }
    private float MaxSpeed { get; set; }
}
