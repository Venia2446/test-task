using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGameButtonController : ButtonControllerBase
{
    protected override void HandleOnClick()
    {
        base.HandleOnClick();

        Application.Quit();
    }
}
