using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMenuController : MonoBehaviour
{
    public GameObject panel;
    
    private void Awake()
    {
        appSystemClient = AppSystemClient.Instance;
        pauseManager = appSystemClient.PauseManager;
        gameEvents = GameEvents.Instance;
        gameEvents.SubscribeGameEvent(Init, Terminate);
    }

    private void OnDestroy()
    {
        gameEvents?.UnsubscribeGameEvents(Init, Terminate);
        Terminate();
    }

    private void Init()
    {
        clientHealthController = ((PveGameMode)appSystemClient.GameMode).playerController.clientHealthController;
        clientHealthController.OnDeath += OpenMainMenu;
    }

    private void Terminate()
    {
        clientHealthController.OnDeath -= OpenMainMenu;
    }
    private void Update()
    {
        if (clientHealthController.IsDead)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isMainMenuEnabled)
            {
                CloseMainMenu();
            }
            else
            {
                OpenMainMenu();
            }
        }
    }
    private void OpenMainMenu()
    {
        pauseManager.TryPauseGame();
        panel.SetActive(true);
        isMainMenuEnabled = true;
    }

    private void CloseMainMenu()
    {
        panel.SetActive(false);
        pauseManager.TryUnpaseGame();
        isMainMenuEnabled = false;
    }

    private PauseManager pauseManager;
    private bool isMainMenuEnabled;
    private GameEvents gameEvents;
    private AppSystemClient appSystemClient;
    private ClientHealthController clientHealthController;

}
