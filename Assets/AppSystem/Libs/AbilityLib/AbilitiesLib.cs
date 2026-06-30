using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class AbilitiesLib : MonoBehaviour
{

    public Ability[] abilities;

    public void Init()
    {
        foreach (var ability in abilities)
        {
            abilityStuctures.Add(ability.type, ability);
        }
    }

    public Dictionary<AbilityType, Ability> Abilities
    {
        get { return abilityStuctures; }
    }

    public Ability GetAbility(AbilityType type)
    {
        return abilityStuctures[type];
    }

    private Dictionary<AbilityType, Ability> abilityStuctures = new Dictionary<AbilityType, Ability>();

}

[System.Serializable]
public class Ability
{

    public AbilityType type;
    public ClientAbilityControllerBase controller;
    public AbilityDataBase data;

}

