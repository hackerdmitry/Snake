using System;
using System.Drawing;

namespace Snake
{
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    public static class Directions
    {
        public static Position ToPosition(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Position(0, -1);
                case Direction.Right:
                    return new Position(1, 0);
                case Direction.Down:
                    return new Position(0, 1);
                case Direction.Left:
                    return new Position(-1, 0);
                default:
                    throw new NotImplementedException();
            }
        }
        
        public static Direction ToDirection(Position position)
        {
            switch (position.X) {
                case 0 when position.Y == -1:
                    return Direction.Up;
                case 1 when position.Y == 0:
                    return Direction.Right;
                case 0 when position.Y == 1:
                    return Direction.Down;
                case -1 when position.Y == 0:
                    return Direction.Left;
                default:
                    throw new ArgumentOutOfRangeException(nameof(position), position, null);
            }
        }
    }
}
