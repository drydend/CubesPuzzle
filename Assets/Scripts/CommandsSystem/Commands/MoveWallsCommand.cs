using System.Collections.Generic;
using WallsSystem;
using UnityEngine;
using System;
using System.Linq;

namespace CommandsSystem
{
    public class MoveWallsCommand : Command, IDisposable
    {
        private List<MoveableWall> _walls;
        private MoveDirection _movementDirection;

        private List<Vector3> _startPositions;

        private List<MoveableWall> _movingWalls = new List<MoveableWall>();

        private bool _hasExecuted;
        private bool _hasUndo;

        public MoveWallsCommand(List<MoveableWall> walls, MoveDirection moveDirection)
        {
            _walls = walls;
            _movementDirection = moveDirection;
            _startPositions = walls.Select(wall => wall.transform.position).ToList();
            IsReady = true;
        }

        public override void Execute()
        {
            if (!IsReady)
            {
                return;
            }

            IsReady = false;

            foreach (var wall in _walls)
            {
                if (wall.CanMoveInDirection(_movementDirection))
                {
                    wall.Move(_movementDirection);
                    wall.ReachedDesiredPosition += OnWallReachedDesiredPosition;
                    _movingWalls.Add(wall);
                }
            }

            _hasExecuted = true;

            if(_movingWalls.Count == 0)
            {
                IsReady = true;
            }
        }

        public override void Undo()
        {
            if (!IsReady)
            {
                return;
            }

            IsReady = false;

            for (int i = 0; i < _walls.Count; i++)
            {
                _walls[i].MoveTo(_startPositions[i]);
                _walls[i].ReachedDesiredPosition += OnWallReachedDesiredPosition;
                _movingWalls.Add(_walls[i]);
            }

            _hasUndo = true;
        }

        public void Dispose()
        {
            if (_hasExecuted)
            {
                foreach (var wall in _walls)
                {
                    wall.ReachedDesiredPosition -= OnWallReachedDesiredPosition;
                }
            }

            if (_hasUndo)
            {
                foreach (var wall in _walls)
                {
                    wall.ReachedDesiredPosition -= OnWallReachedDesiredPosition;
                }
            }
        }

        private void OnWallReachedDesiredPosition(MoveableWall wall)
        {
            _movingWalls.Remove(wall);

            if (_movingWalls.Count == 0)
            {
                IsReady = true;
                Dispose();
            }
        }

        public override void Stop()
        {
            foreach (var wall in _movingWalls)
            {
                wall.StopMovingImidiatly();
            }

            IsReady = true;
            Dispose();
        }
    }
}
