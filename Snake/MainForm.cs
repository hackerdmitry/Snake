using System;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    public class MainForm : Form
    {
        const int WIDTH_FORM = 500, HEIGHT_FORM = 300;
        const int TOP_PADDING_FORM = 30, BOTTOM_PADDING_FORM = 30, BETWEEN_BUTTONS = 20;
        const int WIDTH_BUTTON = 75, HEIGHT_BUTTON = 40;

        readonly int heightHeader;

        public MainForm()
        {
            heightHeader = GetHeightHeader();

            Control[] buttons = GetButtons();
            Size = new Size(WIDTH_FORM,
                            buttons[buttons.Length - 1].Top + HEIGHT_BUTTON + BOTTOM_PADDING_FORM + heightHeader);
            StartPosition = FormStartPosition.CenterScreen;
            Controls.AddRange(buttons);
            Select();
        }

        /// <summary>
        /// Получаем размер шапки для того, чтобы потом ровно выбрать размер окна по высоте
        /// </summary>
        /// <returns>Высота шапки</returns>
        int GetHeightHeader()
        {
            Size = new Size(1, 1);
            return Size.Height;
        }

        /// <summary>
        /// Получение кнопок для отображения их на этой форме
        /// Все кнопки идут по порядку с отступами сверху и снизу и отступами между кнопками, которые задаются константами
        /// </summary>
        /// <returns>Кнопки на форме</returns>
        Control[] GetButtons()
        {
            Button startGame = new Button {Text = "Start Game"};
            Button options = new Button {Text = "Options"};
            Button quit = new Button {Text = "Quit"};

            startGame.Click += ActionClickStartGame;
            options.Click += ActionClickOption;
            quit.Click += ActionClickQuit;

            Button[] allButtons = {startGame, options, quit};

            for (int i = 0; i < allButtons.Length; i++)
            {
                allButtons[i].FlatStyle = FlatStyle.Flat;
                allButtons[i].Size = new Size(WIDTH_BUTTON, HEIGHT_BUTTON);
                allButtons[i].Top = TOP_PADDING_FORM + (BETWEEN_BUTTONS + HEIGHT_BUTTON) * i;
                allButtons[i].Left = (WIDTH_FORM - WIDTH_BUTTON) / 2;
            }

            return allButtons;
        }

        /// <summary>
        /// Действие при нажатии на кнопку "StartGame"
        /// </summary>
        void ActionClickStartGame(object obj, EventArgs eventArgs) { throw new NotImplementedException(); }

        /// <summary>
        /// Действие при нажатии на кнопку "Option"
        /// </summary>
        void ActionClickOption(object obj, EventArgs eventArgs) { throw new NotImplementedException(); }

        /// <summary>
        /// Действие при нажатии на кнопку "Quit"
        /// </summary>
        void ActionClickQuit(object obj, EventArgs eventArgs) { Close(); }
    }
}
