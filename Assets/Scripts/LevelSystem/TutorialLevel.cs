using CommandsSystem;
using GameUI;
using Input;
using StateMachines;
using System;
using System.Collections.Generic;
using Tutorial;
using UnityEngine;

namespace LevelSystem
{
    public class TutorialLevel : Level
    {
        private TutorialVisualizer _tutorialVisualizer;
        private readonly TutorialCompleteTrigger _tutorialCompleteTrigger;
        private TutorialPath _tutorialPath;

        public TutorialLevel(PlayerInput input, LevelPreset levelPreset, ICommandExecutor commandExecutor,
            ILevelCompleteChecker levelWinChecker, UIMenusHolder uIMenusHolder, LevelUI levelUI,
            TutorialPath tutorialPath, TutorialVisualizer tutorialVisualizer, TutorialCompleteTrigger completeTrigger) : 
            base(input, levelPreset, commandExecutor, levelWinChecker, uIMenusHolder, levelUI)
        {
            _tutorialVisualizer = tutorialVisualizer;
            _tutorialCompleteTrigger = completeTrigger;
            _tutorialPath = tutorialPath;
        }

        public override void InitializeStateMachine()
        {
            var states = new Dictionary<Type, BaseState>();
            _stateMachine = new StateMachine(states);

            states.Add(typeof(LevelIdleState), new TutorialIdleState(_stateMachine, _playerInput, _levelWinChecker,
                _tutorialVisualizer , _tutorialPath));
            states.Add(typeof(CubesMovingState), new CubesMovingState(_stateMachine, Walls, _commandExecutor));
            states.Add(typeof(LevelCompleteState), new LevelCompleteState(_levelUI.TutorialCompleteUI, _UIMenusHolder, this));
        }

        public override void OnCompleated()
        {
            _tutorialCompleteTrigger.OnTutorialCompleated();
            base.OnCompleated();
        }

        public override void ResetLevel()
        {
            base.ResetLevel();
            _tutorialPath.Reset();
            _tutorialVisualizer.CloseCurrentStep();
        }

        public override void StartLevel()
        {
            base.StartLevel();
        }

        public override void StartLevelWithSwipe(Vector2 swipeDirection)
        {
            base.StartLevel();
        }

    }
}
