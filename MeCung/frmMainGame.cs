using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MeCung
{
    public partial class frmMainGame : Form
    {
        public frmMainGame()
        {
            game = new GameSettings();
            game.startGame();
            Form_Load();
        }

        GameSettings game;
        private Panel[,] _chessBoardPanels;

        // event handler of Form Load... init things here
        private void Form_Load()
        {
            const int tileSize = 100;
            const int gridSize = 5;
            var clr1 = Color.Black; // can't move
            var clr2 = Color.White; // can move
            var clr3 = Color.DarkBlue; // start
            var clr4 = Color.DarkRed; // goal
            int i = 0;

            _chessBoardPanels = new Panel[gridSize, gridSize];

            for (var n = 0; n < gridSize; n++)
            {
                for (var m = 0; m < gridSize; m++)
                {
                     var newPanel = new Panel
                    {
                        Size = new Size(tileSize, tileSize),
                        Location = new Point(tileSize * n, tileSize * m)
                    };

                    Controls.Add(newPanel);

                    _chessBoardPanels[n, m] = newPanel;

                    if (i != game.getSize())
                    {
                        if (game.board[i] == 0)
                        {
                            newPanel.BackColor = clr1;
                        }
                        else if (game.board[i] == 1)
                        {
                            newPanel.BackColor = clr2;
                        }
                        else if (game.board[i] == 2)
                        {
                            newPanel.BackColor = clr3;
                        }
                        else if (game.board[i] == 3)
                        {
                            newPanel.BackColor = clr4;
                        }
                        i++;
                    }
                }
            }
        }


    }
}
