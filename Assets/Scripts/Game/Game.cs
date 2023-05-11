using GameUI;
using LevelSystem;
using PauseSystem;
using StateMachines;
using System;
using System.Collections.Generic;
using Utils;

public class Game
{
    private StateMachine _stateMachine;

    private IPauseTrigger _pauseTrigger;
    private readonly IUnpauseTrigger _unpauseTrigger;
    private ICoroutinePlayer _coroutinePlayer;
    private ILevelStartTrigger _levelStartTrigger;
    private LevelPauser _levelPauser;
    
    private LevelsConfigs _levelsConfigs;
    private LevelFactory _levelFactory;
    private UIMenusHolder _menusHolder;
    private LevelUI _levelUI;

    public Game(LevelsConfigs levelsConfigs, LevelFactory levelFactory, LevelPauser levelPauser,
        UIMenusHolder uIMenusHolder, LevelUI levelUI, ICoroutinePlayer coroutinePlayer,
        ILevelStartTrigger levelStartTrigger, IPauseTrigger pauseTrigger, IUnpauseTrigger unpauseTrigger)
    {
        _levelFactory = levelFactory;
        _levelPauser = levelPauser;
        _levelsConfigs = levelsConfigs;
        _menusHolder = uIMenusHolder;
        _levelUI = levelUI;
        _coroutinePlayer = coroutinePlayer;
        _levelStartTrigger = levelStartTrigger;
        _pauseTrigger = pauseTrigger;
        _unpauseTrigger = unpauseTrigger;
    }

    public void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>();
        var stateMachine = new StateMachine(states);

        states.Add(typeof(GameLoadingLevelState), new GameLoadingLevelState(stateMachine, _menusHolder,
            _levelUI, _levelFactory, _coroutinePlayer, _levelUI.ScreenFade));
        states.Add(typeof(GameStartState), new GameStartState(stateMachine, _levelUI.LevelStartUI, _menusHolder,
            _levelStartTrigger, _pauseTrigger));
        states.Add(typeof(GameRuningState), new GameRuningState(stateMachine, _levelUI.GameRuningUI, 
            _menusHolder, _pauseTrigger));
        states.Add(typeof(GamePausedState), new GamePausedState(stateMachine, _levelPauser, _unpauseTrigger, 
            _levelUI.GamePausedUI, _menusHolder));
    }

    public void LoadLevel(int levelNumber)
    {
        var config = _levelsConfigs.Configs.Find(cnf => cnf.LevelNumber == levelNumber);
        var args = new LoadingLevelArgs(config);
        _stateMachine.SwithcStateWithParam<GameLoadingLevelState, LoadingLevelArgs>(args);
    }
}
