using System;
using System.Collections.Generic;
using System.Linq;
using Snake.Properties;

namespace Snake
{
    public class Snake : IEntity
    {
        readonly Map map;
        readonly LinkedHashList<Position> snakePositions = new LinkedHashList<Position>();
        Direction currentDirection;
        int startLength; // Начальная длина змеи

        public Snake(Map map, Position startPosition, int length = 1, Direction direction = Direction.Right)
        {
            snakePositions.AddFirst(startPosition);
            this.map = map;
            currentDirection = direction;
            startLength = length;
        }

        /// <summary>
        /// Передвинуть змею в сторону куда смотрит змея в данный момент
        /// </summary>
        /// <returns>Смог передвинуть змею без ее смерти</returns>
        public bool Move() => Move(currentDirection);

        /// <summary>
        /// Передвинуть змею
        /// </summary>
        /// <param name="direction">Направление змеи в зависимости от кнопки на которую нажал пользователь</param>
        /// <returns>Смог передвинуть змею без ее смерти</returns>
        public bool Move(Direction direction)
        {
            if (Math.Abs(direction - currentDirection + 4) % 2 == 1)
                currentDirection = direction;

            Position newHeadPos = snakePositions.First.Value + Directions.ToPosition(currentDirection);
            if (map.IsDeadField(newHeadPos) || IsPartSnake(newHeadPos, false))
                return false;

            snakePositions.AddFirst(newHeadPos);
            if (startLength < 2)
                snakePositions.RemoveLast();
            else
                startLength--;
            return true;
        }

        /// <summary>
        /// Узнать является ли данная позиция частью змеи
        /// </summary>
        /// <param name="position">Данная позиция</param>
        /// <param name="considerHead">Учитывать голову</param>
        /// <returns>Ответ на вопрос</returns>
        public bool IsPartSnake(Position position, bool considerHead = true) =>
            snakePositions.Contains(position) && !(considerHead ^ snakePositions.First.Value == position);

        public void OnPaint(Drawing drawing)
        {
            List<Position> copySnakePositions = snakePositions.LinkedList.ToList();
            for (int i = 0; i < copySnakePositions.Count; i++)
            {
                Direction directionForImage = i == 0
                    ? currentDirection
                    : Directions.ToDirection(copySnakePositions[i - 1] - copySnakePositions[i]);
                float valueX = copySnakePositions[i].X,
                      valueY = copySnakePositions[i].Y;
                if (i == 0)
                    drawing.DrawImage(images.headSnake, valueX, valueY, directionForImage);
                else if (i == copySnakePositions.Count - 1)
                    drawing.DrawImage(images.tailSnake, valueX, valueY, directionForImage);
                else if (Math.Abs((copySnakePositions[i - 1] - copySnakePositions[i + 1]).Length - 2) < 0.01f)
                    drawing.DrawImage(images.bodySnake, valueX, valueY, directionForImage);
                else
                {
                    Position sumPos = copySnakePositions[i - 1] + copySnakePositions[i + 1] - copySnakePositions[i] * 2;
                    directionForImage = GetCurveDirectionForImage(sumPos);
                    drawing.DrawImage(images.curveBodySnake, valueX, valueY, directionForImage);
                }
            }
        }

        static long GetCurrentTimeTicks() => DateTime.Now.Ticks;

        static Direction GetCurveDirectionForImage(Position sumPos)
        {
            switch (sumPos.X)
            {
                case 1 when sumPos.Y == -1:
                    return Direction.Left;
                case -1 when sumPos.Y == -1:
                    return Direction.Down;
                case -1 when sumPos.Y == 1:
                    return Direction.Right;
                case 1 when sumPos.Y == 1:
                    return Direction.Up;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sumPos), $"({sumPos.X}, {sumPos.Y})", null);
            }
        }

        class LinkedHashList<T>
        {
            public LinkedList<T> LinkedList { get; } = new LinkedList<T>();
            public HashSet<T> HashSet { get; } = new HashSet<T>();

            public int Count => LinkedList.Count;

            public LinkedListNode<T> First => LinkedList.First;

            public LinkedListNode<T> Last => LinkedList.Last;

            public bool Contains(T item) => HashSet.Contains(item);

            public LinkedListNode<T> AddFirst(T value)
            {
                HashSet.Add(value);
                return LinkedList.AddFirst(value);
            }

            public LinkedListNode<T> AddLast(T value)
            {
                HashSet.Add(value);
                return LinkedList.AddLast(value);
            }

            public void RemoveFirst()
            {
                HashSet.Remove(LinkedList.First.Value);
                LinkedList.RemoveFirst();
            }

            public void RemoveLast()
            {
                HashSet.Remove(LinkedList.Last.Value);
                LinkedList.RemoveLast();
            }
        }
    }
}
