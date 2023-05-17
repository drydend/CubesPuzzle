using GameUI;
using Input;
using LevelSystem;
using PauseSystem;
using SavingSystem;
using StateMachines;
using System;
using System.Collections.Generic;
using Utils;

public class Game
{
    private StateMachine _stateMachine;

    private IPauseTrigger _pauseTrigger;
    private IUnpauseTrigger _unpauseTrigger;
    private ICoroutinePlayer _coroutinePlayer;
    private ILevelSaveDataLoader _levelDataSaver;
    private LevelPauser _levelPauser;
    private PlayerInput _input;
    private CameraMover _cameraMover;

    private LevelsConfigs _levelsConfigs;
    private LevelFactory _levelFactory;
    private UIMenusHolder _menusHolder;
    private LevelUI _levelUI;

    private Level _currentLevel;

    public int LastCompleatedLevel { get; private set; }

    public int LastLoadedLevel { get; private set; }

    public Game(LevelsConfigs levelsConfigs, LevelFactory levelFactory, LevelPauser levelPauser, CameraMover cameraMover,
        UIMenusHolder uIMenusHolder, LevelUI levelUI, ICoroutinePlayer coroutinePlayer,
        PlayerInput input, IPauseTrigger pauseTrigger, IUnpauseTrigger unpauseTrigger, ILevelSaveDataLoader saveDataLoader)
    {
        _levelFactory = levelFactory;
        _levelPauser = levelPauser;
        _cameraMover = cameraMover;
        _levelsConfigs = levelsConfigs;
        _menusHolder = uIMenusHolder;
        _levelUI = levelUI;
        _coroutinePlayer = coroutinePlayer;
        _input = input;
        _pauseTrigger = pauseTrigger;
        _unpauseTrigger = unpauseTrigger;
        _levelDataSaver = saveDataLoader;
    }

    public void Initialize()
    {
        LoadSaveData();
        InitializeStateMachine();
    }

    public void LoadLastUnlockedLevel()
    {
        LoadLevel(LastCompleatedLevel + 1);
    }

    public void LoadLevel(int levelNumber)
    {
        LastLoadedLevel = levelNumber;
        var config = _levelsConfigs.Configs.Find(cnf => cnf.LevelNumber == levelNumber);
        var args = new LoadingLevelArgs(config);

        _menusHolder.CloseAllMenus();
        _stateMachine.SwithcStateWithParam<GameLoadingLevelState, LoadingLevelArgs>(args);
    }

    public void LoadNextLevel()
    {
        LoadLevel(LastLoadedLevel + 1);
    }

    public void RestartLevel()
    {
        var args = new RestartingLevelArgs(_levelFactory.LastCreateLevel);
        _stateMachine.SwithcStateWithParam<GameRestartingLevelState, RestartingLevelArgs>(args);
    }

    public void SetLastLoadedLevel(Level loadedLevel)
    {
        if (_currentLevel != null)
        {
            _currentLevel.Compleated -= OnCurrentLevelCompleate;
        }

        _currentLevel = loadedLevel;
        _currentLevel.Compleated += OnCurrentLevelCompleate;
    }

    private void LoadSaveData()
    {
        var saveData = _levelDataSaver.LoadLevelSaveData();
        _levelsConfigs.InitializeWithSaveData(saveData);
        LastCompleatedLevel = saveData.LastCompleatedLevel;
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>();
        _stateMachine = new StateMachine(states);

        states.Add(typeof(GameLoadingLevelState), new GameLoadingLevelState(this, _stateMachine, _menusHolder,
            _levelUI, _levelPauser, _levelFactory, _coroutinePlayer, _cameraMover));
        states.Add(typeof(GameStartState), new GameStartState(_stateMachine, _levelUI.LevelStartUI, _cameraMover, _menusHolder,
            _input, _pauseTrigger));
        states.Add(typeof(GameRuningState), new GameRuningState(_stateMachine, _levelUI.GameRuningUI,
            _menusHolder, _pauseTrigger));
        states.Add(typeof(GamePausedState), new GamePausedState(_stateMachine, _levelPauser, _unpauseTrigger,
            _levelUI.GamePausedUI, _menusHolder));
        states.Add(typeof(GameRestartingLevelState), new GameRestartingLevelState(_stateMachine, _coroutinePlayer, _levelUI.ScreenFade));
    }

    private void OnCurrentLevelCompleate()
    {
        if (LastLoadedLevel > LastCompleatedLevel)
        {
            LastCompleatedLevel = LastLoadedLevel;
            var levelConfig = _levelsConfigs.Configs.Find(cnf => cnf.LevelNumber == LastCompleatedLevel + 1);
            levelConfig.Unlock();
            _levelUI.UpdateUI(levelConfig);
            _levelUI.UpdateChoseMenu(_levelsConfigs);
            SaveLevelsData();
        }
    }

    private void SaveLevelsData()
    {
        var levelsData = new List<LevelSaveData>();

        foreach (var config in _levelsConfigs.Configs)
        {
            levelsData.Add(new LevelSaveData(config.LevelNumber, config.IsUnlocked));
        }

        var levelsSaveData = new LevelsSaveData(levelsData, LastCompleatedLevel);

        _levelDataSaver.SaveLevelsData(levelsSaveData);
    }
}
