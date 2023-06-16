using LevelSystem;

public class GameStartStateArgs
{
    public Level CurrentLevel { get; private set; }
    public bool IsTutorial { get; private set; }

    public GameStartStateArgs(Level currentLevel, bool isTutorial = false)
    {
        CurrentLevel = currentLevel;
        IsTutorial = isTutorial;
    }
}