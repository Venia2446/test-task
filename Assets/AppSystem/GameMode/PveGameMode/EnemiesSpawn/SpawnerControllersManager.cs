using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;
using static DifficultyPresetsLib;

public class SpawnerControllersManager : MonoBehaviour
{
    [SerializeField] public GameObject[] spawners;

    public void Init(DiffcultyPreset inPreset, EnemiesStructLib enemiesStructLib, PlayerController playerController)
    {
        target = playerController.player;
        enemyDatas = enemiesStructLib.GetEnemiesDatas();

        foreach (var spawner in spawners)
        {
            spawnerControllers.Add(spawner.GetComponent<EnemySpawnController>());
        }

        StartCoroutine(StartSpawnLogic(inPreset.enemySpawnRateSec));
    }

    public void Terminate()
    {
        StopAllCoroutines();
    }

    private void OnDestroy()
    {
        Terminate();
    }

    private IEnumerator StartSpawnLogic(float frequency)
    {
        Spawn();

        while (true)
        {
            yield return new WaitForSeconds(frequency);
            Spawn();
        }
    }

    public void Spawn()
    {
        var rdnSpawnerIdx = Random.Range(0, spawnerControllers.Count - 1);
        var rdnEnemyType = (EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);
        spawnerControllers[rdnSpawnerIdx].SpawnEnemy(enemyDatas[rdnEnemyType], target);
    }

    private List<EnemySpawnController> spawnerControllers = new List<EnemySpawnController>();
    private Dictionary<EnemyType, EnemyData> enemyDatas;
    private GameObject target;

}
