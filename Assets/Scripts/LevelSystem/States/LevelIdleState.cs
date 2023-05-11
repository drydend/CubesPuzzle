using Input;
using StateMachines;
using System;
using UnityEngine;

namespace LevelSystem.States
{
    public class LevelIdleState : BaseState
    {
        private readonly StateMachine _stateMachine;
        private readonly PlayerInput _playerInput;
        private ILevelWinChecker _winChecker;

        public LevelIdleState(StateMachine stateMachine, PlayerInput input, ILevelWinChecker levelWinChecher) 
        {
            _stateMachine = stateMachine;
            _playerInput = input;
            _winChecker = levelWinChecher;
        }

        public override void Enter()
        {
            _playerInput.Swiped += OnPlayerSwiped;

            if (_winChecker.CheckWin())
            {
                OnPlayerWin();
            }
        }

        private void OnPlayerWin()
        {
            _stateMachine.SwitchState<LevelCompleteState>();
        }

        public override void Exit()
        {
            _playerInput.Swiped -= OnPlayerSwiped;
        }

        private void OnPlayerSwiped(Vector2 direction)
        {
            var wallsMovingStateArgs = new CubesMovingStateArgs(direction);
            _stateMachine.SwithcStateWithParam<CubesMovingState, CubesMovingStateArgs>(wallsMovingStateArgs);
        }
    }
}
