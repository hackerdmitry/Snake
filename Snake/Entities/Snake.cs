using System;
using System.Collections.Generic;
using Snake.Properties;

namespace Snake
{
    public class Snake : IEntity
    {
        readonly Map map;
        readonly LinkedHashList<Position> snakePositions = new LinkedHashList<Position>();
        Direction currentDirection;
        int startLength; // Начальная длина змеи

        public Snake(Map map, Position startPosition, int length = 1)
        {
            snakePositions.AddFirst(startPosition);
            this.map = map;
            currentDirection = Direction.Left;
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
            if (Math.Abs(direction - currentDirection) == 1)
                currentDirection = direction;

            Position newHeadPos = snakePositions.First.Value + Directions.ToPosition(currentDirection);
            if (map.IsDeadField(newHeadPos) || IsPartSnake(newHeadPos, false))
            {
                map.KillEntity(this);
                return false;
            }
            
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
            LinkedListNode<Position> snakePosition = snakePositions.First;
            for (int i = 0; i < snakePositions.Count; i++)
            {
                int valueX = snakePosition.Value.X, valueY = snakePosition.Value.Y;
                if (i == 0)
                    drawing.DrawImage(images.headSnake, valueX, valueY, currentDirection);
                else if (i == snakePositions.Count - 1)
                    drawing.DrawImage(images.tailSnake, valueX, valueY, currentDirection);
                else if (Math.Abs((snakePosition.Previous.Value - snakePosition.Next.Value).Length - 2) < 0.01f)
                    drawing.DrawImage(images.bodySnake, valueX, valueY, currentDirection);
                else
                    drawing.DrawImage(images.curveBodySnake, valueX, valueY, currentDirection);
                snakePosition = snakePosition.Next;
            }
        }

        class LinkedHashList<T>
        {
            readonly LinkedList<T> linkedList = new LinkedList<T>();
            readonly HashSet<T> hashSet = new HashSet<T>();

            public int Count => linkedList.Count;

            public LinkedListNode<T> First => linkedList.First;

            public LinkedListNode<T> Last => linkedList.Last;

            public bool Contains(T item) => hashSet.Contains(item);

            public LinkedListNode<T> AddFirst(T value)
            {
                hashSet.Add(value);
                return linkedList.AddFirst(value);
            }

            public LinkedListNode<T> AddLast(T value)
            {
                hashSet.Add(value);
                return linkedList.AddLast(value);
            }

            public void RemoveFirst()
            {
                hashSet.Remove(linkedList.First.Value);
                linkedList.RemoveFirst();
            }

            public void RemoveLast()
            {
                hashSet.Remove(linkedList.Last.Value);
                linkedList.RemoveLast();
            }
        }
    }
}
