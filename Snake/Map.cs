using System.Collections.Generic;
using System.Drawing;

namespace Snake
{
    public class Map
    {
        static readonly Dictionary<char, IField> legendFields = new Dictionary<char, IField>
        {
            {'w', new Wall()},
            {' ', new Field()}
        };


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
                for (int j = 0; j < charFields.GetLength(1); j++)
                {
                    fields[i, j] = legendFields[charFields[i, j]];
                    if (charFields[i, j] == ' ') 
                        countFreeCells++;
                }
        }

        /// <summary>
        /// Узнать можно ли умереть с полем на данной позиции
        /// </summary>
        /// <param name="position">Данная позиция</param>
        /// <returns>Ответ</returns>
        public bool IsDeadField(Position position) => fields[position.Y, position.X].IsDeadField;

        Bitmap bitmaker;

        /// <summary>
        /// Перерисовка карты
        /// </summary>
        /// <param name="drawing">Реализация рисования</param>
        public void OnPaint(Drawing drawing)
        {
            if (bitmaker == null)
            {
                bitmaker = new Bitmap(Width * Game.LENGTH_SIDE, Height * Game.LENGTH_SIDE);
                using (Graphics g = Graphics.FromImage(bitmaker))
                {
                    Drawing gDrawing = new Drawing();
                    gDrawing.SetGraphics(g);
                    for (int i = 0; i < Height; i++)
                        for (int j = 0; j < Width; j++)
                            gDrawing.DrawImage(fields[i, j].Image, j, i);
                }
            }
            drawing.GetGraphics().DrawImage(bitmaker, 0, 0);
        }
    }
}
