using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameModeBase : MonoBehaviour
{
    public event Action OnGameReady;
    public event Action OnGameEnded;

    private void Awake()
    {
        AppSystem = AppSystemClient.Instance;
        AppSystem.GameMode = this;
    }

    private void OnDestroy()
    {
        OnGameEnded?.Invoke();
        Terminate();
    }
    public virtual void Init(GameModeParams inParams) 
    {
        AppSystem.PauseManager.TryUnpaseGame();

        DiffcultyPreset = inParams.DiffPreset;
        InitGameLibs();
        InitGameSystems();
        OnGameReady?.Invoke();
    }

    protected virtual void Terminate()
    {
        AppSystem.GameMode = null;
    }

    public DiffcultyPreset DiffcultyPreset { get; protected set; }

    protected virtual void InitGameLibs() { }
    protected virtual void InitGameSystems() { }

    protected AppSystemClient AppSystem { get; set; }

}
