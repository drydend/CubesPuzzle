using GameUI;
using LevelSystem;
using PauseSystem;
using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using WallsSystem;

public class GameLoadingLevelState : ParamBaseState<LoadingLevelArgs>
{
    private Game _game;
    private StateMachine _stateMachine;

    private UIMenusHolder _menusHolder;
    private LevelUI _levelUI;
    private LevelPauser _levelPauser;
    private LevelFactory _levelFactory;
    private ICoroutinePlayer _coroutinePlayer;
    private ScreenFade _screenFade;
    private CameraMover _cameraMover;

    private Level _loadedLevel;

    private LoadingLevelArgs _loadingLevelArgs;

    private Coroutine _levelCreationCoroutine;

    public GameLoadingLevelState(Game game, StateMachine stateMachine, UIMenusHolder menusHolder, LevelUI levelUI,
        LevelPauser levelPauser, LevelFactory levelFactory, 
        ICoroutinePlayer coroutinePlayer, CameraMover cameraMover)
    {
        _game = game;
        _stateMachine = stateMachine;
        _menusHolder = menusHolder;
        _levelUI = levelUI;
        _levelPauser = levelPauser;
        _levelFactory = levelFactory;
        _coroutinePlayer = coroutinePlayer;
        _screenFade = levelUI.ScreenFade;
        _cameraMover = cameraMover;
    }

    public override void Enter()
    {
        if (_levelCreationCoroutine != null)
        {
            _coroutinePlayer.StopRoutine(_levelCreationCoroutine);
        }

        _cameraMover.SetLevelConfig(_loadingLevelArgs.LevelConfig);
        _menusHolder.CloseAllMenus();
        _levelCreationCoroutine = _coroutinePlayer.StartRoutine(LoadNewLevel());
    }

    public override void Exit()
    {

    }

    public override void SetArgs(LoadingLevelArgs args)
    {
        _loadingLevelArgs = args;
    }

    private IEnumerator LoadNewLevel()
    {
        yield return _screenFade.Fade();

        ResetPreviousLevel();
        _loadedLevel = _levelFactory.CreateLevel(_loadingLevelArgs.LevelConfig);
        _game.SetLastLoadedLevel(_loadedLevel);
        InitPauseSystem();

        _levelUI.UpdateUI(_loadingLevelArgs.LevelConfig);

        var args = new GameStartStateArgs(_loadedLevel);
        _stateMachine.SwithcStateWithParam<GameStartState, GameStartStateArgs>(args);

        yield return _screenFade.UnFade();
    }

    private void InitPauseSystem()
    {
        var pauseableWalls = GetPauseableFromWalls(_loadedLevel.Walls);
        _levelPauser.AddPauseable(pauseableWalls);
    }

    private void ResetPreviousLevel()
    {
        _levelPauser.ResetPauseables();
        _loadedLevel?.DestroyLevel();
    }

    private List<IPauseable> GetPauseableFromWalls(List<MoveableWall> walls)
    {
        var result = new List<IPauseable>();

        foreach (var item in walls)
        {
            result.Add(item);
        }

        return result;
    }
}
