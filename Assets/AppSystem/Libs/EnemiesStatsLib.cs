using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class EnemiesStatsLib : MonoBehaviour
{
    public EnemyStatsPreset[] enemyPresets;

    public void Init()
    {
        foreach (var preset in enemyPresets)
        {
            enemiesStatsPresets.Add(preset.enemyType, preset);
        }
    }

    public EnemyStatsPreset GetPreset(EnemyType type)
    {
        return enemiesStatsPresets[type];
    }

    [System.Serializable]
    public class EnemyStatsPreset
    {
        public EnemyType enemyType;
        public float hp;
        public float speed;
        public float maxSpeed;
        public float keepDistance;
        public float damage;
        public float acceleration;
        public float attackFrequency;
        public float scoreValue;
    }

    private Dictionary<EnemyType, EnemyStatsPreset> enemiesStatsPresets = new Dictionary<EnemyType, EnemyStatsPreset>();

}
