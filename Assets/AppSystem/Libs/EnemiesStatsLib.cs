using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class EnemiesStatsLib : MonoBehaviour
{
    [SerializeField] public EnemyStatsPreset[] enemyPresets;

    public void Init()
    {
        foreach (var preset in enemyPresets)
        {
            enemiesStatsPresets.Add(preset.enemyType, preset);
        }
    }

    public EnemyStatsPreset GetPreset(enemyType type)
    {
        return enemiesStatsPresets[type];
    }

    [System.Serializable]
    public struct EnemyStatsPreset
    {
        public enemyType enemyType;
        public float hp;
        public float speed;
        public float maxSpeed;
        public float keepDistance;
        public float damage;
        public float acceleration;
        public float attackFrequency;
        public float scoreValue;
    }

    private Dictionary<enemyType, EnemyStatsPreset> enemiesStatsPresets = new Dictionary<enemyType, EnemyStatsPreset>();

}
