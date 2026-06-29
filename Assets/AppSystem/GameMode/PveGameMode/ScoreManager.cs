using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DifficultyPresetsLib;

public class ScoreManager : MonoBehaviour
{
    public delegate void HandleOnScoreUpdated(float score);
    public event HandleOnScoreUpdated OnscoreUpdated;

    public void Init(DiffcultyPreset preset)
    {
        ScoreMultiplicator = preset.ScoreMultiplicator;
    }

    public void IncreaseScore(float value)
    {
        value *= ScoreMultiplicator;
        Score += value;
        OnscoreUpdated?.Invoke(Score);
    }

    public float Score { get; private set; }
    public float ScoreMultiplicator { get; private set; }

}
