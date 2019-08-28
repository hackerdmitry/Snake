using System;

namespace Snake
{
    public struct Position
    {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public float Length => (float) Math.Sqrt(X * X + Y * Y);

        public static Position operator +(Position p1, Position p2) => new Position(p1.X + p2.X, p1.Y + p2.Y);

        public static Position operator -(Position p1, Position p2) => new Position(p1.X - p2.X, p1.Y - p2.Y);
        
        public static Position operator *(Position p, int m) => new Position(p.X * m, p.Y * m);

        public static bool operator ==(Position p1, Position p2) => p1.X == p2.X && p1.Y == p2.Y;
        
        public static bool operator !=(Position p1, Position p2) => p1.X != p2.X || p1.Y != p2.Y;
    }
}
