using GameUI;
using LevelSystem;
using PauseSystem;
using StateMachines;
using UnityEngine;

public class GameStartState : ParamBaseState<GameStartStateArgs>
{
    private StateMachine _stateMachine;

    private UIMenu _levelStateUIMenu;
    private UIMenusHolder _UIMenusHolder;
    private ILevelStartTrigger _levelStartTrigger;
    private IPauseTrigger _levelPauseTrigger;

    private Level _currentLevel;

    public GameStartState(StateMachine stateMachine, UIMenu levelStateUIMenu,
        UIMenusHolder uIMenusHolder, ILevelStartTrigger levelStartTrigger, IPauseTrigger levelPauseTrigger)
    {
        _stateMachine = stateMachine;
        _levelStateUIMenu = levelStateUIMenu;
        _UIMenusHolder = uIMenusHolder;
        _levelStartTrigger = levelStartTrigger;
        _levelPauseTrigger = levelPauseTrigger;
    }

    public override void Enter()
    {
        _UIMenusHolder.OpenMenu(_levelStateUIMenu);
        _levelPauseTrigger.GamePaused += PauseGame;
        _levelStartTrigger.LevelStarted += StartLevel;
    }

    public override void Exit()
    {
        _UIMenusHolder.CloseCurrentMenu();
        _levelPauseTrigger.GamePaused -= PauseGame;
        _levelStartTrigger.LevelStarted -= StartLevel;
    }

    public override void SetArgs(GameStartStateArgs args)
    {
        _currentLevel = args.CurrentLevel;
    }

    private void PauseGame()
    {
        _stateMachine.SwitchState<GamePausedState>();
    }

    private void StartLevel(Vector2 swipeDirection)
    {
        _currentLevel.StartLevelWithSwipe(swipeDirection);
        _stateMachine.SwitchState<GameRuningState>();
    }
}
