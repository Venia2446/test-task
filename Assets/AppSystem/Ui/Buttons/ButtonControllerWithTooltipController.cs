using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControllerWithTooltipController : ButtonControllerBase
{
    public Vector2 shift;
    public string text;

    protected override void Init()
    {
        base.Init();

        tooltipController = TooltipController.Instance;
    }

    protected override void HoverIn()
    {
        base.HoverIn();

        tooltipController.RequestShowTooltip(text, shift);
    }

    protected override void HoverOut()
    {
        tooltipController.RequestHideTooltip();

        base.HoverOut();
    }

    private TooltipController tooltipController;
}
