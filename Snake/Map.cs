using System.Collections.Generic;
using System.Drawing;

namespace Snake
{
    public class Map
    {
        static readonly Dictionary<char, IField> legendFields = new Dictionary<char, IField>
        {
            {'w', new Wall()},
            {'s', new Field()},
            {' ', new Field()}
        };
        readonly HashSet<IEntity> entities = new HashSet<IEntity>();

        /// <summary>
        /// Длина карты в клектках
        /// </summary>
        public int Width => fields.GetLength(1);

        /// <summary>
        /// Ширина карты в клектках
        /// </summary>
        public int Height => fields.GetLength(0);

        readonly IField[,] fields;
        int countFreeCells;

        public Map(char[,] charFields)
        {
            fields = new IField[charFields.GetLength(0), charFields.GetLength(1)];
            for (int i = 0; i < charFields.GetLength(0); i++)
            {
                for (int j = 0; j < charFields.GetLength(1); j++)
                {
                    fields[i, j] = legendFields[charFields[i, j]];
                    if (charFields[i, j] == ' ') countFreeCells++;
                    if (charFields[i, j] == 's')
                    {
                        entities.Add(new Snake(this, new Position(j, i)));
                    }
                }
            }
        }

        /// <summary>
        /// Узнать можно ли умереть с полем на данной позиции
        /// </summary>
        /// <param name="position">Данная позиция</param>
        /// <returns>Ответ</returns>
        public bool IsDeadField(Position position) => fields[position.Y, position.X].IsDeadField;

        /// <summary>
        /// Убить существо
        /// </summary>
        /// <param name="entity">Существо</param>
        public void KillEntity(IEntity entity) { entities.Remove(entity); }
        
        /// <summary>
        /// Перерисовка карты
        /// </summary>
        /// <param name="drawing">Реализация рисования</param>
        public void OnPaint(Drawing drawing)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    drawing.DrawImage(fields[i, j].Image, j, i);
                }
            }
            foreach (IEntity entity in entities)
            {
                entity.Move();
                entity.OnPaint(drawing);
            }
        }
    }
}
