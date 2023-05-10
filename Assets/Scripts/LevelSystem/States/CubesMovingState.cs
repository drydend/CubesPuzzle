using CommandsSystem;
using LevelSystem.States;
using StateMachines;
using System.Collections.Generic;
using Utils;
using Wall;

namespace LevelSystem
{
    public class CubesMovingState : ParamBaseState<CubesMovingStateArgs>
    {
        private StateMachine _stateMachine;

        private ICommandExecutor _commandExecutor;

        private List<MoveableWall> _horizontalWalls = new List<MoveableWall>();
        private List<MoveableWall> _verticalWalls = new List<MoveableWall>();

        private CubesMovingStateArgs _args;

        public CubesMovingState(StateMachine stateMachine, List<MoveableWall> walls, ICommandExecutor commandExecutor)
        {
            _stateMachine = stateMachine;
            _commandExecutor = commandExecutor;
            InitLists(walls);
        }

        public override void Enter()
        {
            MoveWalls();
        }

        public override void Exit()
        {

        }

        public override void SetArgs(CubesMovingStateArgs args)
        {
            _args = args;
        }

        private void InitLists(List<MoveableWall> walls)
        {
            foreach (var wall in walls)
            {
                if (wall.WallType == WallType.Vertical)
                {
                    _verticalWalls.Add(wall);
                }
                else
                {
                    _horizontalWalls.Add(wall);
                }
            }
        }

        private void MoveWalls()
        {
            MoveWallsCommand movementCommand;

            WallType wallType;
            MoveDirection moveDirection;
            
            if(!WallsMovementDirectionCalculator
                .CalculateMoveDirectionAndWallType(_args._inputDirection, out wallType, out moveDirection))
            {
                _stateMachine.SwitchState<LevelIdleState>();
            }

            if (wallType == WallType.Vertical)
            {
                movementCommand = new MoveWallsCommand(_verticalWalls, moveDirection);
            }
            else
            {
                movementCommand = new MoveWallsCommand(_horizontalWalls, moveDirection);
            }

            if (!_commandExecutor.TryExecuteCommand(movementCommand, OnEndedMoving))
            {
                throw new System.Exception("Can`t execute command");
            }
        }

        private void OnEndedMoving()
        {
            _stateMachine.SwitchState<LevelIdleState>();
        }

    }
}
