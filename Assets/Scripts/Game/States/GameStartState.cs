using GameUI;
using LevelSystem;
using PauseSystem;
using StateMachines;
using UnityEngine;

public class GameStartState : ParamBaseState<GameStartStateArgs>
{
    private StateMachine _stateMachine;

    private UIMenu _levelStateUIMenu;
    private CameraMover _cameraMover;
    private UIMenusHolder _UIMenusHolder;
    private ILevelStartTrigger _levelStartTrigger;
    private IPauseTrigger _levelPauseTrigger;

    private GameStartStateArgs _args;

    public GameStartState(StateMachine stateMachine, UIMenu levelStateUIMenu, CameraMover cameraMover,
        UIMenusHolder uIMenusHolder, ILevelStartTrigger levelStartTrigger, IPauseTrigger levelPauseTrigger)
    {
        _stateMachine = stateMachine;
        _levelStateUIMenu = levelStateUIMenu;
        _cameraMover = cameraMover;
        _UIMenusHolder = uIMenusHolder;
        _levelStartTrigger = levelStartTrigger;
        _levelPauseTrigger = levelPauseTrigger;
    }

    public override void Enter()
    {
        if (_args.IsTutorial)
        {
            _args.CurrentLevel.StartLevel();
            _stateMachine.SwitchState<GameRuningState>();
            return; 
        }

        _cameraMover.SetToLevelStartPosition();
        _UIMenusHolder.OpenMenu(_levelStateUIMenu);
        _levelPauseTrigger.GamePaused += PauseGame;
        _levelStartTrigger.LevelStarted += StartLevel;
    }

    public override void Exit()
    {
        _cameraMover.MoveToLevelRuningPosition();
        _UIMenusHolder.CloseCurrentMenu();
        _levelPauseTrigger.GamePaused -= PauseGame;
        _levelStartTrigger.LevelStarted -= StartLevel;
    }

    public override void SetArgs(GameStartStateArgs args)
    {
        _args = args;
    }

    private void PauseGame()
    {
        _stateMachine.SwitchState<GamePausedState>();
    }

    private void StartLevel(Vector2 swipeDirection)
    {
        _args.CurrentLevel.StartLevelWithSwipe(swipeDirection);
        _stateMachine.SwitchState<GameRuningState>();
    }
}
