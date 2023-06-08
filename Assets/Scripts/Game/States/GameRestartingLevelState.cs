using GameUI;
using LevelSystem;
using StateMachines;
using System.Collections;
using UnityEngine;
using Utils;

public class GameRestartingLevelState : ParamBaseState<RestartingLevelArgs>
{
    private StateMachine _stateMachine;

    private ICoroutinePlayer _coroutinePlayer;
    private ScreenFade _screenFade;

    private Coroutine _restartCoroutine;

    private RestartingLevelArgs _args;

    private bool _isStillRestarting;

    public GameRestartingLevelState(StateMachine stateMachine, ICoroutinePlayer coroutinePlayer, ScreenFade screenFade)
    {
        _stateMachine = stateMachine;
        _coroutinePlayer = coroutinePlayer;
        _screenFade = screenFade;
    }

    public override void Enter()
    {   
        if(_isStillRestarting)
        {
            _coroutinePlayer.StopRoutine(_restartCoroutine);
        }

        _restartCoroutine = _coroutinePlayer.StartRoutine(RestartLevel());
    }

    public override void Exit()
    {

    }

    public override void SetArgs(RestartingLevelArgs args)
    {
        _args = args;
    }

    private IEnumerator RestartLevel()
    {
        _isStillRestarting = true;
        yield return _screenFade.Fade();

        _args.CurrentLevel.ResetLevel();

        _stateMachine.SwithcStateWithParam<GameStartState, GameStartStateArgs>(new GameStartStateArgs(_args.CurrentLevel));

        yield return _screenFade.UnFade();
        _isStillRestarting = false;
    }
}