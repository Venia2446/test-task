using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Globals;

public class ChangeSceneButtonController : ButtonControllerBase
{
    public SceneType targetScene = SceneType.NONE;

    protected override void HandleOnClick()
    {
        base.HandleOnClick();

        string scene;
        if (targetScene == SceneType.NONE)
        {
            scene = SceneManager.GetActiveScene().name;
        }
        else
        {
            scene = targetScene.ToString();
        }

        var appSystem = AppSystemClient.Instance;

        appSystem.loadingScreenController.LoadScene(scene);
    }

    protected LoadingScreenController loadingScreenController;
}
