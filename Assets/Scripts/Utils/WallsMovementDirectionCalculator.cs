using UnityEngine;

namespace Utils
{
    public static class WallsMovementDirectionCalculator
    {
        private const float Threshold = 0.8f;

        private static readonly Vector2 UpLeftDirection = new Vector2(-0.707f, 0.707f);
        private static readonly Vector2 UpRightDirection = new Vector2(0.707f, 0.707f);
        private static readonly Vector2 DownRightDirection = new Vector2(0.707f, -0.707f);
        private static readonly Vector2 DownLeftDirection = new Vector2(-0.707f, -0.707f);

        private static readonly Vector2 UpDirection = Vector2.up;
        private static readonly Vector2 DownDirection = Vector2.down;
        private static readonly Vector2 RightDirection = Vector2.right;
        private static readonly Vector2 LeftDirection = Vector2.left;


        public static bool TryCalculateMoveDirectionAndWallType(Vector2 direction, out WallType wallType, out MoveDirection moveDirection)
        {
            if (Vector2.Dot(UpDirection, direction) > Threshold || Vector2.Dot(UpLeftDirection, direction) > Threshold)
            {
                wallType = WallType.Vertical;
                moveDirection = MoveDirection.Forward;
                return true;
            }
            else if (Vector2.Dot(DownDirection, direction) > Threshold || Vector2.Dot(DownRightDirection, direction) > Threshold)
            {
                wallType = WallType.Vertical;
                moveDirection = MoveDirection.Backward;
                return true;
            }
            else if (Vector2.Dot(RightDirection, direction) > Threshold || Vector2.Dot(UpRightDirection, direction) > Threshold)
            {
                wallType = WallType.Horizontal;
                moveDirection = MoveDirection.Forward;
                return true;
            }
            else if(Vector2.Dot(LeftDirection, direction) > Threshold || Vector2.Dot(DownLeftDirection, direction) > Threshold)
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
