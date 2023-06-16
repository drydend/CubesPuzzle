using LevelSystem;

public class LoadingLevelArgs
{
    public LevelConfig LevelConfig { get; private set; }
    public LevelType LevelType { get; private set; }

    public LoadingLevelArgs(LevelConfig levelConfig, LevelType levelType = LevelType.Common)
    {
        LevelConfig = levelConfig;
        LevelType = levelType;
    }
}