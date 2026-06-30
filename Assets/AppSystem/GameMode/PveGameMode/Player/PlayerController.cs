using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DifficultyPresetsLib;
using static BulletsStructLib;
using static Globals;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public ClientHealthController clientHealthController;
    public ClientMovementController clientMovementController;
    public ClientAttackController clientAttackController;
    public ClientAbilitySystem clientAbilitySystem;

    public void Init(DiffcultyPreset diffPreset, BulletsStructLib bulletStructLib, AbilitiesLib abilitiesLib, AudioSystem audioSystem)
    {
        clientHealthController.Init(CreateHealthData(diffPreset));
        clientAttackController.Init(diffPreset, bulletStructLib, audioSystem);
        clientAbilitySystem.Init(abilitiesLib);
    }

    public void Terminate()
    {
        clientAbilitySystem.Terminate();
        clientHealthController.Terminate();
        clientAttackController.Terminate();
    }

    protected ClientHealthData CreateHealthData(DiffcultyPreset diffPreset)
    {
        var healtData = new ClientHealthData();

        healtData.MaxHealth        = diffPreset.ClientMaxHealth;
        healtData.MaxArmor         = diffPreset.ClienMaxArmor;
        healtData.ArmorRegenValue  = diffPreset.ClientArmorRegenValue;
        healtData.ArmorRegenRateMs = diffPreset.ClientArmorRegenRateMs;
        healtData.ArmorRegenDelay  = diffPreset.ArmorRegenDelay;
        return healtData;
    }
}
