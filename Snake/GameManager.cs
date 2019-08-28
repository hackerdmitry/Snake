using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using Snake.Properties;

namespace Snake
{
    public class GameManager
    {
        public const int INTERVAL_MILLIS_ENTITIES_TIMER = 100;
        Map map;
        Drawing drawing;
        Game game;
        int targetScore, currentScore;
        static readonly Dictionary<string, string> maps = new Dictionary<string, string>();
        static readonly Dictionary<char, Direction> legendDirections = new Dictionary<char, Direction>
        {
            {'u', Direction.Up},
            {'r', Direction.Right},
            {'d', Direction.Down},
            {'l', Direction.Left}
        };
        Direction userDirection = Direction.Right;

        readonly HashSet<IEntity> entities = new HashSet<IEntity>();
        readonly HashSet<ChangeCollection> eventSubscriptions = new HashSet<ChangeCollection>();
        event ChangeCollection ChangeCollectionEntities;

        delegate void ChangeCollection();

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

        public GameManager(string nameLevel, Game game)
        {
            this.game = game;
            string[] lines = maps[nameLevel].Split('\n', '\r').Where(x => !string.IsNullOrEmpty(x)).ToArray();
            targetScore = int.Parse(lines[0]);
            
            int leadupLines = 1;
            while (lines[leadupLines++] != "startLevel") { }

            char[,] charFields = new char[lines.Length - leadupLines, lines[leadupLines].Length];
            for (int i = 0; i < charFields.GetLength(0); i++)
            {
                for (int j = 0; j < charFields.GetLength(1); j++)
                {
                    charFields[i, j] = lines[i + leadupLines][j];
                }
            }
            map = new Map(charFields);

            for (int i = 1; i < leadupLines; i++)
            {
                string[] infoEntity = lines[i].Split();
                switch (infoEntity[0])
                {
                    case "snake":
                        entities.Add(new Snake(map, 
                                               new Position(int.Parse(infoEntity[1]), int.Parse(infoEntity[2])),
                                               int.Parse(infoEntity[3]), legendDirections[infoEntity[4][0]]));
                        break;
                }
            }
            CreateTimerEntities();
        }

        /// <summary>
        /// Поменять направление, куда двигаются все существа
        /// </summary>
        /// <param name="newDirection">Новое направление</param>
        public void ChangeUserDirection(Direction newDirection) { userDirection = newDirection; }

        /// <summary>
        /// Создание таймера для движения всех существ
        /// </summary>
        void CreateTimerEntities()
        {
//            AutoResetEvent autoEvent = new AutoResetEvent(false);
            System.Threading.Timer timer = 
                new System.Threading.Timer(MoveEntities);
            timer.Change(0, INTERVAL_MILLIS_ENTITIES_TIMER);
//            timer.Interval = INTERVAL_ENTITIES_TIMER;
//            timer.Tick += MoveEntities;
//            timer.Start();
        }

        /// <summary>
        /// Убить существо
        /// </summary>
        /// <param name="entity">Существо</param>
        void KillEntity(IEntity entity)
        {
            void ChangeCollection() => entities.Remove(entity);
            eventSubscriptions.Add(ChangeCollection);
            ChangeCollectionEntities += ChangeCollection;
        }

        void ClearEventCollectionEntities()
        {
            foreach (ChangeCollection eventSubscription in eventSubscriptions)
                ChangeCollectionEntities -= eventSubscription;
            eventSubscriptions.Clear();
        }

        void MoveEntities(object sender)
        {
            foreach (IEntity entity in entities.Where(entity => !entity.Move(userDirection)))
                KillEntity(entity);
            if (eventSubscriptions.Count != 0)
            {
                ChangeCollectionEntities();
                ClearEventCollectionEntities();
            }
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
            foreach (IEntity entity in entities) 
                entity.OnPaint(drawing);
        }
    }
}
