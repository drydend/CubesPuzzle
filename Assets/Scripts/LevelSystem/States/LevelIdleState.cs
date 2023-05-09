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

        public LevelIdleState(StateMachine stateMachine, PlayerInput input) 
        {
            _stateMachine = stateMachine;
            _playerInput = input;
        }

        public override void Enter()
        {
            _playerInput.Swiped += OnPlayerSwiped;
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
