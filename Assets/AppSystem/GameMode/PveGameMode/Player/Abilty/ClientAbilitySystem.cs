using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;
using static AbilitiesLib;

public class ClientAbilitySystem : MonoBehaviour
{
    public void Init(AbilitiesLib abilitiesLib)
    {
        abilities = abilitiesLib.Abilities;
        foreach (var ability in abilities)
        {
            ability.Value.controller.Init(ability.Value);
        }
    }

    public void Terminate()
    {
        foreach (var ability in abilities)
        {
            ability.Value.controller?.Terminate();
        }
    }

    public Ability GetAbility(AbilityType type)
    {
        return abilities[type];
    }

    private Dictionary<AbilityType, Ability> abilities;
    
}