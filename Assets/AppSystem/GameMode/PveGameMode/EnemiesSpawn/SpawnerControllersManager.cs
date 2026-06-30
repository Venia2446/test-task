using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;
using static DifficultyPresetsLib;

public class SpawnerControllersManager : MonoBehaviour
{
    public GameObject[] spawners;

    public void Init(DiffcultyPreset inPreset, EnemiesStructLib enemiesStructLib, PlayerController playerController)
    {
        Target = playerController.player;
        EnemyDatas = enemiesStructLib.GetEnemiesDatas();

        SpawnDelay = new WaitForSeconds(inPreset.EnemySpawnRateSec);
        
        foreach (var spawner in spawners)
        {
            spawnerControllers.Add(spawner.GetComponent<EnemySpawnController>());
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
        var rdnSpawnerIdx = Random.Range(0, spawnerControllers.Count - 1);
        var rdnEnemyType = (EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);
        spawnerControllers[rdnSpawnerIdx].SpawnEnemy(EnemyDatas[rdnEnemyType], Target);
    }

    private List<EnemySpawnController> spawnerControllers = new List<EnemySpawnController>();

    private WaitForSeconds SpawnDelay { get; set; }
    private Dictionary<EnemyType, EnemyData> EnemyDatas { get; set; }
    private GameObject Target { get; set; }

}
