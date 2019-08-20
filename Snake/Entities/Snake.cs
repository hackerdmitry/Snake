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

        public Snake(Map map, Position startPosition)
        {
            snakePositions.AddFirst(startPosition);
            this.map = map;
            currentDirection = Direction.Right;
        }

        /// <summary>
        /// Передвинуть змею в сторону куда смотрит змея в данный момент
        /// </summary>
        public void Move() => Move(currentDirection);

        /// <summary>
        /// Передвинуть змею
        /// </summary>
        /// <param name="direction">Направление змеи в зависимости от кнопки на которую нажал пользователь</param>
        public void Move(Direction direction)
        {
            if (Math.Abs(direction - currentDirection) == 1)
                currentDirection = direction;
            snakePositions.AddFirst(snakePositions.Last.Value + Directions.ToPosition(currentDirection));
            snakePositions.RemoveLast();

            if (map.IsDeadField(snakePositions.First.Value) || IsPartSnake(snakePositions.First.Value, false))
                map.KillEntity(this);
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
                if (i == 0)
                    drawing.DrawImage(images.headSnake, snakePosition.Value.X, snakePosition.Value.Y);
                else if (i == snakePositions.Count - 1)
                    drawing.DrawImage(images.tailSnake, snakePosition.Value.X, snakePosition.Value.Y);
                else if (Math.Abs((snakePosition.Previous.Value - snakePosition.Next.Value).Length - 2) < 0.01f)
                    drawing.DrawImage(images.curveBodySnake, snakePosition.Value.X, snakePosition.Value.Y);
                else
                    drawing.DrawImage(images.bodySnake, snakePosition.Value.X, snakePosition.Value.Y);
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
