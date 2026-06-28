using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents
{
    public static GameEvents Instance { get; private set; }

    public delegate void HandleOnGameReady();
    private event HandleOnGameReady OnGameReady;

    public delegate void HandleOnGameEnded();
    private event HandleOnGameEnded OnGameEnded;

    public GameEvents()
    {
        Instance = this;
    }

    public void SubscribeGameEvent(HandleOnGameReady cbReady = null, HandleOnGameEnded cbEnded = null)
    {
        if (cbEnded != null)
        {
            OnGameEnded += cbEnded;
        }

        if (cbReady != null)
        {
            OnGameReady += cbReady;
            if (isGameReady)
            {
                cbReady.Invoke();
            }
        }
    }

    public void UnsubscribeGameEvents(HandleOnGameReady cbReady = null, HandleOnGameEnded cbEnded = null)
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
        isGameReady = true;
        OnGameReady.Invoke();
    }

    public void GameEnded()
    {
        if (isGameReady)
            isGameReady = false;
        OnGameEnded.Invoke();
      
    }
   public bool IsGameReady()
    {
        return isGameReady;
    }

    private bool isGameReady;
}
