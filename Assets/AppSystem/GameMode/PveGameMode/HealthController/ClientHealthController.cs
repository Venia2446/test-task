using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        MaxArmor = castedData.MaxArmor;
        ArmorRegenValue = castedData.ArmorRegenValue;
        ArmorRegenRateMs = castedData.ArmorRegenRateMs;
        ArmorRegDelay = castedData.ArmorRegenDelay;

        Armor = MaxArmor;
    }

    public override void Terminate()
    {
        StopAllCoroutines();

        base.Terminate();
    }

    public float MinArmor
    {
        get { return minArmor; }
    }
    public bool HasArmor
    {
        get { return Armor != 0; }
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

        AudioSystem.PlayOneShot(AudioClipType.MELEE_HIT);
        StopRegetArmor();
        StartCoroutine(StartArmorRegen(ArmorRegDelay, ArmorRegenRateMs / 1000));
    }

    private IEnumerator StartArmorRegen(float delay, float frequency)
    {
        yield return new WaitForSeconds(delay);
        while (true)
        {
            yield return new WaitForSeconds(frequency);
            var newArmor = Mathf.Clamp(Armor + ArmorRegenValue, MinArmor, MaxArmor);
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
    public float MaxArmor { get; private set; }
    public float Armor { get; private set; }
    public float ArmorRegenValue { get; private set; }
    public float ArmorRegenRateMs { get; private set; }
    public float ArmorRegDelay { get; private set; }
}
