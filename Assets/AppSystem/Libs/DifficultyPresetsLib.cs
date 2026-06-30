using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;


public class DifficultyPresetsLib : MonoBehaviour
{
    public DiffcultyPreset[] difficultyPresets;

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
    public DifficultyPresetType Type = DifficultyPresetType.EASY;
    public float ClientMaxHealth = 100f;
    public float ClienMaxArmor = 150f;
    public float ClientArmorRegenValue = 0.3f;
    public float ClientDamage = 25f;
    public int ClientArmorRegenRateMs = 100;
    public float EnemyHealthScale = 1f;
    public float EnemyDamageScale = 1f;
    public float EnemySpawnRateSec = 1f;
    public float ArmorRegenDelay = 3f;
    public float ScoreMultiplicator = 1f;

}