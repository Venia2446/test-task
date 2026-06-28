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

        BuildLibs();
        BuildGlobalSystems();

        isCreated = true;

        OnCreated?.Invoke();
        loadingScreenController.LoadScene(SceneType.TITLE.ToString());
        DontDestroyOnLoad(this);
    }

    private void BuildLibs()
    {
        diffLib.Init();
        enemyStatsLib.Init();
    }

    private void BuildGlobalSystems()
    {
        gameEvents = new GameEvents();
        pauseManager = new PauseManager();
        gameModeBuider = new GameModeBuilder(diffLib);
        gameModeBuider.CreateGameModeParams();
    }

    public void SubscribeGameMode(GameModeBase inGameMode)
    {
        activeGameMode = inGameMode;
        activeGameMode.OnGameReady += FireGameReady;
        activeGameMode.OnGameEnded += FireGameEnded;
        gameModeBuider.BuildGameMode(activeGameMode);

    }
    public void UnsubsribeGameMode()
    {
        if (activeGameMode == null)
        {
            return;
        }

        activeGameMode.OnGameReady -= FireGameReady;
        activeGameMode.OnGameEnded -= FireGameEnded;
    }

    public void SetDifficulty(DifficultyPresetType type)
    {
        gameModeBuider.SetGameModeStartParams(type);
    }

    public bool IsCreated
    {
        get { return isCreated; }
    }

    public PauseManager PauseManager
    {
        get { return pauseManager; }
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
        get { return activeGameMode; }
    }

    private void FireGameReady()
    {
        gameEvents.GameReady();
    }

    private void FireGameEnded()
    {
        gameModeBuider.CreateGameModeParams();
        gameEvents.GameEnded();
    }

    private GameModeBase activeGameMode;
    private PauseManager pauseManager;
    private GameEvents gameEvents;
    private GameModeBuilder gameModeBuider;

    private bool isCreated;

}
