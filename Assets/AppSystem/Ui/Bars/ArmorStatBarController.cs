using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorStatBarController : StatBarController
{
    protected override void InitInner()
    {
        base.InitInner();

        clientHealthController.OnArmorRegen += UpdateBar;
    }

    protected override void Subscribe()
    {
        base.Subscribe();

        clientHealthController.OnReceivedArmorDamage += UpdateBar;
        UpdateBar(clientHealthController.Armor);
    }

    protected override void Unsubscribe()
    {
        clientHealthController.OnReceivedArmorDamage -= UpdateBar;
        clientHealthController.OnArmorRegen -= UpdateBar;

        base.Unsubscribe();
    }

    protected override void SetLerpRange()
    {
        base.SetLerpRange();

        minValue = clientHealthController.MinArmor;
        maxValue = clientHealthController.MaxArmor;
    }
}
