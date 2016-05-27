using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeCung
{
    public class treeSearch
    {
        public int position {get;set;}
        public int posStart { get; set; }
        public int posGoal { get; set; }
        public int cost { get; set; }
        public int distance { get; set; }
        public treeSearch()
        {
            elements = new List<int>();
            
        }
        public List<int> elements;
        public treeSearch link1 ;
        public treeSearch link2 ;
        public treeSearch link3 ;
        public treeSearch link4 ;
        public treeSearch parent ;
    }

    class GameSettings
    {
        #region Variable
        public List<int> board;
        private List<int> initState;
        private List<int> goalState;
        private treeSearch goalStateNode;
        private List<treeSearch> States; // danh sach cac trang thai
        private treeSearch root; // trang thai parent
        int posStart, posGoal = 0; // vi tri cua trang thai dau va cuoi.
        bool success = false;

        int row = 5, col = 5;
        public int getSize()
        {
            return board.Count;
        }
        public GameSettings()
        {
            board = new List<int>();
            initState = new List<int>();
            goalState = new List<int>();
            States = new List<treeSearch>();
            root = new treeSearch();
            goalStateNode = new treeSearch();

        }
        #endregion

        #region Start Game
        public void startGame()
        {
            //createBoard(row, col) ;
            makeRoot(initState);
            success = false;
            treeSearch currentNode = new treeSearch();

            while (States.Count > 0)
            {
                currentNode = States.ElementAt(0);
                States.RemoveAt(0);
                insertNodeTree(currentNode);
            }
        }
        
        public void createInitState(List<int> state)
        {
            for (int i = 0; i < state.Count; i++)
            {
                initState.Add(state[i]);
            }
        }
        public void createGoalState(List<int> state) 
        {
            for (int i = 0; i < state.Count; i++)
            {
                goalState.Add(state[i]);
                goalStateNode.elements.Add(state[i]);
                if (goalStateNode.elements[i] == 2)
                {
                    goalStateNode.posStart = i;
                }
                else if (goalStateNode.elements[i] == 3)
                {
                    goalStateNode.posGoal = i;
                }
            }
            goalState[posGoal] = goalState[posStart];
            goalState[posStart] = 1;
           
        }
        #endregion

        #region Self-Create Board Game
        public void createBoard(int row,int column)
        {
            
            
        }
        #endregion

        #region Create Random Board
        public void createRandomBoard(int row, int column)
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
                posStart = start;
                posGoal = finish;
            }

            createInitState(board);
            createGoalState(board);
        }
        #endregion

        #region Main Logic Game
        public void makeRoot(List<int> state)
        {
            treeSearch rootNode = new treeSearch();
            for (int i = 0; i < state.Count; i++)
            {
                //rootNode.elements[i] = state[i];
                rootNode.elements.Add(state[i]);
                if (rootNode.elements[i] == 2)
                {
                    rootNode.position = i;
                    rootNode.posStart = i;
                }
                else if (rootNode.elements[i] == 3)
                {
                    rootNode.posGoal = i;
                }
            }

            rootNode.distance = 0;
            rootNode.cost = getCost(rootNode);
            rootNode.link1 = null; rootNode.link2 = null; rootNode.link3 = null; rootNode.link4 = null;
            rootNode.parent = null;

            root = rootNode;
            States.Add(root);
            
        }
        public void changeValueAfterExit(treeSearch currentNode)
        {
            if (currentNode.position == currentNode.posGoal)
            {
                currentNode.elements[posGoal] = 2;
                currentNode.elements[currentNode.parent.posStart] = 1;
            }
            else
            {
                return;
            }
        }
        public void insertNodeTree(treeSearch node)
        {
            treeSearch firstNode = new treeSearch();
            makeMove(node, firstNode, 'U');
            if (firstNode.elements != null)
            {
                firstNode.position = firstNode.posStart;
                firstNode.distance = node.distance + 1;
                firstNode.cost = getCost(firstNode) + firstNode.distance;
                firstNode.link1 = null; firstNode.link2 = null; firstNode.link3 = null; firstNode.link4 = null;
                firstNode.parent = node;
                changeValueAfterExit(firstNode);
            }
            

            treeSearch secondNode = new treeSearch();
            makeMove(node, secondNode, 'R');
            if (secondNode.elements != null)
            {
                secondNode.position = secondNode.posStart;
                secondNode.distance = node.distance + 1;
                secondNode.cost = getCost(secondNode) + secondNode.distance;
                secondNode.link1 = null; secondNode.link2 = null; secondNode.link3 = null; secondNode.link4 = null;
                secondNode.parent = node;
                changeValueAfterExit(secondNode);
            }
           

            treeSearch thirdNode = new treeSearch();
            makeMove(node, thirdNode, 'D');
            if (thirdNode.elements != null)
            {
                thirdNode.position = thirdNode.posStart;
                thirdNode.distance = node.distance + 1;
                thirdNode.cost = getCost(thirdNode) + thirdNode.distance;
                thirdNode.link1 = null; thirdNode.link2 = null; thirdNode.link3 = null; thirdNode.link4 = null;
                thirdNode.parent = node;
                changeValueAfterExit(thirdNode);
            }
            

            treeSearch fouthNode = new treeSearch();
            makeMove(node, fouthNode, 'L');
            if (fouthNode.elements != null)
            {
                fouthNode.position = fouthNode.posStart;
                fouthNode.distance = node.distance + 1;
                fouthNode.cost = getCost(fouthNode)+fouthNode.distance;
                fouthNode.link1 = null; fouthNode.link2 = null; fouthNode.link3 = null; fouthNode.link4 = null;
                fouthNode.parent = node;
                changeValueAfterExit(fouthNode);
            }
            

            if (node.link1 == null && firstNode.elements != null)
            {
                node.link1 = firstNode;
                if (!success) States.Add(node.link1);
            }
            else
            {
                firstNode = null;
            }

            if (node.link2 == null && secondNode.elements != null)
            {
                node.link2 = secondNode;
                if (!success) States.Add(node.link2);
            }
            else
            {
                secondNode = null;
            }

            if (node.link3 == null && thirdNode.elements != null)
            {
                node.link3 = thirdNode;
                if (!success) States.Add(node.link3);
            }
            else
            {
                thirdNode = null;
            }

            if (node.link4 == null && fouthNode.elements != null)
            {
                node.link4 = fouthNode;
                if (!success) States.Add(node.link4);
            }
            else
            {
                fouthNode = null;
            }

            // sort
            sortState();
            // check success
            checkSuccess(node);

        }
        public void sortState()
        {
            treeSearch temp;
            for (int i = 0; i < States.Count -1 ; i++)
            {
                for (int j = 0; j < States.Count -1; j++)
                {
                    if (States[i].cost > States[j + 1].cost)
                    {
                        temp = States[i];
                        States[i] = States[j + 1];
                        States[j + 1] = temp;
                    }
                }
            }
        }
        public void checkSuccess(treeSearch state)
        {
            bool success1 = true, success2 = true, success3 = true, success4 = true;
            
                if (state.link1 != null)
                {
                    if (state.link1.position != goalStateNode.posGoal)
                    {
                        success1 = false;
                    }
                }
                else
                {
                    success1 = false;
                }

                if (state.link2 != null)
                {
                    if (state.link2.position != goalStateNode.posGoal)
                    {
                        success2 = false;
                    }
                }
                else
                {
                    success2 = false;
                }

                if (state.link3 != null)
                {
                    if (state.link3.position != goalStateNode.posGoal)
                    {
                        success3 = false;
                    }
                }
                else
                {
                    success3 = false;
                }

                if (state.link4 != null)
                {
                    if (state.link4.position != goalStateNode.posGoal)
                    {
                        success4 = false;
                    }
                }
                else
                {
                    success4 = false;
                }


            if (success1)
            {
                success = true;
                printResultConsole(state.link1);
            }
            else if (success2)
            {
                success = true;
                printResultConsole(state.link2);
            }
            else if (success3)
            {
                success = true;
                printResultConsole(state.link3);
            }
            else if (success4)
            {
                success = true;
                printResultConsole(state.link4);
            }
            if (success)
            {
                while (States.Count > 0)
                {
                    States.RemoveAt(0);
                }
            }
        }
        List<treeSearch> temp1 = new List<treeSearch>();
        public void printResultConsole(treeSearch node)
        {
            while (node != null)
            {
                temp1.Add(node);
                node = node.parent;
            }

            for (int j = temp1.Count - 1; j >= 0; j--)
            {
                for (int i = 0; i < temp1[j].elements.Count; i++)
                {
                    System.Console.Write(temp1[j].elements[i] + " ");
                    if (i % 5 == 4)
                    {
                        System.Console.WriteLine();
                    }
                }
                System.Console.WriteLine();
            }
        }
        public void makeMove(treeSearch parentNode, treeSearch currentNode, char director)
        {
            for (int i = 0; i < parentNode.elements.Count; i++)
            {
                currentNode.elements.Add(parentNode.elements[i]);
                if (currentNode.elements[i] == 2)
                {
                    currentNode.position = i;
                    currentNode.posStart = i;
                }
                else if (currentNode.elements[i] == 3)
                {
                    currentNode.posGoal = i;
                }
            }

            int temp;
            switch (director)
            {
                case 'U':
                    if (currentNode.position != 0 && currentNode.position != 1 && currentNode.position != 2 && currentNode.position != 3 && currentNode.position != 4)
                    {
                        int pos = currentNode.position;

                        if (currentNode.elements[pos - 5] != 0)
                        {
                            temp = currentNode.elements[pos];
                            currentNode.elements[pos] = currentNode.elements[pos - 5];
                            currentNode.elements[pos - 5] = temp;
                            currentNode.posStart = pos - 5;
                        }
                        else
                        {
                            currentNode.elements = null;
                        }
                    }
                    else
                    {
                        currentNode.elements = null;
                    }
                    break;
                case 'R':
                    if(currentNode.position != 4 && currentNode.position != 9 && currentNode.position != 14 && currentNode.position != 19 && currentNode.position != 24)
                    {
                        int pos = currentNode.position;
                        if (currentNode.elements[pos + 1] != 0)
                        {
                            temp = currentNode.elements[pos];
                            currentNode.elements[pos] = currentNode.elements[pos + 1];
                            currentNode.elements[pos + 1] = temp;
                            currentNode.posStart = pos + 1;
                        }
                        else
                        {
                            currentNode.elements = null;
                        }
                    }
                    else
                    {
                        currentNode.elements = null;
                    }
                    break;
                case 'D':
                    if (currentNode.position != 20 && currentNode.position != 21 && currentNode.position != 22 && currentNode.position != 23 && currentNode.position != 24)
                    {
                        int pos = currentNode.position;
                        if (currentNode.elements[pos + 5] != 0)
                        {
                            temp = currentNode.elements[pos];
                            currentNode.elements[pos] = currentNode.elements[pos + 5];
                            currentNode.elements[pos + 5] = temp;
                            currentNode.posStart = pos + 5;
                        }
                        else
                        {
                            currentNode.elements = null;
                        }
                    }
                    else
                    {
                        currentNode.elements = null;
                    }
                    break;
                case 'L':
                    if (currentNode.position != 0 && currentNode.position != 5 && currentNode.position != 10 && currentNode.position != 15 && currentNode.position != 20)
                    {
                        int pos = currentNode.position;
                        if (currentNode.elements[pos - 1] != 0)
                        {
                            temp = currentNode.elements[pos];
                            currentNode.elements[pos] = currentNode.elements[pos - 1];
                            currentNode.elements[pos - 1] = temp;
                            currentNode.posStart = pos - 1;
                        }
                        else
                        {
                            currentNode.elements = null;
                        }
                    }
                    else
                    {
                        currentNode.elements = null;
                    }
                    break;
                default:

                    break;
            }
        }
        public int getCost(treeSearch state)
        {
            int cost = 0;
            int a, b;
            //for (int i = 0; i < state.elements.Count; i++)
            //{
            //    if (state.elements[i] != goalState[i])
            //    {
            //        cost++;
            //    }
            //}
            if (state.posStart > state.posGoal)
            {
                a = state.posStart;
                b = state.posGoal;
            }
            else
            {
                a = posGoal;
                b = posStart;
            }
            cost = a - b;
            return cost;
        }

        public bool isEmpty()
        {
            return root.elements.Count > 0;
        }
#endregion
    }
}
