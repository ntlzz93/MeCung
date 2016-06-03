using System;
using System.Collections.Generic;
using System.IO;
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
        public treeSearch link5;
        public treeSearch link6;
        public treeSearch link7;
        public treeSearch link8;
        public treeSearch parent ;
    }

    class GameSettings
    {
        #region Variable
        private List<int> board;
        private List<int> initState;
        private List<int> goalState;
        private treeSearch goalStateNode;
        private List<treeSearch> States; // danh sach cac trang thai
        private treeSearch root; // trang thai parent
        int posStart, posGoal = 0; // vi tri cua trang thai dau va cuoi.
        bool success = false;

        #region Create Map Additional

        private List<int> map;
        private List<List<int>> lstMap;
        private int counter = 0;

        private string fileName = "D:\\Study\\IT\\Artificial Intelligence\\Source Code\\MeCung\\MeCung\\map.txt";

        public void createMap()
        {
            var fileContent = File.ReadAllText(fileName);
            var stringArray = fileContent.Split((string[])null, StringSplitOptions.RemoveEmptyEntries);
            var numbersArray = stringArray.Select(arg => int.Parse(arg)).ToList();

            for (int i = 0; i < numbersArray.Count; i++)
            {
                int number = numbersArray[i];
                map.Add(number);
                counter++;
                if (counter == row * col)
                {
                    lstMap.Add(map);
                    map = new List<int>();
                    counter = 0;
                }
            }
        }

        #endregion
        const int row = 5, col = 5;
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
            map = new List<int>();
            lstMap = new List<List<int>>();

            createMap();
        }
        #endregion

        #region Start Game
        public void startGame()
        {
            createRandomBoard(row, col);
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

        #region Create Random Board
        public void createRandomBoard(int row, int column)
        {
            Random rdm = new Random();
            board = new List<int>();

            //for (int i = 0; i < row * column; i++)
            //{
            //    int num = rdm.Next(0, 2);
            //    board.Add(num);
            //}
            //int start = rdm.Next(0, 25);
            //int finish = rdm.Next(0, 25);
            //if (start != finish)
            //{
            //    board[start] = 2;
            //    board[finish] = 3;
            //    posStart = start;
            //    posGoal = finish;
            //}

            int size = lstMap.Count;
            int num = rdm.Next(0, size+1);
            board = lstMap.ElementAt(num);

            for (int i = 0; i < board.Count; i++)
            {
                if (board[i] == 2)
                {
                    posStart = i;
                }
                else if (board[i] == 3)
                {
                    posGoal = i;
                }
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
            rootNode.link5 = null; rootNode.link6 = null; rootNode.link7 = null; rootNode.link8 = null;
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
            treeSearch fifthNode = new treeSearch();
            makeMove(node,fifthNode,"UR");
            if (fifthNode.elements != null)
            {
                fifthNode.position = fifthNode.posStart;
                fifthNode.distance = node.distance + 1;
                fifthNode.cost = getCost(fifthNode) + fifthNode.distance;
                fifthNode.link1 = null; fifthNode.link2 = null; fifthNode.link3 = null; fifthNode.link4 = null;
                fifthNode.link5 = null; fifthNode.link6 = null; fifthNode.link7 = null; fifthNode.link8 = null;
                fifthNode.parent = node;
                changeValueAfterExit(fifthNode);
            }

            treeSearch sixthNode = new treeSearch();
            makeMove(node, sixthNode, "DR");
            if (sixthNode.elements != null)
            {
                sixthNode.position = sixthNode.posStart;
                sixthNode.distance = node.distance + 1;
                sixthNode.cost = getCost(sixthNode) + sixthNode.distance;
                sixthNode.link1 = null; sixthNode.link2 = null; sixthNode.link3 = null; sixthNode.link4 = null;
                sixthNode.link5 = null; sixthNode.link6 = null; sixthNode.link7 = null; sixthNode.link8 = null;
                sixthNode.parent = node;
                changeValueAfterExit(sixthNode);
            }

            treeSearch seventhNode = new treeSearch();
            makeMove(node, seventhNode, "DL");
            if (seventhNode.elements != null)
            {
                seventhNode.position = seventhNode.posStart;
                seventhNode.distance = node.distance + 1;
                seventhNode.cost = getCost(seventhNode) + seventhNode.distance;
                seventhNode.link1 = null; seventhNode.link2 = null; seventhNode.link3 = null; seventhNode.link4 = null;
                seventhNode.link5 = null; seventhNode.link6 = null; seventhNode.link7 = null; seventhNode.link8 = null;
                seventhNode.parent = node;
                changeValueAfterExit(seventhNode);
            }

            treeSearch eighthNode = new treeSearch();
            makeMove(node, eighthNode, "UL");
            if (eighthNode.elements != null)
            {
                eighthNode.position = eighthNode.posStart;
                eighthNode.distance = node.distance + 1;
                eighthNode.cost = getCost(eighthNode) + eighthNode.distance;
                eighthNode.link1 = null; eighthNode.link2 = null; eighthNode.link3 = null; eighthNode.link4 = null;
                eighthNode.link5 = null; eighthNode.link6 = null; eighthNode.link7 = null; eighthNode.link8 = null;
                eighthNode.parent = node;
                changeValueAfterExit(eighthNode);
            }

            treeSearch firstNode = new treeSearch();
            makeMove(node, firstNode, "U");
            if (firstNode.elements != null)
            {
                firstNode.position = firstNode.posStart;
                firstNode.distance = node.distance + 1;
                firstNode.cost = getCost(firstNode) + firstNode.distance;
                firstNode.link1 = null; firstNode.link2 = null; firstNode.link3 = null; firstNode.link4 = null;
                firstNode.link5 = null; firstNode.link6 = null; firstNode.link7 = null; firstNode.link8 = null;
                firstNode.parent = node;
                changeValueAfterExit(firstNode);
            }
            

            treeSearch secondNode = new treeSearch();
            makeMove(node, secondNode, "R");
            if (secondNode.elements != null)
            {
                secondNode.position = secondNode.posStart;
                secondNode.distance = node.distance + 1;
                secondNode.cost = getCost(secondNode) + secondNode.distance;
                secondNode.link1 = null; secondNode.link2 = null; secondNode.link3 = null; secondNode.link4 = null;
                secondNode.link5 = null; secondNode.link6 = null; secondNode.link7 = null; secondNode.link8 = null;
                secondNode.parent = node;
                changeValueAfterExit(secondNode);
            }
           

            treeSearch thirdNode = new treeSearch();
            makeMove(node, thirdNode, "D");
            if (thirdNode.elements != null)
            {
                thirdNode.position = thirdNode.posStart;
                thirdNode.distance = node.distance + 1;
                thirdNode.cost = getCost(thirdNode) + thirdNode.distance;
                thirdNode.link1 = null; thirdNode.link2 = null; thirdNode.link3 = null; thirdNode.link4 = null;
                thirdNode.link5 = null; thirdNode.link6 = null; thirdNode.link7 = null; thirdNode.link8 = null;
                thirdNode.parent = node;
                changeValueAfterExit(thirdNode);
            }
            

            treeSearch fouthNode = new treeSearch();
            makeMove(node, fouthNode, "L");
            if (fouthNode.elements != null)
            {
                fouthNode.position = fouthNode.posStart;
                fouthNode.distance = node.distance + 1;
                fouthNode.cost = getCost(fouthNode)+fouthNode.distance;
                fouthNode.link1 = null; fouthNode.link2 = null; fouthNode.link3 = null; fouthNode.link4 = null;
                fouthNode.link5 = null; fouthNode.link6 = null; fouthNode.link7 = null; fouthNode.link8 = null;
                fouthNode.parent = node;
                changeValueAfterExit(fouthNode);
            }


            if (node.link5 == null && fifthNode.elements != null)
            {
                node.link5 = fifthNode;
                if (!success) States.Add(node.link5);
            }
            else
            {
                fifthNode = null;
            }

            if (node.link6 == null && sixthNode.elements != null)
            {
                node.link6 = sixthNode;
                if (!success) States.Add(node.link6);
            }
            else
            {
                sixthNode = null;
            }

            if (node.link7 == null && seventhNode.elements != null)
            {
                node.link7 = seventhNode;
                if (!success) States.Add(node.link7);
            }
            else
            {
                seventhNode = null;
            }

            if (node.link8 == null && eighthNode.elements != null)
            {
                node.link8 = eighthNode;
                if (!success) States.Add(node.link8);
            }
            else
            {
                eighthNode = null;
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
            bool success5 = true, success6 = true, success7 = true, success8 = true;

                if (state.link5 != null)
                {
                    if (state.link5.position != goalStateNode.posGoal)
                    {
                        success5 = false;
                    }
                }
                else
                {
                    success5 = false;
                }

                if (state.link6 != null)
                {
                    if (state.link6.position != goalStateNode.posGoal)
                    {
                        success6 = false;
                    }
                }
                else
                {
                    success6 = false;
                }

                if (state.link7 != null)
                {
                    if (state.link7.position != goalStateNode.posGoal)
                    {
                        success7 = false;
                    }
                }
                else
                {
                    success7 = false;
                }

                if (state.link8 != null)
                {
                    if (state.link8.position != goalStateNode.posGoal)
                    {
                        success8 = false;
                    }
                }
                else
                {
                    success8 = false;
                }

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
                addLstResult(state.link1);
            }
            else if (success2)
            {
                success = true;
                printResultConsole(state.link2);
                addLstResult(state.link2);
            }
            else if (success3)
            {
                success = true;
                printResultConsole(state.link3);
                addLstResult(state.link3);
            }
            else if (success4)
            {
                success = true;
                printResultConsole(state.link4);
                addLstResult(state.link4);
            }
            else if (success5)
            {
                success = true;
                printResultConsole(state.link5);
                addLstResult(state.link5);
            }
            else if (success6)
            {
                success = true;
                printResultConsole(state.link6);
                addLstResult(state.link6);
            }
            else if (success7)
            {
                success = true;
                printResultConsole(state.link7);
                addLstResult(state.link7);
            }
            else if (success8)
            {
                success = true;
                printResultConsole(state.link8);
                addLstResult(state.link8);
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

        public  List<treeSearch> temp2 = new List<treeSearch>();
        public void addLstResult(treeSearch node)
        {
            while (node != null)
            {
                temp2.Add(node);
                node = node.parent;
            }
           
        }

        public void makeMove(treeSearch parentNode, treeSearch currentNode, string director)
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
                case "U":
                    if (canMoveUp(currentNode))
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
                case "R":
                    if(canMoveRight(currentNode))
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
                case "D":
                    if (canMoveDown(currentNode))
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
                case "L":
                    if (canMoveLeft(currentNode))
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
                case "UR":
                    if(canMoveUp(currentNode) && canMoveRight(currentNode))
                    {
                        int pos = currentNode.position;
                        if (currentNode.elements[pos - 4] != 0)
                        {
                            temp = currentNode.elements[pos];
                            currentNode.elements[pos] = currentNode.elements[pos - 4];
                            currentNode.elements[pos - 4] = temp;
                            currentNode.posStart = pos - 4;
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
                case"DR":
                    if(canMoveDown(currentNode) && canMoveRight(currentNode) )
                    {
                        int pos = currentNode.position;
                        if (currentNode.elements[pos + 6] != 0)
                        {
                            temp = currentNode.elements[pos];
                            currentNode.elements[pos] = currentNode.elements[pos + 6];
                            currentNode.elements[pos + 6] = temp;
                            currentNode.posStart = pos + 6;
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
                case"DL":
                    if (canMoveDown(currentNode) && canMoveLeft(currentNode))
                    {
                        int pos = currentNode.position;
                        if (currentNode.elements[pos + 4] != 0)
                        {
                            temp = currentNode.elements[pos];
                            currentNode.elements[pos] = currentNode.elements[pos + 4];
                            currentNode.elements[pos + 4] = temp;
                            currentNode.posStart = pos + 4;
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
                case"UL":
                    if (canMoveUp(currentNode) && canMoveLeft(currentNode))
                    {
                        int pos = currentNode.position;
                        if (currentNode.elements[pos - 6] != 0)
                        {
                            temp = currentNode.elements[pos];
                            currentNode.elements[pos] = currentNode.elements[pos - 6];
                            currentNode.elements[pos - 6] = temp;
                            currentNode.posStart = pos - 6;
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
        public bool canMoveUp(treeSearch currentNode)
        {
            if (currentNode.position != 0 && currentNode.position != 1 && currentNode.position != 2 && currentNode.position != 3 && currentNode.position != 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool canMoveRight(treeSearch currentNode)
        {
            if (currentNode.position != 4 && currentNode.position != 9 && currentNode.position != 14 && currentNode.position != 19 && currentNode.position != 24)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool canMoveDown(treeSearch currentNode)
        {
            if (currentNode.position != 20 && currentNode.position != 21 && currentNode.position != 22 && currentNode.position != 23 && currentNode.position != 24)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool canMoveLeft(treeSearch currentNode)
        {
            if (currentNode.position != 0 && currentNode.position != 5 && currentNode.position != 10 && currentNode.position != 15 && currentNode.position != 20)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int getCost(treeSearch state)
        {
            int cost = 0;
            int a, b;
 
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
