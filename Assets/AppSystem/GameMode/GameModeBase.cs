using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DifficultyPresetsLib;
using static GameModeBuilder;

public class GameModeBase : MonoBehaviour
{
    public delegate void HandleOnGameReady();
    public delegate void HandleOnGameEnded();

    public event HandleOnGameReady OnGameReady;
    public event HandleOnGameEnded OnGameEnded;

    private void Awake()
    {
        appSystem = AppSystemClient.Instance;
        appSystem.GameMode = this;
    }

    private void OnDestroy()
    {
        Terminate();
        OnGameEnded?.Invoke();
    }
    public virtual void Init(DiffcultyPreset inDiffPreset) 
    {
        diffPreset = inDiffPreset;
        CreateGameLibs();
        CreateGameSystems();
        appSystem.PauseManager.TryUnpaseGame();
        OnGameReady?.Invoke();
    }

    protected virtual void Terminate()
    {
        appSystem.GameMode = null;
    }

    public DiffcultyPreset DiffcultyPreset
    {
        get { return diffPreset;  }
    }
    protected virtual void CreateGameLibs() { }
    protected virtual void CreateGameSystems() { }
    protected AppSystemClient appSystem;
    private DiffcultyPreset diffPreset;
}
