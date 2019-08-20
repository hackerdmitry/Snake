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
            }
            throw new NotImplementedException();
        }
    }
}
