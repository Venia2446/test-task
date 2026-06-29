using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;
using static AbilitiesLib;

public class ClientAbilitySystem : MonoBehaviour
{
    public void Init(AbilitiesLib abilitiesLib)
    {
        Abilities = abilitiesLib.Abilities;
        foreach (var ability in Abilities)
        {
            ability.Value.Controller.Init(ability.Value);
        }
    }

    public void Terminate()
    {
        foreach (var ability in Abilities)
        {
            ability.Value.Controller?.Terminate();
        }
    }

    public Ability GetAbility(AbilityType type)
    {
        return Abilities[type];
    }

    private Dictionary<AbilityType, Ability> Abilities { get; set; }
    
}