using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;


public class DifficultyPresetsLib : MonoBehaviour
{
    [SerializeField] public DiffcultyPreset[] difficultyPresets;

    public delegate void HandleOnLoaded();
    public event HandleOnLoaded OnLoaded;

    public void Init()
    {
        foreach (DiffcultyPreset preset in difficultyPresets)
        {
            presetsDict.Add(preset.type, preset);
        }
    }

    public DiffcultyPreset GetPreset(DifficultyPresetType preset)
    {
        return presetsDict[preset];
    }

    [System.Serializable]
    public class DiffcultyPreset
    {
        public DifficultyPresetType type   = DifficultyPresetType.EASY;
        public float clientMaxHealth       = 100f;
        public float clienMaxArmor         = 150f;
        public float clientArmorRegenValue = 0.3f;
        public float clientDamage          = 25f;
        public int clientArmorRegenRateMs  = 100;
        public float enemyHealthScale      = 1f;
        public float enemyDamageScale      = 1f;
        public float enemySpawnRateSec     = 1f;             
        public float armorRegenDelay       = 3f;
        public float scoreMultiplicator    = 1f;
    }

    private Dictionary<DifficultyPresetType, DiffcultyPreset> presetsDict = new Dictionary<DifficultyPresetType, DiffcultyPreset>();
}
