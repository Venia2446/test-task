using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Globals;

public class AppSystemClient : MonoBehaviour
{
    public DifficultyPresetsLib diffLib;
    public EnemiesStatsLib enemyStatsLib;
    public LoadingScreenController loadingScreenController;

    public delegate void HandleOnAppSystemCreated();
    public event HandleOnAppSystemCreated OnCreated;
    public static AppSystemClient Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        InitLibs();
        BuildGlobalSystems();
        InitGlobalSystems();

        IsCreated = true;
        OnCreated?.Invoke();

        loadingScreenController.LoadScene(SceneType.TITLE.ToString());
        DontDestroyOnLoad(this);
    }

    private void InitLibs()
    {
        diffLib.Init();
        enemyStatsLib.Init();
    }

    private void BuildGlobalSystems()
    {
        GameEvents = new GameEvents();
        PauseManager = new PauseManager();
        GameModeBuider = new GameModeBuilder(diffLib);
    }

    private void InitGlobalSystems()
    {
        GameModeBuider.Init();
    }

    public void SubscribeGameMode(GameModeBase inGameMode)
    {
        ActiveGameMode = inGameMode;
        ActiveGameMode.OnGameReady += FireGameReady;
        ActiveGameMode.OnGameEnded += FireGameEnded;
        GameModeBuider.BuildGameMode(ActiveGameMode);
    }
    public void UnsubsribeGameMode()
    {
        if (ActiveGameMode == null)
        {
            return;
        }

        ActiveGameMode.OnGameReady -= FireGameReady;
        ActiveGameMode.OnGameEnded -= FireGameEnded;
    }

    public void SetDifficulty(DifficultyPresetType type)
    {
        GameModeBuider.SetGameModeStartParams(type);
    }

    public GameModeBase GameMode
    {
        set
        {
            if (value == null)
            {
                UnsubsribeGameMode();
            }
            else
            {
                SubscribeGameMode(value);
            }
        }
        get { return ActiveGameMode; }
    }

    private void FireGameReady()
    {
        GameEvents.GameReady();
    }

    private void FireGameEnded()
    {
        GameModeBuider.Init();
        GameEvents.GameEnded();
    }

    public bool IsCreated { get; private set; }
    public PauseManager PauseManager { get; private set; }

    private GameModeBase ActiveGameMode { get; set; }
    private GameEvents GameEvents { get; set; }
    private GameModeBuilder GameModeBuider { get; set; }

}
