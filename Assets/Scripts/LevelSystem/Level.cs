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
        private List<MoveableWall> _walls;
        private ICommandExecutor _commandExecutor;
        private ILevelWinChecker _levelWinChecker;
        private UIMenusHolder _UIMenusHolder;
        private LevelUI _levelUI;

        public Level(PlayerInput input, LevelPreset levelPreset, 
            ICommandExecutor commandExecutor, ILevelWinChecker levelWinChecker, 
            UIMenusHolder uIMenusHolder, LevelUI levelUI) 
        {
            _playerInput = input;
            _preset = levelPreset;
            _walls = _preset.Walls;
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
            states.Add(typeof(CubesMovingState), new CubesMovingState(_stateMachine, _walls, _commandExecutor));
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
