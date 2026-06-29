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
    public float shootingDelay;
    public Transform spawnerTransform;
    public Transform gunTransform;

    public int bulletsCount = 5;

    public void Init(DiffcultyPreset preset, BulletsStructLib bulletStructLib, AudioSystem inAudioSystem)
    {
        AudioSystem = inAudioSystem;
        BulletStruct = bulletStructLib.GetBulletStruct(BulletType.SMALL);
        ClientDamage = preset.ClientDamage;
        Camera = Camera.main;
        IsAttackReady = true;
    }

    public void Terminate()
    {
        IsAttackReady = false;
        StopAllCoroutines();
    }

    private void OnDestroy()
    {
        Terminate();
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
        var rad = GetRadToMouseTarget();
        var angle = Quaternion.Euler(0f, 0f, rad);
        gunTransform.rotation = angle;
        
        if (!IsAttackReady)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            IsAttackReady = false;
            AudioSystem.PlayOneShot(AudioClipType.PLAYER_SHOOTING);
            for (int i = 0; i < bulletsCount; i ++)
            {
                var spreadedRad = rad - Random.Range(-spreadeAngle, spreadeAngle);
                var spreadedAngle = Quaternion.Euler(0f, 0f, spreadedRad);
                StartCoroutine(BuildBullet(BulletStruct, spawnerTransform.position, spreadedAngle));
            }

            StartCoroutine(ResetAttackReadyFlag(shootingDelay));
        }
    }

    private IEnumerator BuildBullet(BulletStruct bulletStructure, Vector3 position, Quaternion angle)
    {
        var obj = Instantiate(bulletStructure.gameObj, position, angle);
        var bulletData = new BulletDataBase();
        bulletData.Angle = angle;
        bulletData.Damage = ClientDamage;
        bulletData.BulletStruct = BulletStruct;

        var bulletContr = obj.GetComponent<ClientBulletController>();
        bulletContr.Init(bulletData);

        yield return null;
    }

    private IEnumerator ResetAttackReadyFlag(float delay)
    {
        yield return new WaitForSeconds(delay);
        IsAttackReady = true;
    }

    private BulletStruct BulletStruct { get; set; }
    private Camera Camera { get; set; }
    private AudioSystem AudioSystem { get; set; }
    private float ClientDamage { get; set; }
    private bool IsAttackReady { get; set; }

}
