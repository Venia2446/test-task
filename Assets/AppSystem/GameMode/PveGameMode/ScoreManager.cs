using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public event Action<float> OnScoreUpdated;

    public void Init(DiffcultyPreset preset)
    {
        ScoreMultiplicator = preset.ScoreMultiplicator;
    }

    public void IncreaseScore(float value)
    {
        value *= ScoreMultiplicator;
        Score += value;
        OnScoreUpdated?.Invoke(Score);
    }

    public float Score { get; private set; }
    public float ScoreMultiplicator { get; private set; }

}
