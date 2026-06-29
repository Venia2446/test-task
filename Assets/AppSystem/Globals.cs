using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public const string playerTag = "Player";
    public const string enemyTag = "Enemy";
    public const DifficultyPresetType defaultDifficulty = DifficultyPresetType.EASY;
    public const int maxFill = 1;

    public enum EnemyType
    {
        NORMAL = 0,
        FAST = 1,
        HEAVY = 2,
        SHOOTER = 3,
        BOSS = 4,
    }
    public enum DifficultyPresetType
    {
        EASY,
        NORMAL,
        HARD,
    }

    public enum StatType
    {
        HEALTH,
        ARMOR,
    }

    public enum SceneType
    {
        BOOTUP = 0,
        TITLE = 1,
        PVE = 2,
        NONE,
    }
    public enum BulletType
    {
        SMALL,
        MEDIUM,
        POWER_SHOT,
    }

    public enum BulletAction
    {
        NONE,
        HIT,
        HIT_AND_DESTROY,
        DESTROY,
    }
    public enum GameModeType
    {
        PVE,
    }

    public enum AbilityType
    {
        POWER_SHOT,
    }

    public enum AudioClipType
    {
        PLAYER_SHOOTING,
        ABILITY,
        MELEE_HIT,
        RANGE_HIT,
        CLIENT_HIT,
        ENEMY_DEAD,
    }

}