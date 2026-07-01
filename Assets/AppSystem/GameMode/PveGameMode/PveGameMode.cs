using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;
public class PveGameMode : GameModeBase
{
    public BulletsStructLib bulletsStructLib;
    public EnemiesStructLib enemiesStructLib;
    public AbilitiesLib abilitiesLib;
    public SoundsLib soundsLib;

    public SpawnerControllersManager spawnerManager;
    public PlayerController playerController;
    public ScoreManager scoreManager;
    public EnemiesPool enemiesPool;
    public BulletsPool bulletsPool;

    public AudioSystem audioSystem;


    protected override void InitGameLibs()
    {
        base.InitGameLibs();

        enemiesStructLib.Init();
        bulletsStructLib.Init();
        abilitiesLib.Init();
        soundsLib.Init();
    }

    protected override void InitGameSystems()
    {
        base.InitGameSystems();

        bulletsPool.Init(bulletsStructLib);
        enemiesPool.Init(enemiesStructLib);
        audioSystem.Init(soundsLib);
        scoreManager.Init(DiffcultyPreset);
        playerController.Init(DiffcultyPreset, bulletsStructLib, abilitiesLib, audioSystem, bulletsPool);
        spawnerManager.Init(DiffcultyPreset, enemiesStructLib, playerController, enemiesPool);
    }

    protected override void Terminate()
    {
        audioSystem?.Terminate();
        spawnerManager?.Terminate();
        playerController?.Terminate();

        base.Terminate();
    }

}
