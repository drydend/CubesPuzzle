using LevelSystem;
using System.Collections.Generic;

public class Game
{
    private LevelsConfigs _levelsConfigs;
    private LevelFactory _levelFactory;

    private Level _currentLevel;

    public Game(LevelsConfigs levelsConfigs, LevelFactory levelFactory)
    {
        _levelFactory = levelFactory;
        _levelsConfigs = levelsConfigs;
    }

    public void StartLevel(int levelNumber)
    {
        _currentLevel?.DestroyLevel();

        var levelConfig = _levelsConfigs.Configs.Find(cnf => cnf.LevelNumber == levelNumber);
        var level = _levelFactory.CreateLevel(levelConfig);

        _currentLevel = level;
        _currentLevel.StartLevel();
    }

    public void StartNextLevel()
    {
        if(_currentLevel == null)
        {
            return;
        }
    }
}
