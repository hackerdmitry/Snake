using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using Snake.Properties;

namespace Snake
{
    public class GameManager
    {
        Map map;
        Drawing drawing;
        int targetScore, currentScore;
        static readonly Dictionary<string, string> maps = new Dictionary<string, string>();

        static GameManager()
        {
            ResourceManager rm = levels.ResourceManager;
            ResourceSet rs = rm.GetResourceSet(new CultureInfo("en-US"), true, true);
            if (rs != null)
            {
                IEnumerable<DictionaryEntry> levels = rs.Cast<DictionaryEntry>().Where(x => x.Value is string);
                foreach (DictionaryEntry level in levels)
                    maps.Add((string) level.Key, (string) level.Value);
            }
        }

        public GameManager(string nameLevel)
        {
            string[] lines = maps[nameLevel].Split('\n', '\r').Where(x => !string.IsNullOrEmpty(x)).ToArray();
            targetScore = int.Parse(lines[0]);

            char[,] charFields = new char[lines.Length - 1, lines[1].Length];
            for (int i = 0; i < charFields.GetLength(0); i++)
            {
                for (int j = 0; j < charFields.GetLength(1); j++)
                {
                    charFields[i, j] = lines[i + 1][j];
                }
            }
            map = new Map(charFields);
        }

        public Map GetMap() => map;

        /// <summary>
        /// Перерисовка карты
        /// </summary>
        /// <param name="graphics">Поверхность рисования</param>
        public void OnPaint(Graphics graphics)
        {
            if (drawing == null)
                drawing = new Drawing();
            drawing.SetGraphics(graphics);
            map.OnPaint(drawing);
        }
    }
}
