using UnityEngine;

namespace Utils
{
    public static class WallsMovementDirectionCalculator
    {
        private static readonly Vector2 UpDirection = new Vector2(-0.707f, 0.707f);
        private static readonly Vector2 DownDirection = new Vector2(0.707f, -0.707f);
        private static readonly Vector2 RightDirection = new Vector2(0.707f, 0.707f);
        private static readonly Vector2 LeftDirection = new Vector2(-0.707f, -0.707f);

        private const float Threshold = 0.8f;

        public static bool CalculateMoveDirectionAndWallType(Vector2 direction, out WallType wallType, out MoveDirection moveDirection)
        {
            if (Vector2.Dot(UpDirection, direction) > Threshold)
            {
                wallType = WallType.Vertical;
                moveDirection = MoveDirection.Forward;
                return true;
            }
            else if (Vector2.Dot(DownDirection, direction) > Threshold)
            {
                wallType = WallType.Vertical;
                moveDirection = MoveDirection.Backward;
                return true;
            }
            else if (Vector2.Dot(RightDirection, direction) > Threshold)
            {
                wallType = WallType.Horizontal;
                moveDirection = MoveDirection.Forward;
                return true;
            }
            else if(Vector2.Dot(LeftDirection, direction) > Threshold)
            {
                wallType = WallType.Horizontal;
                moveDirection = MoveDirection.Backward;
                return true;
            }

            wallType = default(WallType);
            moveDirection = default(MoveDirection);

            return false;
        }
    }
}
