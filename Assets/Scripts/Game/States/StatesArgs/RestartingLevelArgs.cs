using LevelSystem;

public class RestartingLevelArgs
{
    public Level CurrentLevel { get; private set; }

    public RestartingLevelArgs(Level level)
    {
        CurrentLevel = level;
    }
}