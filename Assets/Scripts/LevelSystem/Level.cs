using CommandsSystem;
using Input;
using LevelSystem.States;
using StateMachines;
using System;
using System.Collections.Generic;
using Wall;

namespace LevelSystem
{
    public class Level 
    {
        private StateMachine _stateMachine;

        private PlayerInput _playerInput;
        private LevelPreset _preset;
        private List<MoveableWall> _walls;
        private ICommandExecutor _commandExecutor;

        public Level(PlayerInput input, LevelPreset levelPreset, ICommandExecutor commandExecutor)
        {
            _playerInput = input;
            _preset = levelPreset;
            _walls = _preset.Walls;
            _commandExecutor = commandExecutor;
        }

        public void InitializeStateMachine()
        {
            var states = new Dictionary<Type, BaseState>();
            _stateMachine = new StateMachine(states);

            states.Add(typeof(LevelIdleState), new LevelIdleState(_stateMachine, _playerInput));
            states.Add(typeof(CubesMovingState), new CubesMovingState(_stateMachine, _walls, _commandExecutor));
        }

        public void StartLevel()
        {
            _stateMachine.SwitchState<LevelIdleState>();
        }

        public void DestroyLevel() 
        {
            _stateMachine.ExitAllStates();
            UnityEngine.Object.Destroy(_preset);
        }
    }
}
