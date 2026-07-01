using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class SpawnerControllersManager : MonoBehaviour
{
    public EnemySpawnController[] spawners;

    public void Init(DiffcultyPreset inPreset, EnemiesStructLib enemiesStructLib, PlayerController playerController, EnemiesPool enemiesPool)
    {
        Target = playerController.player;
        EnemyDatas = enemiesStructLib.GetEnemiesDatas();

        SpawnDelay = new WaitForSeconds(inPreset.EnemySpawnRateSec);
        
        foreach (var spawner in spawners)
        {
            spawner.Init(enemiesPool);
        }

        StartCoroutine(StartSpawnLogic());
    }

    public void Terminate()
    {
        StopAllCoroutines();
    }

    private IEnumerator StartSpawnLogic()
    {
        Spawn();

        while (true)
        {
            yield return SpawnDelay;
            Spawn();
        }
    }

    public void Spawn()
    {
        var rdnSpawnerIdx = Random.Range(0, spawners.Length - 1);
        var rdnEnemyType = (EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);
        spawners[rdnSpawnerIdx].SpawnEnemy(EnemyDatas[rdnEnemyType], Target);
    }

    private WaitForSeconds SpawnDelay { get; set; }
    private Dictionary<EnemyType, EnemyData> EnemyDatas { get; set; }
    private GameObject Target { get; set; }

}
