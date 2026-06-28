using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCooldownBarController : PowerShotAbilityBar
{
    protected override void Subscribe()
    {
        base.Subscribe();

        powerShotAbilityController.OnCooldownUpdated += UpdateBar;
        UpdateBar(powerShotAbilityController.Cooldown);
    }

    protected override void Unsubscribe()
    {
        powerShotAbilityController.OnCooldownUpdated -= UpdateBar;

        base.Unsubscribe();
    }
    protected override void SetLerpRange()
    {
        base.SetLerpRange();

        minValue = abilityData.cooldown; 
        maxValue = 0;
    }

}
