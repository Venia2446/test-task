using static Globals;

public class GameModeBuilder
{
    public GameModeBuilder(DifficultyPresetsLib presetsLib)
    {
        difficultyPresetsLib = presetsLib;
        GameModeParams = new GameModeParams();
    }

    public void SetGameModeStartParams(DifficultyPresetType type)
    {
        GameModeParams.DiffPreset = difficultyPresetsLib.GetPreset(type);
    }

    public void BuildGameMode(GameModeBase gameMode)
    {
        gameMode.Init(GameModeParams);
    }

    private GameModeParams GameModeParams { get; set; }
    private DifficultyPresetsLib difficultyPresetsLib { get; set; }

}

public class GameModeParams
{
    public DiffcultyPreset DiffPreset { get; set; }
}
