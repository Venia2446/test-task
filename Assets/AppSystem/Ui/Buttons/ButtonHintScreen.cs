using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHintScreen : ButtonControllerBase
{
    public bool isOpenAction;
    public GameObject hintScreen;
    protected override void HandleOnClick()
    {
        base.HandleOnClick();

        hintScreen.SetActive(isOpenAction);
    }

}
