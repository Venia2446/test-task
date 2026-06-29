using UnityEngine;
using static BulletsStructLib;
public class BulletDataBase
{
    public Quaternion Angle { get; set; } 
    public float Damage { get; set; }
    public BulletStruct BulletStruct { get; set; }
    public float Speed { get; set; }

}
