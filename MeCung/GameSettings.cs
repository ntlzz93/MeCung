using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeCung
{
    public class treeSearch
    {
        public int position {get;set;}
        public int cost { get; set; }
        public int distance { get; set; }

        public List<int> elements;
        public treeSearch link1;
        public treeSearch link2;
        public treeSearch link3;
        public treeSearch link4;
    }

    class GameSettings
    {
        public List<int> board;
        public int getSize()
        {
            return board.Count;
        }
        public GameSettings()
        {

        }

        public void startGame()
        {

        }
        public void createBoard(int row,int column)
        {
            
            Random rdm = new Random();
            board = new List<int>();

            for (int i = 0; i < row * column; i++)
            {
                int num = rdm.Next(0, 2);
                board.Add(num);
            }
            int start = rdm.Next(0, 25);
            int finish = rdm.Next(0, 25);
            if (start != finish)
            {
                board[start] = 2;
                board[finish] = 3;
            }
        }

    }
}
