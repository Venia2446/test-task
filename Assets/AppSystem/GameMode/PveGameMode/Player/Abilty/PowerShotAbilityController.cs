using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerShotAbilityController : ClientAbilityControllerBase
{
    public Transform spawn;
    public Transform defaultAbilityTransform;

    public event Action<float> OnChargeUpdated;

    public override void Init(Ability inAbility)
    {
        base.Init(inAbility);

        var gameMode = (PveGameMode)AppSystemClient.Instance.GameMode;
        ClientAttackController = gameMode.playerController.clientAttackController;
        BulletsPool = gameMode.bulletsPool;
        BulletStruct = gameMode.bulletsStructLib.GetBulletStruct(Globals.BulletType.POWER_SHOT);

        CastedData = (ChargedAbilityData)Data;
    }

    protected override void Activate()
    {
        CanActivate = false;
        IsCharging = true;
        FireAbilityActivated();
    }

    protected override void UpdateInner()
    {
        base.UpdateInner();

        if (IsCharging)
        {
            ChargeUp();
            if (Charge >= CastedData.maxCharge)
            {
                StopCharge();
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            TryActivate();
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            if (IsCharging)
            {
                StopCharge();
            }
        }
    }
    private void StopCharge()
    {
        IsCharging = false;
 
        var angle = ClientAttackController.GetAngleToMouseTarget();
        BuildBullet(spawn.position, angle, CastedData.damage * Charge);

        Charge = 0;
        OnChargeUpdated?.Invoke(Charge);
        StartCooldown();
    }

    private void BuildBullet(Vector3 position, Quaternion angle, float damage)
    {
        var bulletData = new PowerShotBulletData();
        bulletData.Angle = angle;
        bulletData.Damage = damage;
        bulletData.BulletStruct = BulletStruct;
        bulletData.Charge = Charge;

        var obj = BulletsPool.GetFromPool(Globals.BulletType.POWER_SHOT, position, angle);
        var bulletController = obj.GetComponent<PowerShotBulletController>();
        bulletController.Init(bulletData);
    }

    private void ChargeUp()
    {
        Charge += CastedData.chargeUp;
        OnChargeUpdated?.Invoke(Charge);
    }

    public float Charge { get; private set; }

    private BulletStruct BulletStruct { get; set; }
    private ClientAttackController ClientAttackController { get; set; }
    private ChargedAbilityData CastedData { get; set; }
    private BulletsPool BulletsPool { get; set; }
    private bool IsCharging { get; set; }

}
