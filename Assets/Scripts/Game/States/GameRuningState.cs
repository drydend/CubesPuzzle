using GameUI;
using PauseSystem;
using StateMachines;

public class GameRuningState : BaseState
{
    private readonly StateMachine _stateMachine;
    private readonly UIMenu _gameRuningUI;
    private readonly UIMenusHolder _UIMenusHolder;
    private readonly IPauseTrigger _pauseTrigger;

    public GameRuningState(StateMachine stateMachine ,UIMenu gameRuningUI,
        UIMenusHolder UIMenusHolder, IPauseTrigger pauseTrigger)
    {
        _stateMachine = stateMachine;
        _gameRuningUI = gameRuningUI;
        _UIMenusHolder = UIMenusHolder;
        _pauseTrigger = pauseTrigger;
    }
    public override void Enter()
    {
        _pauseTrigger.GamePaused += PauseGame;
        _UIMenusHolder.OpenMenu(_gameRuningUI);
    }

    public override void Exit()
    {
        _UIMenusHolder.CloseCurrentMenu();
        _pauseTrigger.GamePaused -= PauseGame;
    }

    private void PauseGame()
    {
        _stateMachine.SwitchState<GamePausedState>();
    }
}
