using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using static Globals;
using static DifficultyPresetsLib;

public class SelectDifficlultyButtonController : ChangeSceneButtonController
{
    public DifficultyPresetType requestedDiffcilulty;

    protected override void HandleOnClick()
    {
        AppSystemClient.Instance.SetDifficulty(requestedDiffcilulty);

        base.HandleOnClick();
    }

}
