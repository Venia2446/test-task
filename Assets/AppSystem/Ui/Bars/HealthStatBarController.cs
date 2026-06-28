using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStatBarController : StatBarController
{
    protected override void Subscribe()
    {
        base.Subscribe();

        clientHealthController.OnReceivedHealthDamage += UpdateBar;
        UpdateBar(clientHealthController.Health);
    }

    protected override void Unsubscribe()
    {
        clientHealthController.OnReceivedHealthDamage -= UpdateBar;

        base.Unsubscribe();
    }

    protected override void SetLerpRange()
    {
        base.SetLerpRange();

        minValue = clientHealthController.MinHealth;
        maxValue = clientHealthController.MaxHealth;
    }
}
