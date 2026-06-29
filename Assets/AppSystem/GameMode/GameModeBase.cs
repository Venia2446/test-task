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
        AppSystem = AppSystemClient.Instance;
        AppSystem.GameMode = this;
    }

    private void OnDestroy()
    {
        Terminate();
        OnGameEnded?.Invoke();
    }
    public virtual void Init(GameModeParams inParams) 
    {
        DiffcultyPreset = inParams.DiffPreset;
        InitGameLibs();
        InitGameSystems();
        OnGameReady?.Invoke();
    }

    protected virtual void Terminate()
    {
        AppSystem.GameMode = null;
    }

    protected virtual void InitGameLibs() { }
    protected virtual void InitGameSystems() { }

    public DiffcultyPreset DiffcultyPreset { get; protected set; }
    protected AppSystemClient AppSystem { get; set; }

}
