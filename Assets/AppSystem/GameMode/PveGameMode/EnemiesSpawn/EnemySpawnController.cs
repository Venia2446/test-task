using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public void SpawnEnemy(EnemyData inEnemyData, GameObject target)
    {
        var obj = Instantiate(inEnemyData.GetObj(), gameObject.GetComponent<Transform>().transform);
        obj.SetActive(true);
        obj.GetComponent<EnemyBase>().Init(target);
    }

}