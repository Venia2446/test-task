using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DifficultyPresetsLib;
using static Globals;

public class ClientHealthController : HealthControllerBase
{
    public delegate void HandleOnReceivedArmorDamage(float currentArmor);
    public delegate void HandleArmorRegen(float currentArmor);

    public event HandleOnReceivedArmorDamage OnReceivedArmorDamage;
    public event HandleArmorRegen OnArmorRegen;

    public override void Init(HealthDataBase data)
    {
        base.Init(data);

        var castedData = (ClientHealthData)data;
        MaxArmor = castedData.maxArmor;
        armorRegenValue = castedData.armorRegenValue;
        armorRegenRateMs = castedData.armorRegenRateMs;
        armorRegDelay = castedData.armorRegDelay;

        Armor = MaxArmor;
    }

    public override void Terminate()
    {
        StopAllCoroutines();

        base.Terminate();
    }

    public bool HasArmor
    {
        get { return Armor != 0; }
    }

    public float Armor
    {
        set { armor = value; }
        get { return armor; }
    }

    public float MaxArmor
    {
        set { maxArmor = value; }
        get { return maxArmor; }
    }

    public float MinArmor
    {
        get { return minArmor; }
    }

    public override void RegisterTakingDamage(float damage)
    {
        var remainingDamage = TryRegisterArmorDamage(damage);
        TryUpdateDeadState(TryRegisterHealthDamage(remainingDamage));
    }

    private float TryRegisterArmorDamage(float damage)
    {
        if (!HasArmor)
        {
            return damage;
        }

        float remainingDamage = 0f;
        var newArmorValue = Armor - damage;

        if (newArmorValue == Armor)
        {
            return remainingDamage;
        }

        if (newArmorValue < MinArmor)
        {
            remainingDamage = Mathf.Abs(newArmorValue);
        }

        Armor = Mathf.Clamp(newArmorValue, MinArmor, MaxArmor);
        OnReceivedArmorDamage?.Invoke(Armor);
        FireRecieveDamage(StatType.ARMOR);

        return remainingDamage;
    }

    protected override void LocalHandleOnOnReceiveDamage()
    {
        base.LocalHandleOnOnReceiveDamage();

        audioSystem.PlayOneShot(AudioClipType.MELEE_HIT);
        StopRegetArmor();
        StartCoroutine(StartArmorRegen(armorRegDelay, armorRegenRateMs / 1000));
    }

    private IEnumerator StartArmorRegen(float delay, float frequency)
    {
        yield return new WaitForSeconds(delay);
        while (true)
        {
            yield return new WaitForSeconds(frequency);
            var newArmor = Mathf.Clamp(Armor + armorRegenValue, MinArmor, MaxArmor);
            if (Armor != newArmor)
            {
                Armor = newArmor;
                OnArmorRegen?.Invoke(Armor);
            }
        }
    }

    private void StopRegetArmor()
    {
        StopAllCoroutines();
    }

    private const float minArmor = 0;
    private float maxArmor;

    private float armor;

    private float armorRegenValue;
    private float armorRegenRateMs;
    private float armorRegDelay;
}
