using Input;
using StateMachines;
using UnityEngine;

namespace LevelSystem
{
    public class LevelIdleState : BaseState
    {
        private readonly StateMachine _stateMachine;
        private readonly PlayerInput _playerInput;
        private ILevelCompleteChecker _winChecker;

        public LevelIdleState(StateMachine stateMachine, PlayerInput input, ILevelCompleteChecker levelWinChecher) 
        {
            _stateMachine = stateMachine;
            _playerInput = input;
            _winChecker = levelWinChecher;
        }

        public override void Enter()
        {
            _playerInput.SwipedOnGameField += OnPlayerSwiped;

            if (_winChecker.CheckWin())
            {
                OnPlayerWin();
            }
        }

        public override void Exit()
        {
            _playerInput.SwipedOnGameField -= OnPlayerSwiped;
        }

        private void OnPlayerWin()
        {
            _stateMachine.SwitchState<LevelCompleteState>();
        }

        private void OnPlayerSwiped(Vector2 direction)
        {
            var wallsMovingStateArgs = new CubesMovingStateArgs(direction);
            _stateMachine.SwithcStateWithParam<CubesMovingState, CubesMovingStateArgs>(wallsMovingStateArgs);
        }
    }
}
