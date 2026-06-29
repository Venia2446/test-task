using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;
using static AbilitiesLib;


public class ChargeBarController : PowerShotAbilityBar
{

    protected override void Subscribe()
    {
        base.Subscribe();

        powerShotAbilityController.OnChargeUpdated += UpdateBar;
        UpdateBar(powerShotAbilityController.Charge);
    }

    protected override void Unsubscribe()
    {
        powerShotAbilityController.OnChargeUpdated -= UpdateBar;

        base.Unsubscribe();
    }

    protected override void SetLerpRange()
    {
        base.SetLerpRange();

        minValue = abilityData.MinCharge;
        maxValue = abilityData.MaxCharge;
    }

}
