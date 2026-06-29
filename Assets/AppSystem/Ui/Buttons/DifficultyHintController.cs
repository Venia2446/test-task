using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DifficultyPresetsLib;
using static Globals;

public class DifficultyHintController : ButtonControllerWithTooltipController
{
    public DifficultyPresetType type;

    protected override void Init()
    {
        base.Init();

        var preset = AppSystemClient.Instance.diffLib.GetPreset(type);
        text = string.Format(UI_DIFFICULTY_HINT, preset.EnemyHealthScale, preset.EnemyDamageScale, 
                             preset.EnemySpawnRateSec, preset.ClientArmorRegenValue, preset.ArmorRegenDelay, preset.ScoreMultiplicator);
    }

    private const string UI_DIFFICULTY_HINT = "Enemy health scale: {0}\nEnemy damage scale: {1}\nEnemy spawn rate(sec): {2}" +
                                               "\nArmor regen: {3}\nArmor regen delay(sec): {4}\nScore miltiplicator: {5}";
                                    
}
