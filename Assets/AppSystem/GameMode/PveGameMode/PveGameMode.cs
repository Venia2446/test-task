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

    public AudioSystem audioSystem;

    protected override void CreateGameLibs()
    {
        base.CreateGameLibs();

        enemiesStructLib.Init();
        bulletsStructLib.Init();
        abilitiesLib.Init();
        soundsLib.Init();
    }

    protected override void CreateGameSystems()
    {
        base.CreateGameSystems();

        audioSystem.Init(soundsLib);
        scoreManager.Init(DiffcultyPreset);
        playerController.Init(DiffcultyPreset, bulletsStructLib, abilitiesLib, audioSystem);
        spawnerManager.Init(DiffcultyPreset, enemiesStructLib, playerController);
    }

    protected override void Terminate()
    {
        audioSystem?.Terminate();
        spawnerManager?.Terminate();
        playerController?.Terminate();

        base.Terminate();
    }

}
