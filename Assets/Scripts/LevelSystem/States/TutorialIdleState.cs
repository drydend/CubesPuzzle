using Input;
using StateMachines;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Tutorial;
using UnityEditor.Search;
using UnityEngine;
using Utils;

namespace LevelSystem
{
    public class TutorialIdleState : BaseState
    {
        private StateMachine _stateMachine;
        private PlayerInput _playerInput;
        private ILevelCompleteChecker _winChecker;

        private TutorialVisualizer _visualizer;
        private TutorialPath _tutorial;

        private TutorialStep _currentStep;

        public TutorialIdleState(StateMachine stateMachine, PlayerInput input,
            ILevelCompleteChecker levelWinChecher, TutorialVisualizer tutorialVisualizer, TutorialPath tutorialPath)
        {
            _stateMachine = stateMachine;
            _playerInput = input;
            _winChecker = levelWinChecher;
            _visualizer = tutorialVisualizer;
            _tutorial = tutorialPath;
        }

        public override void Enter()
        {
            _visualizer.Show();

            _playerInput.SwipedOnGameField += OnPlayerSwiped;
            _playerInput.Tapped += OnPlayerTapped;

            MoveToNextTutorialStep();
        }

        public override void Exit()
        {
            _playerInput.SwipedOnGameField -= OnPlayerSwiped;
            _playerInput.Tapped -= OnPlayerTapped;
        }

        private void MoveToNextTutorialStep()
        {
            _currentStep = _tutorial.GetCurrentStep();

            if (_currentStep == null)
            {
                _visualizer.Hide();
                OnTutorialCompleated();
            }
            else
            {
                _visualizer.VisualizeStep(_currentStep);
            }
        }

        private void OnPlayerTapped()
        {
            if (!_visualizer.IsAnimationOver)
            {
                _visualizer.SkipAnimation();
                return;
            }

            if (_currentStep.CompleteCodition == TutorialStepCompleteCodition.Tap)
            {
                MoveToNextTutorialStep();
            }
        }

        private void OnPlayerSwiped(Vector2 direction)
        {
            if (!_visualizer.IsAnimationOver)
            {
                _visualizer.SkipAnimation();
            }

            var isInputCorrect = WallsMovementDirectionCalculator.TryCalculateMoveDirectionAndWallType(direction,
                out WallType wallType, out MoveDirection moveDirection);

            if (!isInputCorrect)
            {
                return;
            }

            switch (_currentStep.CompleteCodition)
            {
                case TutorialStepCompleteCodition.HorizontalPositiveSwipe:

                    if (wallType == WallType.Horizontal && moveDirection == MoveDirection.Forward)
                    {
                        Move(direction);
                    }
                    break;

                case TutorialStepCompleteCodition.HorizontalNegativeSwipe:
                    if (wallType == WallType.Horizontal && moveDirection == MoveDirection.Backward)
                    {
                        Move(direction);
                    }
                    break;
                case TutorialStepCompleteCodition.VerticalPositiveSwipe:
                    if (wallType == WallType.Vertical && moveDirection == MoveDirection.Forward)
                    {
                        Move(direction);
                    }
                    break;
                case TutorialStepCompleteCodition.VerticalNegativeSwipe:
                    if (wallType == WallType.Vertical && moveDirection == MoveDirection.Backward)
                    {
                        Move(direction);
                    }
                    break;
                default:
                    break;
            }
        }

        private void Move(Vector2 direction)
        {
            var wallsMovingStateArgs = new CubesMovingStateArgs(direction);
            _stateMachine.SwithcStateWithParam<CubesMovingState, CubesMovingStateArgs>(wallsMovingStateArgs);
        }

        private void OnTutorialCompleated()
        {
            _stateMachine.SwitchState<LevelCompleteState>();
        }
    }
}
