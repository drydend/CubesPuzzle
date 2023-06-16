using CommandsSystem;
using GameUI;
using Input;
using StateMachines;
using System;
using System.Collections.Generic;
using WallsSystem;
using UnityEngine;

namespace LevelSystem
{
    public class Level 
    {
        protected StateMachine _stateMachine;

        protected PlayerInput _playerInput;
        protected LevelPreset _preset;
        protected ICommandExecutor _commandExecutor;
        protected ILevelCompleteChecker _levelWinChecker;
        protected UIMenusHolder _UIMenusHolder;
        protected LevelUI _levelUI;

        public List<MoveableWall> Walls { get; private set; }

        public event Action Compleated;

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

        public virtual void InitializeStateMachine()
        {
            var states = new Dictionary<Type, BaseState>();
            _stateMachine = new StateMachine(states);

            states.Add(typeof(LevelIdleState), new LevelIdleState(_stateMachine, _playerInput, _levelWinChecker));
            states.Add(typeof(CubesMovingState), new CubesMovingState(_stateMachine, Walls, _commandExecutor));
            states.Add(typeof(LevelCompleteState), new LevelCompleteState(_levelUI.LevelCompleteUI, _UIMenusHolder, this));
        }

        public virtual void OnCompleated()
        {
            Compleated?.Invoke();
        }

        public virtual void ResetLevel()
        {
            _stateMachine.ExitAllStates();
            _levelUI.ResetUI();
            _preset.ResetLevel();
            _commandExecutor.ResetCommandExecutor();
        }

        public virtual void StartLevel()
        {
            _stateMachine.SwitchState<LevelIdleState>();
        }

        public virtual void StartLevelWithSwipe(Vector2 swipeDirection)
        {
            var args = new CubesMovingStateArgs(swipeDirection);
            _stateMachine.SwithcStateWithParam<CubesMovingState, CubesMovingStateArgs>(args);
        }

        public void DestroyLevel() 
        {
            _stateMachine.ExitAllStates();
            _preset.gameObject.SetActive(false);
            UnityEngine.Object.Destroy(_preset.gameObject);
        }
    }
}
