using static DifficultyPresetsLib;
using static Globals;

public class GameModeBuilder
{
    public GameModeBuilder(DifficultyPresetsLib presetsLib)
    {
        difficultyPresetsLib = presetsLib;
    }

    public void CreateGameModeParams()
    {
        gameModeParams = new GameModeParams();
    }

    public void SetGameModeStartParams(DifficultyPresetType type)
    {
        gameModeParams.diffPreset = difficultyPresetsLib.GetPreset(type);
    }

    public void BuildGameMode(GameModeBase gameMode)
    {

        gameMode.Init(gameModeParams.diffPreset);
    }

    protected struct GameModeParams
    {
        public DiffcultyPreset diffPreset;
    }

    protected GameModeParams gameModeParams;
    protected DifficultyPresetsLib difficultyPresetsLib;

}
