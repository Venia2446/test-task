using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;


public class DifficultyPresetsLib : MonoBehaviour
{
    public DiffcultyPreset[] difficultyPresets;

    public delegate void HandleOnLoaded();
    public event HandleOnLoaded OnLoaded;

    public void Init()
    {
        foreach (DiffcultyPreset preset in difficultyPresets)
        {
            presetsDict.Add(preset.Type, preset);
        }
    }

    public DiffcultyPreset GetPreset(DifficultyPresetType preset)
    {
        return presetsDict[preset];
    }

    private Dictionary<DifficultyPresetType, DiffcultyPreset> presetsDict = new Dictionary<DifficultyPresetType, DiffcultyPreset>();
}

[System.Serializable]
public class DiffcultyPreset
{
    public DifficultyPresetType Type { get; set; } = DifficultyPresetType.EASY;
    public float ClientMaxHealth { get; set; } = 100f;
    public float ClienMaxArmor { get; set; } = 150f;
    public float ClientArmorRegenValue { get; set; } = 0.3f;
    public float ClientDamage { get; set; } = 25f;
    public int ClientArmorRegenRateMs { get; set; } = 100;
    public float EnemyHealthScale { get; set; } = 1f;
    public float EnemyDamageScale { get; set; } = 1f;
    public float EnemySpawnRateSec { get; set; } = 1f;
    public float ArmorRegenDelay { get; set; } = 3f;
    public float ScoreMultiplicator { get; set; } = 1f;

}