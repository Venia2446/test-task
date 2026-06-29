using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Globals;

public class BarControllerBase : MonoBehaviour
{
    private void Awake()
    {
        appSystem = AppSystemClient.Instance;
        gameEvents = GameEvents.Instance;
        gameEvents.SubscribeGameEvent(Init, Terminate);
    }
    private void Init() 
    {
        playerController = ((PveGameMode)appSystem.GameMode).playerController;
        image = gameObject.GetComponent<Image>();
        InitInner();
        SetLerpRange();
        Subscribe();
    }

    public void Terminate()
    {
        gameEvents?.UnsubscribeGameEvents(Init, Terminate);
        Unsubscribe();
    }

    private void OnDestroy()
    {

        Terminate();
    }

    protected virtual void InitInner()
    {

    }

    protected virtual void Subscribe()
    {

    }

    protected virtual void Unsubscribe()
    {

    }

    protected void UpdateBar(float value)
    {
        float lerp1 = Mathf.InverseLerp(minValue, maxValue, value);
        float lerp2 = Mathf.Lerp(0, maxFill, lerp1);
        image.fillAmount = lerp2;
    }

    protected virtual void SetLerpRange() { }

    protected PlayerController playerController;
    protected AppSystemClient appSystem;
    protected Image image;

    protected float minValue;
    protected float maxValue;

    private GameEvents gameEvents;

}
