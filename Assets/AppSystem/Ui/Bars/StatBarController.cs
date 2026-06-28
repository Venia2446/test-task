using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBarController : BarControllerBase
{
    protected override void InitInner()
    {
        base.InitInner();

        clientHealthController = playerController.clientHealthController;
    }

    protected ClientHealthController clientHealthController;
}
