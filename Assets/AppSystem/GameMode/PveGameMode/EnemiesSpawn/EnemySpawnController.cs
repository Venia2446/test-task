using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public Transform spawnerTransform;

    public void SpawnEnemy(EnemyData inEnemyData, GameObject target)
    {
        var obj = Instantiate(inEnemyData.GetObj(), spawnerTransform);
        obj.SetActive(true);
        obj.GetComponent<EnemyBase>().Init(target);
    }

}