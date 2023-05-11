using GameUI;
using LevelSystem;
using StateMachines;
using System;
using System.Collections;
using UnityEngine;
using Utils;

public class GameLoadingLevelState : ParamBaseState<LoadingLevelArgs>
{
    private StateMachine _stateMachine;

    private UIMenusHolder _menusHolder;
    private LevelUI _levelUI;
    private LevelFactory _levelFactory;
    private ICoroutinePlayer _coroutinePlayer;
    private ScreenFade _screenFade;

    private Level _loadedLevel;

    private LoadingLevelArgs _loadingLevelArgs;

    private Coroutine _levelCreationCoroutine;

    public GameLoadingLevelState(StateMachine stateMachine, UIMenusHolder menusHolder,LevelUI levelUI,  
        LevelFactory levelFactory, ICoroutinePlayer coroutinePlayer, ScreenFade screenFade)
    {
        _stateMachine = stateMachine;
        _menusHolder = menusHolder;
        _levelFactory = levelFactory;
        _coroutinePlayer = coroutinePlayer;
        _screenFade = screenFade;
    }

    public override void Enter()
    {
        _menusHolder.CloseAllMenus();
        _levelCreationCoroutine = _coroutinePlayer.StartRoutine(LoadNewLevel());
    }

    public override void Exit()
    {
        if(_levelCreationCoroutine != null ) 
        {
            throw new Exception("You can not exit from loading level start until current level loading");
        }
    }

    public override void SetArgs(LoadingLevelArgs args)
    {
        _loadingLevelArgs = args;
    }

    private IEnumerator LoadNewLevel()
    {
        yield return _screenFade.Fade();

        _loadedLevel?.DestroyLevel();
        _loadedLevel = _levelFactory.CreateLevel(_loadingLevelArgs.LevelConfig);
        
        _levelUI.UpdateUI(_loadingLevelArgs.LevelConfig);
        
        yield return _screenFade.UnFade();
        
        var args = new GameStartStateArgs(_loadedLevel);
        _stateMachine.SwithcStateWithParam<GameStartState, GameStartStateArgs>(args);
    }

}
