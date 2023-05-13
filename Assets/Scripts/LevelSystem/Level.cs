using CommandsSystem;
using GameUI;
using Input;
using LevelSystem.States;
using StateMachines;
using System;
using System.Collections.Generic;
using WallsSystem;
using UnityEngine;

namespace LevelSystem
{
    public class Level 
    {
        private StateMachine _stateMachine;

        private PlayerInput _playerInput;
        private LevelPreset _preset;
        private ICommandExecutor _commandExecutor;
        private ILevelCompleteChecker _levelWinChecker;
        private UIMenusHolder _UIMenusHolder;
        private LevelUI _levelUI;

        public List<MoveableWall> Walls { get; private set; }

        public Level(PlayerInput input, LevelPreset levelPreset, 
            ICommandExecutor commandExecutor, ILevelCompleteChecker levelWinChecker, 
            UIMenusHolder uIMenusHolder, LevelUI levelUI) 
        {
            _playerInput = input;
            _preset = levelPreset;
            Walls = _preset.Walls;
            _commandExecutor = commandExecutor;
            _levelWinChecker = levelWinChecker;
            _UIMenusHolder = uIMenusHolder;
            _levelUI = levelUI;
        }

        public void InitializeStateMachine()
        {
            var states = new Dictionary<Type, BaseState>();
            _stateMachine = new StateMachine(states);

            states.Add(typeof(LevelIdleState), new LevelIdleState(_stateMachine, _playerInput, _levelWinChecker));
            states.Add(typeof(CubesMovingState), new CubesMovingState(_stateMachine, Walls, _commandExecutor));
            states.Add(typeof(LevelCompleteState), new LevelCompleteState(_levelUI.LevelCompleteUI, _UIMenusHolder));
        }

        public void StartLevel()
        {
            _stateMachine.SwitchState<LevelIdleState>();
        }

        public void StartLevelWithSwipe(Vector2 swipeDirection)
        {
            var args = new CubesMovingStateArgs(swipeDirection);
            _stateMachine.SwithcStateWithParam<CubesMovingState, CubesMovingStateArgs>(args);
        }

        public void DestroyLevel() 
        {
            _stateMachine.ExitAllStates();
            UnityEngine.Object.Destroy(_preset);
        }
    }
}
