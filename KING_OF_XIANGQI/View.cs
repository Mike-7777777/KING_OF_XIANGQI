using System;
using System.Collections.Generic;
using System.Text;

namespace KING_OF_XIANGQI
{
    public class View 
    {
        private string[] arrForPieces = new string[7] { "车", "马", "象", "士", "将", "炮", "兵" }; //储存棋子的一维数组
        private string[,] arrForBoard = {
                                    {"  ","一 ","   ","二 ","   ","三 ","   ","四 ","   ","五 ","   ","六 ","   ","七 ","   ","八 ","   ","九"},//0
                                    {"1 ","┏ "," ━  ","┳ "," ━  ","┳ "," ━  ","┳ "," ━  ","┳ "," ━  ","┳ "," ━  ","┳ "," ━  ","┳ "," ━  ","┓ "},//1
                                    { "  ","┃ ","  ", "  ┃", "  ", "   ┃", "  ", "   ┃", "  ╲  ", "┃", "  ╱  ", "┃", "   ", "  ┃", "  ", "   ┃", "   ", "  ┃ " },//2
                                    {"2 ","┣ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","┫ "},//3
                                    { "  ","┃ ","  ", "  ┃", "  ", "   ┃", "  ", "   ┃", "  ╱  ", "┃", "  ╲  ", "┃", "   ", "  ┃", "  ", "   ┃", "   ", "  ┃ " },//4
                                    {"3 ","┣ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","┫ "},//5
                                    { "  ","┃ ","  ", "  ┃", "  ", "   ┃", "  ", "   ┃", "     ", "┃", "     ", "┃", "   ", "  ┃", "  ", "   ┃", "   ", "  ┃ " },//6
                                    {"4 ","┣ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","┫ "},//7
                                    { "  ","┃ ","  ", "  ┃", "  ", "   ┃", "  ", "   ┃", "     ", "┃", "     ", "┃", "   ", "  ┃", "  ", "   ┃", "   ", "  ┃ " },//8
                                    {"5 ","┣ "," ━  ","┻ "," ━  ","┻ "," ━  ","┻ "," ━  ","┻ "," ━  ","┻ "," ━  ","┻ "," ━  ","┻ "," ━  ","┫ "},//9
                                    { "  ","┃ ","  ", "   ", "  ", "楚河  ", "  ", "    ", "   ", "┃", "     ", " ", "   ", "   ", "漢界  ", "   ", "  ", "┃ " },//10
                                    {"6 ","┣ "," ━  ","┳ "," ━  ","┳ "," ━  ","┳ "," ━  ","┳ "," ━  ","┳ "," ━  ","┳ "," ━  ","┳ "," ━  ","┫ "},//11
                                    { "  ","┃ ","  ", "  ┃", "  ", "   ┃", "  ", "   ┃", "     ", "┃", "     ", "┃", "   ", "  ┃", "  ", "   ┃", "   ", "  ┃ " },//12
                                    {"7 ","┣ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","┫ "},//13
                                    { "  ","┃ ","  ", "  ┃", "  ", "   ┃", "  ", "   ┃", "     ", "┃", "     ", "┃", "   ", "  ┃", "  ", "   ┃", "   ", "  ┃ " },//14
                                    {"8 ","┣ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","┫ "},//15
                                    {"  ", "┃ ","  ", "  ┃", "  ", "   ┃", "  ", "   ┃", "  ╲  ", "┃", "  ╱  ", "┃", "   ", "  ┃", "  ", "   ┃", "   ", "  ┃ " },//16
                                    {"9 ","┣ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","╋ "," ━  ","┫ "},//17
                                    {"  ", "┃ ","  ", "  ┃", "  ", "   ┃", "  ", "   ┃", "  ╱  ", "┃", "  ╲  ", "┃", "   ", "  ┃", "  ", "   ┃", "   ", "  ┃ " },//18
                                    {"10","┗ "," ━  ","┻ "," ━  ","┻ "," ━  ","┻ "," ━  ","┻ "," ━  ","┻ "," ━  ","┻ "," ━  ","┻ "," ━  ","┛ "},//19
                                 }; // 棋盘二维数组


        static void Main(string[] args)   //for test
            {
                View a = new View();

                // 棋盘开始 输入1开始棋局
                a.GameReady("1");

                //展示棋盘（带棋子）
                a.InitialBoardForDisplay();               

                //选择棋子 以 及 展示可以行走的棋子的possible movement（变色）
                //a.PossibleMovementPoint(dataTable);  // datatable 为 table 类型的初始棋盘

               //棋子走动的位置改变
                // a.PositionChangingDisplay(dataTable);
            }

        public void PossibleMovementPoint(Table dataTable)
        {
            // 获取getcolor 数组
            int[,] colorTable = dataTable.getColor();
            Piece[,] arrTable = dataTable.getArr();
            // 重新打印带有possible movement 的棋盘
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 18; j++)
                {   
                    int tX = (i + 1) / 2;
                    int tY = (j + 1) / 2;
                    if(colorTable[(i+1)/2, (j+1)/2] == 1)// 获取数组getcolor 数组里面元素为1 的坐标
                    {
                        arrForBoard[i, j] = arrTable[tX, tY].GetType().ToString();
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.ForegroundColor = ConsoleColor.Red;//可移动路径显示为红色
                        Console.Write (arrForBoard[i,j]); 
                        Console.ResetColor();
                    }
                    else Console.Write(arrForBoard[i,j]); 

                    while (j == 17)
                    {
                        Console.WriteLine("");
                       break;
                    }
                }
            }
            
        }
        
        /*0 " 1 2 3 4 5 6 7 8 9 1011121314151617
          1 " ┏━┳━┳━┳━┳━┳━┳━┳━┓\n" +
          2 " ┃  ┃  ┃  ┃╲┃╱┃  ┃  ┃  ┃\n" +
          3 " ┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
          4 " ┃  ┃  ┃  ┃╱┃╲┃  ┃  ┃  ┃\n" +
          5 " ┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
          6 " ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃\n" +
          7 " ┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
          8 " ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃\n" +
          9 " ┣━┻━┻━┻━┻━┻━┻━┻━┫\n" +
          10" ┃              ┃              ┃\n" +
          11" ┣━┳━┳━┳━┳━┳━┳━┳━┫\n" +
          12" ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃\n" +
          13" ┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
          14" ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃\n" +
          15" ┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
          16" ┃  ┃  ┃  ┃╲┃╱┃  ┃  ┃  ┃\n" +
          17" ┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
          18" ┃  ┃  ┃  ┃╱┃╲┃  ┃  ┃  ┃\n" +
          19" ┗━┻━┻━┻━┻━┻━┻━┻━┛\n");*/
       
        public void PositionChangingDisplay(Table dataTable)
        {
            // 获取getArr() 数组
            Piece [,] arrTable = dataTable.getArr();

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 18; j++)
                {                   
                    // color 获取color为黑或红的棋子
                    int tempX = (i+1)/2;
                    int tempY = (j+1)/2;
                    string color= arrTable[tempX,tempY].getColor();
                    arrForBoard[i, j] = arrTable[tempX, tempY].GetType().ToString();

                    if (arrTable[tempX ,tempY ] != null && color == "Black" )//  获取数组getArr() 数组里面元素为1 的坐标
                    {

                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(arrForBoard);
                            Console.ResetColor();
                    }
                    else if(arrTable[tempX ,tempY ] != null && color == "Red") // 红方棋子
                    {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(arrForBoard);
                            Console.ResetColor();
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write(arrForBoard);
                    }
                    while (j == 17)
                    {
                        Console.WriteLine("");
                        Console.ResetColor();
                        break;
                    }
                }
            }
        }

        // 游戏开始按钮 （"1"代表开始）
        public string GameReady(string parameter) //parameter 开始参数
        {
            Console.WriteLine("are you ready for game? 1 means ready:");
            parameter = Console.ReadLine();

            if (parameter == "1")
            {

                Console.Clear();
                return "0";
            }
            else
            {
                Console.WriteLine("please input again.");
                return GameReady("1");
            }
        }

        /*初始棋盘示意(代码坐标参考系）
          0 " 1 2 3 4 5 6 7 8 9 1011121314151617
          1 " ┏━┳━┳━┳━┳━┳━┳━┳━┓\n" +
          2 " ┃  ┃  ┃  ┃╲┃╱┃  ┃  ┃  ┃\n" +
          3 " ┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
          4 " ┃  ┃  ┃  ┃╱┃╲┃  ┃  ┃  ┃\n" +
          5 " ┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
          6 " ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃\n" +
          7 " ┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
          8 " ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃\n" +
          9 " ┣━┻━┻━┻━┻━┻━┻━┻━┫\n" +
          10" ┃              ┃              ┃\n" +
          11" ┣━┳━┳━┳━┳━┳━┳━┳━┫\n" +
          12" ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃\n" +
          13" ┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
          14" ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃\n" +
          15" ┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
          16" ┃  ┃  ┃  ┃╲┃╱┃  ┃  ┃  ┃\n" +
          17" ┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
          18" ┃  ┃  ┃  ┃╱┃╲┃  ┃  ┃  ┃\n" +
          19" ┗━┻━┻━┻━┻━┻━┻━┻━┛\n");*/

        // 初始棋盘 （有棋子）展示
        public void InitialBoardForDisplay()
        {

            for (int i = 0; i < 20; i++)
            {   
                for (int j = 0; j < 18; j++)
                {
                    if ((i == 1 && j == 1) || (i == 1 && j == 17)) // 蓝方车
                        arrForBoard[i, j] = arrForPieces[0];
                    if ((i == 1 && j == 3) || (i == 1 && j == 15)) // 蓝方马
                        arrForBoard[i, j] = arrForPieces[1];
                    if ((i == 1 && j == 5) || (i == 1 && j == 13))// 蓝方象
                        arrForBoard[i, j] = arrForPieces[2];
                    if ((i == 1 && j == 7) || (i == 1 && j == 11))// 蓝方士
                        arrForBoard[i, j] = arrForPieces[3];
                    if (i == 1 && j == 9)// 蓝方将
                        arrForBoard[i, j] = arrForPieces[4];
                    if ((i == 5 && j == 3) || (i == 5 && j == 15))// 蓝方炮
                        arrForBoard[i, j] = arrForPieces[5];
                    if ((i == 7 && j == 1) || (i == 7 && j == 5) || (i == 7 && j == 9) || (i == 7 && j == 13) || (i == 7 && j == 17))// 蓝方兵
                        arrForBoard[i, j] = arrForPieces[6];
                    if ((i == 13 && j == 1) || (i == 13 && j == 5) || (i == 13 && j == 9) || (i == 13 && j == 13) || (i == 13 && j == 17))// 红方兵
                        arrForBoard[i, j] = arrForPieces[6];
                    if ((i == 15 && j == 3) || (i == 15 && j == 15))// 红方炮
                        arrForBoard[i, j] = arrForPieces[5];
                    if ((i == 19 && j == 7) || (i == 19 && j == 11))// 红方士
                        arrForBoard[i, j] = arrForPieces[3];
                    if ((i == 19 && j == 5) || (i == 19 && j == 13))// 红方象
                        arrForBoard[i, j] = arrForPieces[2];
                    if ((i == 19 && j == 3) || (i == 19 && j == 15)) // 红方马
                        arrForBoard[i, j] = arrForPieces[1];
                    if ((i == 19 && j == 1) || (i == 19 && j == 17)) // 红方车
                        arrForBoard[i, j] = arrForPieces[0];
                    if (i == 19 && j == 9) // 红方将
                        arrForBoard[i, j] = arrForPieces[4];
                }
            }
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 18; j++)
                {
                    

                    if (i <= 10)
                    {
                        if (arrForBoard[i, j] == arrForPieces[0] || arrForBoard[i, j] == arrForPieces[1] ||
                            arrForBoard[i, j] == arrForPieces[2] || arrForBoard[i, j] == arrForPieces[3] ||
                            arrForBoard[i, j] == arrForPieces[4] || arrForBoard[i, j] == arrForPieces[5] || arrForBoard[i, j] == arrForPieces[6])
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(arrForBoard[i, j]);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.Write(arrForBoard[i, j]);
                        }
                        }
                    else
                    {
                        if (arrForBoard[i, j] == arrForPieces[0] || arrForBoard[i, j] == arrForPieces[1] ||
                            arrForBoard[i, j] == arrForPieces[2] || arrForBoard[i, j] == arrForPieces[3] ||
                            arrForBoard[i, j] == arrForPieces[4] || arrForBoard[i, j] == arrForPieces[5] || arrForBoard[i, j] == arrForPieces[6])
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Red;                       
                            Console.Write(arrForBoard[i, j]);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.Write(arrForBoard[i, j]);                          
                            Console.ResetColor();
                        }
                    }

                    while (j == 17)
                    {
                        Console.WriteLine("");
                        Console.ResetColor();
                        break;
                    }
                }
            }
            Console.ResetColor();
        }
    }
}