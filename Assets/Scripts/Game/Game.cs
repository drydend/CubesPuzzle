using GameUI;
using Input;
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
    private IUnpauseTrigger _unpauseTrigger;
    private ICoroutinePlayer _coroutinePlayer;
    private LevelPauser _levelPauser;
    private PlayerInput _input;
    private CameraMover _cameraMover;

    private LevelsConfigs _levelsConfigs;
    private LevelFactory _levelFactory;
    private UIMenusHolder _menusHolder;
    private LevelUI _levelUI;

    public Game(LevelsConfigs levelsConfigs, LevelFactory levelFactory, LevelPauser levelPauser, CameraMover cameraMover,
        UIMenusHolder uIMenusHolder, LevelUI levelUI, ICoroutinePlayer coroutinePlayer,
        PlayerInput input, IPauseTrigger pauseTrigger, IUnpauseTrigger unpauseTrigger)
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
    }

    public void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>();
        _stateMachine = new StateMachine(states);

        states.Add(typeof(GameLoadingLevelState), new GameLoadingLevelState(_stateMachine, _menusHolder,
            _levelUI, _levelPauser, _levelFactory, _coroutinePlayer, _cameraMover));
        states.Add(typeof(GameStartState), new GameStartState(_stateMachine, _levelUI.LevelStartUI, _cameraMover, _menusHolder,
            _input, _pauseTrigger));
        states.Add(typeof(GameRuningState), new GameRuningState(_stateMachine, _levelUI.GameRuningUI,
            _menusHolder, _pauseTrigger));
        states.Add(typeof(GamePausedState), new GamePausedState(_stateMachine, _levelPauser, _unpauseTrigger,
            _levelUI.GamePausedUI, _menusHolder));
    }

    public void LoadLevel(int levelNumber)
    {
        var config = _levelsConfigs.Configs.Find(cnf => cnf.LevelNumber == levelNumber);
        var args = new LoadingLevelArgs(config);
        _stateMachine.SwithcStateWithParam<GameLoadingLevelState, LoadingLevelArgs>(args);
    }
}
