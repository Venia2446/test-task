using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BulletsStructLib;
using static AbilitiesLib;

public class PowerShotAbilityController : ClientAbilityControllerBase
{
    public Transform spawn;

    public delegate void HandleOnChargeUpdated(float charge);
    public event HandleOnChargeUpdated OnChargeUpdated;

    public override void Init(AbilitiesLib.Ability inAbility)
    {
        base.Init(inAbility);

        var gameMode = (PveGameMode)AppSystemClient.Instance.GameMode;
        clientAttackController = gameMode.playerController.clientAttackController;
        bulletStruct = gameMode.bulletsStructLib.GetBulletStruct(Globals.BulletType.POWER_SHOT);
        castedData = (ChargedAbilityData)data;
    }

    protected override void Activate()
    {
        canActivate = false;
        isCharging = true;
        FireAbilityActivated();
    }

    protected override void UpdateInner()
    {
        base.UpdateInner();

        if (isCharging)
        {
            ChargeUp();
            if (charge >= castedData.maxCharge)
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
            if (isCharging)
            {
                StopCharge();
            }
        }
    }

    private void StopCharge()
    {
        isCharging = false;
 
        var angle = clientAttackController.GetAngleToMouseTarget();
        StartCoroutine(BuildBullet(bulletStruct, spawn.position, angle, castedData.damage * charge));

        charge = 0;
        OnChargeUpdated?.Invoke(charge);
        StartCooldown();
    }

    private IEnumerator BuildBullet(BulletStruct bulletStructure, Vector3 position, Quaternion angle, float damage)
    {
        var obj = Instantiate(bulletStructure.stuct, position, angle);
        var bulletData = new PowerShotBulletData();
        bulletData.angle = angle;
        bulletData.damage = damage;
        bulletData.bulletStruct = bulletStruct;
        bulletData.charge = charge;

        var bulletController = obj.GetComponent<PowerShotBulletController>();
        bulletController.Init(bulletData);

        yield return null;
    }

    private void ChargeUp()
    {
        charge += castedData.chargeUp;
        OnChargeUpdated?.Invoke(charge);
    }

    public float Charge
    {
        get { return charge; }
    }

    private BulletStruct bulletStruct;
    private ClientAttackController clientAttackController;
    private ChargedAbilityData castedData;

    private float charge;
    private bool isCharging;

}
