using UnityEngine;

namespace LevelSystem
{
    public class CubesMovingStateArgs
    {
        public Vector2 _inputDirection { get; private set; }

        public CubesMovingStateArgs(Vector2 direction) 
        {
            _inputDirection = direction;
        }
    }
}