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
            InitializeComponent();
            listResult = new List<treeSearch>();
            lst = new List<int[,]>();

            game = new GameSettings();
            game.startGame();
            listResult = game.temp2;
            if (listResult != null)
            {
                for (int i = listResult.Count - 1; i >= 0; i--)
                {
                    convert(listResult[i].elements);
                }
                // init map
                state = lst.ElementAt(0);
                Form_Load(pictureBox1, 100, state);
            }
            else
            {
                MessageBox.Show("Không tìm thấy kết quả");
                return;
            }
        }

        GameSettings game;

        // danh sach ket qua tim duoc tu GameSetting
        List<treeSearch> listResult;

        // mang 2 chieu cua ket qua
        List<int[,]> lst;

        int[,] temp;

        // Trang thai di chuyen
        int[,] state = new int[5,5];

        // kich co cua hop
        int size;

        private void convert(List<int> lstTree)
        {
            temp = new int[5, 5];
            int k = 0;
            while (k < lstTree.Count)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        temp[i, j] = lstTree[k];
                        k++;
                    }
                }
            }
            //result = temp;
            lst.Add(temp);
        }

        private void Form_Load(PictureBox pb,int size,int[,] state)
        {
            this.size = size;
            //this.state = state;
            pb.Width = size * state.GetLength(1);
            pb.Height = size * state.GetLength(0);
            pb.Visible = true;
            timer1.Start();
        }

        private void drawTable(Graphics g,int[,] state){
            Color color = new Color();
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    switch (state[i, j])
                    {
                        case 0:
                            color = Color.Black;
                            break;
                        case 1:
                            color = Color.White;
                            break;
                        case 2:
                            color = Color.Blue;
                            break;
                        case 3:
                            color = Color.Red;
                            break;
                    }
                    g.FillRectangle(new SolidBrush(color), new Rectangle(j * size, i * size, size, size));
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            drawTable(g, state);
        }
        int count = 1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (count < lst.Count)
            {
                state = lst[count];
                pictureBox1.Invalidate();
                count++;
            }
            else
            {
                timer1.Stop();
            }
        }

    }
}
