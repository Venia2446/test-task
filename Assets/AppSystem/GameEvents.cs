using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents
{
    public static GameEvents Instance { get; private set; }

    private event Action OnGameReady;
    private event Action OnGameEnded;

    public GameEvents()
    {
        Instance = this;
    }

    public void SubscribeGameEvent(Action cbReady = null, Action cbEnded = null)
    {
        if (cbEnded != null)
        {
            OnGameEnded += cbEnded;
        }

        if (cbReady != null)
        {
            OnGameReady += cbReady;
            if (IsGameReady)
            {
                cbReady.Invoke();
            }
        }
    }

    public void UnsubscribeGameEvents(Action cbReady = null, Action cbEnded = null)
    {
        if (cbReady != null)
        {
            OnGameReady -= cbReady;
        }

        if (cbEnded != null)
        {
            OnGameEnded -= cbEnded;
        }
    }
   public void GameReady()
    {
        IsGameReady = true;
        OnGameReady?.Invoke();
    }

    public void GameEnded()
    {
        if (IsGameReady)
            IsGameReady = false;
        OnGameEnded?.Invoke();
    }

    public bool IsGameReady { get; private set; }

}
