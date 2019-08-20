using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    public class Game : Form
    {
        public const int LENGTH_SIDE = 32;
        const int TIMER_INTERVAL = 200;

        readonly int heightHeader;
        readonly GameManager gameManager;

        public Game()
        {
            heightHeader = GetCalculatedHeightHeader();

            CreateTimer();

            gameManager = new GameManager("level1");
            TuneWindowForm();
        }

        /// <summary>
        /// Получаем размер шапки
        /// </summary>
        /// <returns>Высота шапки</returns>
        public int GetHeightHeader() => heightHeader;

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
            Timer timer = new Timer();
            timer.Interval = TIMER_INTERVAL;
            timer.Enabled = true;
            timer.Tick += (o, e) => Invalidate();
            timer.Start();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Owner.Show();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            gameManager.OnPaint(e.Graphics);
        }
    }
}
