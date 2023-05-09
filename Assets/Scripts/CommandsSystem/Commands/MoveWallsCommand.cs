﻿using System.Collections.Generic;
using Wall;
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
        }

        public override void Execute()
        {   
            if(!IsDone) 
            {
                return;
            }

            IsDone = false;

            foreach (var wall in _walls)
            {
                wall.Move(_movementDirection);
                wall.StopedMoving += OnWallStoper;
                _movingWalls.Add(wall);
            }

            _hasExecuted = true;
        }

        public override void Undo()
        {
            if (!IsDone)
            {
                return;
            }

            IsDone = false;

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
                    wall.StopedMoving -= OnWallStoper;
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
                IsDone = true;
                Dispose();
            }
        }

        private void OnWallStoper(MoveableWall wall)
        {
            _movingWalls.Remove(wall);

            if(_movingWalls.Count == 0)
            {
                IsDone = true;
                Dispose();
            }
        }
    }
}
