using UnityEngine;

public class EnemyAttackDataBase : MonoBehaviour
{
    public ClientHealthController ClientHealthSystem { get; set; }
    public GameObject Target { get; set; }
    public float Damage { get; set; }
    public float AttackFrequency { get; set; } 

}
