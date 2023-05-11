using LevelSystem;

public class LoadingLevelArgs
{
    public LevelConfig LevelConfig { get; private set; }

    public LoadingLevelArgs(LevelConfig levelConfig)
    {
        LevelConfig = levelConfig;
    }
}