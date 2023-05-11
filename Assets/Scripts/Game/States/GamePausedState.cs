using GameUI;
using PauseSystem;
using StateMachines;

public class GamePausedState : BaseState
{
    private StateMachine _stateMachine;

    private LevelPauser _levelPauser;
    private IUnpauseTrigger _unpauseTrigger;
    private readonly UIMenu _pauseMenu;
    private readonly UIMenusHolder _menuHolder;

    public GamePausedState(StateMachine stateMachine, LevelPauser levelPauser, IUnpauseTrigger unpauseTrigger, 
        UIMenu pauseMenu, UIMenusHolder menuHolder)
    {
        _stateMachine = stateMachine;
        _levelPauser = levelPauser;
        _unpauseTrigger = unpauseTrigger;
        _pauseMenu = pauseMenu;
        _menuHolder = menuHolder;
    }

    public override void Enter()
    {
        _menuHolder.OpenMenu(_pauseMenu);
        _levelPauser.PauseGame();
        _unpauseTrigger.GameUnpaused += UnpauseGame;
    }

    public override void Exit()
    {
        _menuHolder.CloseCurrentMenu();
        _levelPauser.UnpauseGame();
        _unpauseTrigger.GameUnpaused -= UnpauseGame;
    }

    private void UnpauseGame()
    {
        _stateMachine.SwitchToPreviousState();
    }
}
