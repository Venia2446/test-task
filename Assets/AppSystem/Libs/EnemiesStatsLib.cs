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
            enemiesStatsPresets.Add(preset.EnemyType, preset);
        }
    }

    public EnemyStatsPreset GetPreset(EnemyType type)
    {
        return enemiesStatsPresets[type];
    }

    [System.Serializable]
    public class EnemyStatsPreset
    {
        public EnemyType EnemyType { get; set; }
        public float Hp { get; set; }
        public float Speed { get; set; }
        public float MaxSpeed { get; set; }
        public float KeepDistance { get; set; }
        public float Damage { get; set; }
        public float Acceleration { get; set; }
        public float AttackFrequency { get; set; }
        public float ScoreValue { get; set; }
    }

    private Dictionary<EnemyType, EnemyStatsPreset> enemiesStatsPresets = new Dictionary<EnemyType, EnemyStatsPreset>();

}
