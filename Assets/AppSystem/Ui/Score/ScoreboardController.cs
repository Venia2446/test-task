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
        diffType = gameMode.DiffcultyPreset.Type.ToString();
        scoreManager = gameMode.scoreManager;
        multiplicator = scoreManager.ScoreMultiplicator;
        scoreManager.OnscoreUpdated += HandleOnScoreUpdated;
        HandleOnScoreUpdated(scoreManager.Score);
    }

    private void Terminate()
    {
        gameEvents?.UnsubscribeGameEvents(Init, Terminate);
        scoreManager.OnscoreUpdated -= HandleOnScoreUpdated;
    }

    private void OnDestroy()
    {

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
