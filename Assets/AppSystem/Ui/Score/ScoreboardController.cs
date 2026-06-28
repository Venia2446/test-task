using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Globals;

public class ScoreboardController : MonoBehaviour
{
    public Text text;

    private void Awake()
    {
        gameEvents = GameEvents.Instance;
        gameEvents.SubscribeGameEvent(Init, Terminate);
    }

    private void Init()
    {
        var gameMode = (PveGameMode)AppSystemClient.Instance.GameMode;
        diffType = gameMode.DiffcultyPreset.type.ToString();
        scoreManager = gameMode.scoreManager;
        multiplicator = scoreManager.Multiplicator;
        scoreManager.OnscoreUpdated += HandleOnScoreUpdated;
        HandleOnScoreUpdated(scoreManager.Score);
    }

    private void Terminate()
    {
        scoreManager.OnscoreUpdated -= HandleOnScoreUpdated;
    }

    private void OnDestroy()
    {
        gameEvents?.UnsubscribeGameEvents(Init, Terminate);
        Terminate();
    }

    private void HandleOnScoreUpdated(float score)
    {
        text.text = string.Format(UI_SCOREBOARD, score, multiplicator, diffType);
    }

    private const string UI_SCOREBOARD = "{0} : Multiplicator x{1}, Difficulty: {2}";

    private GameEvents gameEvents;
    private ScoreManager scoreManager;

    private float multiplicator;
    private string diffType;

}
