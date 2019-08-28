using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Snake
{
    public class Game : Form
    {
        public const int LENGTH_SIDE = 32;
        const int TIMER_INTERVAL = 16; // Интервал отрисовки графики

        readonly int heightHeader;
        readonly GameManager gameManager;
        Timer timer;

        public Game()
        {
            heightHeader = GetCalculatedHeightHeader();

            CreateTimer();

            gameManager = new GameManager("level1", this);
            TuneWindowForm();
        }

        /// <summary>
        /// Получаем размер шапки
        /// </summary>
        /// <returns>Высота шапки</returns>
        public int GetHeightHeader() => heightHeader;

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.W:
                    gameManager.ChangeUserDirection(Direction.Up);
                    break;
                case Keys.A:
                    gameManager.ChangeUserDirection(Direction.Left);
                    break;
                case Keys.S:
                    gameManager.ChangeUserDirection(Direction.Down);
                    break;
                case Keys.D:
                    gameManager.ChangeUserDirection(Direction.Right);
                    break;
            }
        }

        /// <summary>
        /// Настроить текущее окно
        /// </summary>
        void TuneWindowForm()
        {
            Size = new Size(gameManager.GetMap().Width * LENGTH_SIDE + 16,
                            gameManager.GetMap().Height * LENGTH_SIDE + heightHeader);
            StartPosition = FormStartPosition.CenterScreen;
            DoubleBuffered = true;
        }

        /// <summary>
        /// Рассчитать размер шапки для того, чтобы потом ровно выбрать размер окна по высоте
        /// </summary>
        /// <returns>Высота шапки</returns>
        int GetCalculatedHeightHeader()
        {
            Size = new Size(1, 1);
            return Size.Height;
        }

        /// <summary>
        /// Создание таймера, который вызывает перерисовку окна
        /// </summary>
        void CreateTimer()
        {
            timer = new Timer();
            timer.Interval = TIMER_INTERVAL;
            timer.Enabled = true;
            timer.Tick += (o, e) => Invalidate();
//            graphics = CreateGraphics();
//            timer.Tick += (o, e) => gameManager.OnPaint(graphics);
            timer.Start();
        }

        public void AddTick(EventHandler tick) { timer.Tick += tick; }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Owner.Show();
        }

        Graphics graphics;

        protected override void OnPaint(PaintEventArgs e)
        {
            gameManager.OnPaint(e.Graphics);
            e.Graphics.ResetTransform();
        }
    }
}
