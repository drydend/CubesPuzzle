using LevelSystem;

public class GameStartStateArgs
{
    public Level CurrentLevel { get; private set; }

    public GameStartStateArgs(Level currentLevel)
    {
        CurrentLevel = currentLevel;
    }
}