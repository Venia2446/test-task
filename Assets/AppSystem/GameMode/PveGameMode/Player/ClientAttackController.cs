using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;
using static Globals;
using static Utils;
using static BulletsStructLib;
using static DifficultyPresetsLib;

public class ClientAttackController : MonoBehaviour
{
    public float spreadeAngle = 15f;
    public float attackDelay = 1f;
    public Transform spawnerTransform;
    public Transform gunTransform;

    public int bulletsCount = 5;

    public void Init(DiffcultyPreset preset, BulletsStructLib bulletStructLib, AudioSystem inAudioSystem)
    {
        ResetAttackDelay = new WaitForSeconds(attackDelay);

        AudioSystem = inAudioSystem;
        BulletStruct = bulletStructLib.GetBulletStruct(BulletType.SMALL);
        ClientDamage = preset.ClientDamage;
        Camera = Camera.main;
        IsAttackReady = true;

        IsInited = true;
    }

    public void Terminate()
    {
        IsInited = false;
        IsAttackReady = false;
        StopAllCoroutines();
    }

    public Quaternion GetAngleToMouseTarget()
    {
        return Quaternion.Euler(0f, 0f, GetRadToMouseTarget());
    }

    public float GetRadToMouseTarget()
    {
        var objPos = gunTransform.position;
        var worldMousePos = Camera.ScreenToWorldPoint(Input.mousePosition);
        return Utils.CalculateRotationAngleRad(worldMousePos, objPos);
    }

    private void Update()
    {
        if (!IsInited)
        {
            return;
        }

        var angleRad = GetRadToMouseTarget();
        var angle = Quaternion.Euler(0f, 0f, angleRad);
        gunTransform.rotation = angle;
        
        if (!IsAttackReady)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            IsAttackReady = false;
            AudioSystem.PlayOneShot(AudioClipType.PLAYER_SHOOTING);
            StartCoroutine(BuildBullets(angleRad));
        }
    }

    private IEnumerator BuildBullets(float inAngleRad)
    {
        for (int i = 0; i < bulletsCount; i++)
        {
            var spreadedRad = inAngleRad - Random.Range(-spreadeAngle, spreadeAngle);
            var spreadedAngle = Quaternion.Euler(0f, 0f, spreadedRad);

            var obj = Instantiate(BulletStruct.gameObj, spawnerTransform.position, spreadedAngle);
            var bulletData = new BulletDataBase();
            bulletData.Angle = spreadedAngle;
            bulletData.Damage = ClientDamage;
            bulletData.BulletStruct = BulletStruct;

            var bulletContr = obj.GetComponent<ClientBulletController>();
            bulletContr.Init(bulletData);
        }

        yield return ResetAttackDelay;
        IsAttackReady = true;
    }

    private WaitForSeconds ResetAttackDelay { get; set; }
    private BulletStruct BulletStruct { get; set; }
    private Camera Camera { get; set; }
    private AudioSystem AudioSystem { get; set; }
    private float ClientDamage { get; set; }
    private bool IsAttackReady { get; set; }
    private bool IsInited { get; set; }

}
