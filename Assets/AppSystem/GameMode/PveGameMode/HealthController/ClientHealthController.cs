using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static Globals;

public class ClientHealthController : HealthControllerBase
{

    public event Action<float> OnReceivedArmorDamage;
    public event Action<float> OnArmorRegen;

    public override void Init(HealthDataBase data)
    {
        base.Init(data);

        var castedData = (ClientHealthData)data;
        MaxArmor = castedData.MaxArmor;
        ArmorRegenValue = castedData.ArmorRegenValue;
        ArmorRegenRateMs = castedData.ArmorRegenRateMs;
        ArmorRegDelay = castedData.ArmorRegenDelay;

        Armor = MaxArmor;

        RegenDelay = new WaitForSeconds(ArmorRegDelay);
        RegenFrequency = new WaitForSeconds(ArmorRegenRateMs / 1000);
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
        get { return !Mathf.Approximately(Armor, 0); }
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

        if (Mathf.Approximately(newArmorValue, Armor))
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

    protected override void LocalHandleOnReceiveDamage()
    {
        base.LocalHandleOnReceiveDamage();

        AudioSystem.PlayOneShot(AudioClipType.MELEE_HIT);
        StopRegetArmor();
        StartCoroutine(StartArmorRegen());
    }

    private IEnumerator StartArmorRegen()
    {
        yield return RegenDelay;
        while (true)
        {
            yield return RegenFrequency;
            var newArmor = Mathf.Clamp(Armor + ArmorRegenValue, MinArmor, MaxArmor);
            if (!Mathf.Approximately(Armor, newArmor))
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

    public float MaxArmor { get; private set; }
    public float Armor { get; private set; }
    public float ArmorRegenValue { get; private set; }
    public float ArmorRegenRateMs { get; private set; }
    public float ArmorRegDelay { get; private set; }

    private const float minArmor = 0;
    private WaitForSeconds RegenDelay { get; set; }
    private WaitForSeconds RegenFrequency { get; set; }

}
