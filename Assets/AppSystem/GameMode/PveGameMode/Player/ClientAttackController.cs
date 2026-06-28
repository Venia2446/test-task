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
        audioSystem = inAudioSystem;
        bulletStruct = bulletStructLib.GetBulletStruct(BulletType.SMALL);
        clientDamage = preset.clientDamage;
        camera = Camera.main;
        isAttackReady = true;
    }

    public void Terminate()
    {
        isAttackReady = false;
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
        var worldMousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        return Utils.CalculateRotationAngleRad(worldMousePos, objPos);
    }

    private void Update()
    {
        var rad = GetRadToMouseTarget();
        var angle = Quaternion.Euler(0f, 0f, rad);
        gunTransform.rotation = angle;
        
        if (!isAttackReady)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isAttackReady = false;
            audioSystem.PlayOneShot(AudiolClipType.PLAYER_SHOOTING);
            for (int i = 0; i < bulletsCount; i ++)
            {
                var spreadedRad = rad - Random.Range(-spreadeAngle, spreadeAngle);
                var spreadedAngle = Quaternion.Euler(0f, 0f, spreadedRad);
                StartCoroutine(BuildBullet(bulletStruct, spawnerTransform.position, spreadedAngle));
            }

            StartCoroutine(ResetAttackReadyFlag(shootingDelay));
        }
    }

    private IEnumerator BuildBullet(BulletStruct bulletStructure, Vector3 position, Quaternion angle)
    {
        var obj = Instantiate(bulletStructure.stuct, position, angle);
        var bulletData = new BulletDataBase();
        bulletData.angle = angle;
        bulletData.damage = clientDamage;
        bulletData.bulletStruct = bulletStruct;

        var bulletContr = obj.GetComponent<ClientBulletController>();
        bulletContr.Init(bulletData);

        yield return null;
    }

    private IEnumerator ResetAttackReadyFlag(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAttackReady = true;
    }

    private BulletStruct bulletStruct;
    private Camera camera;
    private AudioSystem audioSystem;

    private float clientDamage;
    private bool isAttackReady = false;

}
