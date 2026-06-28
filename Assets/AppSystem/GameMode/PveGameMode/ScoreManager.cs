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
        Multiplicator = preset.scoreMultiplicator;
    }

    public void IncreaseScore(float value)
    {
        value *= Multiplicator;
        score += value;
        OnscoreUpdated?.Invoke(score);
    }

    public float Score
    {
        set { score = value; }
        get { return score; }
    }

    public float Multiplicator
    {
        set { scoreMultiplicator = value; }
        get { return scoreMultiplicator; }
    }
    protected float score;
    protected float scoreMultiplicator;
}
