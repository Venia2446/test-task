using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;
using static AbilitiesLib;

public class PowerShotAbilityBar : BarControllerBase
{
    protected override void InitInner()
    {
        base.InitInner();

        var ability = playerController.clientAbilitySystem.GetAbility(abilityType);
        powerShotAbilityController = (PowerShotAbilityController)ability.controller;
        abilityData = (ChargedAbilityData)ability.data;
    }

    protected PowerShotAbilityController powerShotAbilityController;
    protected ChargedAbilityData abilityData;

    private const AbilityType abilityType = AbilityType.POWER_SHOT;
    
}
