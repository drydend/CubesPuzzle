using UnityEngine;

namespace Utils
{
    public static class WallsMovementDirectionCalculator
    {
        private const float Threshold = 0.8f;

        public static MoveDirection CalculateMoveDirectionAndWallType(Vector2 direction, out WallType wallType)
        {
            if (Vector2.Dot(Vector2.up, direction) < Threshold)
            {
                wallType = WallType.Vertical;
                return MoveDirection.Forward;
            }
            else if (Vector2.Dot(Vector2.down, direction) < Threshold)
            {
                wallType = WallType.Vertical;
                return MoveDirection.Backward;
            }
            else if (Vector2.Dot(Vector2.right, direction) < Threshold)
            {
                wallType = WallType.Horizontal;
                return MoveDirection.Forward;
            }
            else
            {
                wallType = WallType.Horizontal;
                return MoveDirection.Backward;
            }
        }
    }
}
