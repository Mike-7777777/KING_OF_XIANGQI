using System;
using System.Collections.Generic;
using System.Text;

namespace KING_OF_XIANGQI
{
    using System;
    public class View
    {
        private string arrForPieces = "车马象士将炮兵"; //储存棋子的一维数组
        private readonly string arrForBoard =

 "　零　一　二　三　四　五　六　七　八" +
 "九┏─┳─┳─┳─┳─┳─┳─┳─┓" +
 "　┃　┃　┃　┃╲┃╱┃　┃　┃　┃" +
 "八┣─╋─╋─╋─╋─╋─╋─╋─┫" +
 "　┃　┃　┃　┃╱┃╲┃　┃　┃　┃" +
 "七┣─╋─╋─╋─╋─╋─╋─╋─┫" +
 "　┃　┃　┃　┃　┃　┃　┃　┃　┃" +
 "六┣─╋─╋─╋─╋─╋─╋─╋─┫" +
 "　┃　┃　┃　┃　┃　┃　┃　┃　┃" +
 "五┣─┻─┻─┻─┻─┻─┻─┻─┫" +
 "　┃　　　楚河　　┃　　汉界　　　┃" +
 "四┣─┳─┳─┳─┳─┳─┳─┳─┫" +
 "　┃　┃　┃　┃　┃　┃　┃　┃　┃" +
 "三┣─╋─╋─╋─╋─╋─╋─╋─┫" +
 "　┃　┃　┃　┃　┃　┃　┃　┃　┃" +
 "二┣─╋─╋─╋─╋─╋─╋─╋─┫" +
 "　┃　┃　┃　┃╲┃╱┃　┃　┃　┃" +
 "一┣─╋─╋─╋─╋─╋─╋─╋─┫" +
 "　┃　┃　┃　┃╱┃╲┃　┃　┃　┃" +
 "零┗─┻─┻─┻─┻─┻─┻─┻─┛";
        // 棋盘二维数组
        private string arrGamingBoard =

 "　零　一　二　三　四　五　六　七　八" +
 "九┏─┳─┳─┳─┳─┳─┳─┳─┓" +
 "　┃　┃　┃　┃╲┃╱┃　┃　┃　┃" +
 "八┣─╋─╋─╋─╋─╋─╋─╋─┫" +
 "　┃　┃　┃　┃╱┃╲┃　┃　┃　┃" +
 "七┣─╋─╋─╋─╋─╋─╋─╋─┫" +
 "　┃　┃　┃　┃　┃　┃　┃　┃　┃" +
 "六┣─╋─╋─╋─╋─╋─╋─╋─┫" +
 "　┃　┃　┃　┃　┃　┃　┃　┃　┃" +
 "五┣─┻─┻─┻─┻─┻─┻─┻─┫" +
 "　┃　　　楚河　　┃　　汉界　　　┃" +
 "四┣─┳─┳─┳─┳─┳─┳─┳─┫" +
 "　┃　┃　┃　┃　┃　┃　┃　┃　┃" +
 "三┣─╋─╋─╋─╋─╋─╋─╋─┫" +
 "　┃　┃　┃　┃　┃　┃　┃　┃　┃" +
 "二┣─╋─╋─╋─╋─╋─╋─╋─┫" +
 "　┃　┃　┃　┃╲┃╱┃　┃　┃　┃" +
 "一┣─╋─╋─╋─╋─╋─╋─╋─┫" +
 "　┃　┃　┃　┃╱┃╲┃　┃　┃　┃" +
 "零┗─┻─┻─┻─┻─┻─┻─┻─┛"; // 棋盘二维数组


        /*static void Main(string[] args)   //for test
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
            */

        public void PossibleMovementPoint(Table dataTable)
        {
            Console.Clear();//刷新console
            // 获取getcolor 数组
            int[,] colorTable = dataTable.GetColor();
            Piece[,] arrTable = dataTable.GetArr();
            ////新版本改动位置（view1)
            int mX = dataTable.GetChosePiece()[0];////chosen pieces
            int mY = dataTable.GetChosePiece()[1];////chosen pieces
            ////版本改动(view1)

            // 重新打印带有possible movement 的棋盘
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 18; j++)
                {

                    int tX, bX, tY, bY;
                    tX = (j + 1) / 2 - 1;// (j - 1) / 2;
                    bX = (j + 1) % 2; //(j - 1) % 2; 
                    tY = 9 - (i - 1) / 2; //(19 - i) / 2;
                    bY = (i - 1) % 2; //(19 - i) % 2;
                    if (i == 19 - 2 * mY && j == 2 * mX + 1) //chose point 
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.ForegroundColor = ConsoleColor.Green;//选中的棋子变成绿色
                        Console.Write(arrGamingBoard[18 * i + j]);
                        Console.ResetColor();
                    }
                    ////版本改动(view1)
                    else if (bX == 0 && bY == 0 && tX >= 0 && tY >= 0 && arrTable[tX, tY] != null && colorTable[tX, tY] == 1) // possible move
                    {
                        //arrGamingBorad[i, j] = ArrTableGetType(arrTable[tX, tY].GetType().ToString());
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.ForegroundColor = ConsoleColor.Blue;//可移动路径显示为blue色
                        Console.Write(arrGamingBoard[18 * i + j]);
                        Console.ResetColor();
                    }
                    else if (bX == 0 && bY == 0 && tX >= 0 && tY >= 0 && arrTable[tX, tY] == null && colorTable[tX, tY] == 1) // possible move
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.ForegroundColor = ConsoleColor.Blue;//可移动路径显示为blue色
                        Console.Write(arrForBoard[18 * i + j]);
                        Console.ResetColor();
                    }
                    else if (bX == 0 && bY == 0 && arrTable[tX, tY] != null)
                    {
                        string color;
                        color = arrTable[tX, tY].GetColor();

                        ///版本改动（view2)  
                        string PiecesPos = ArrTableGetType(arrTable[tX, tY].GetType().ToString()); /// get the position for pieces
                        arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1); ///remove the element previous
                        arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, PiecesPos);///insert the element(piece we need)in it 
                                                                                        ///版本改动（view2) 

                        if (color == "Black")//  获取数组getArr() 数组里面元素为1 的坐标
                        {

                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(arrGamingBoard[(18 * i) + j]);
                            Console.ResetColor();
                        }
                        else if (color == "Red") // 红方棋子
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(arrGamingBoard[(18 * i) + j]);
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write(arrForBoard[(18 * i) + j]);
                        Console.ResetColor();
                    }
                    while (j == 17)
                    {
                        Console.WriteLine("");

                        break;
                    }
                }
            }

        }
        public string ArrTableGetType(string typeString)
        {
            //
            switch (typeString)
            {
                case "KING_OF_XIANGQI.Rook":
                    return "车";
                case "KING_OF_XIANGQI.General":
                    return "将";
                case "KING_OF_XIANGQI.Horse":
                    return "马";
                case "KING_OF_XIANGQI.Cannon":
                    return "炮";
                case "KING_OF_XIANGQI.Mandarin":
                    return "士";
                case "KING_OF_XIANGQI.Elephant":
                    return "象";
                case "KING_OF_XIANGQI.Pawn":
                    return "卒";
            }
            return "-";
        }

        /*初始棋盘示意(代码坐标参考系）
        19" ┏━┳━┳━┳━┳━┳━┳━┳━┓\n" +
        18" ┃  ┃  ┃  ┃╲┃╱┃  ┃  ┃  ┃\n" +
        17" ┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
        16" ┃  ┃  ┃  ┃╱┃╲┃  ┃  ┃  ┃\n" +
        15" ┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
        14" ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃\n" +
        13" ┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
        12" ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃\n" +
        11" ┣━┻━┻━┻━┻━┻━┻━┻━┫\n" +
        10" ┃              ┃              ┃\n" +
        9 " ┣━┳━┳━┳━┳━┳━┳━┳━┫\n" +
        8 " ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃\n" +
        7 " ┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
        6 " ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃\n" +
        5 " ┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
        4 " ┃  ┃  ┃  ┃╲┃╱┃  ┃  ┃  ┃\n" +
        3 " ┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
        2 " ┃  ┃  ┃  ┃╱┃╲┃  ┃  ┃  ┃\n" +
        1 " ┗━┻━┻━┻━┻━┻━┻━┻━┛\n"
        0 " 1 2 3 4 5 6 7 8 9 1011121314151617);*/

        public void PositionChangingDisplay(Table dataTable)
        {
            Console.Clear();//刷新console
            // 获取getArr() 数组
            Piece[,] arrTable = dataTable.GetArr();
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 18; j++)
                {
                    int tX = (j + 1) / 2 - 1;// (j - 1) / 2;
                    int bX = (j + 1) % 2; //(j - 1) % 2; 
                    int tY = 9 - (i - 1) / 2; //(19 - i) / 2;
                    int bY = (i - 1) % 2; //(19 - i) % 2                    // color 获取color为黑或红的棋子
                    if (bX == 0 && bY == 0 && arrTable[tX, tY] != null)
                    {
                        string color;
                        color = arrTable[tX, tY].GetColor();

                        ///版本改动（view2)  
                        string PiecesPos = ArrTableGetType(arrTable[tX, tY].GetType().ToString());/// get the position for pieces
                        arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1);///remove the element previous
                        arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, PiecesPos);///insert the element(piece we need)in it 
                                                                                        ///版本改动（view2) 

                        if (color == "Black")//  获取数组getArr() 数组里面元素为1 的坐标
                        {

                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(arrGamingBoard[(18 * i) + j]);
                            Console.ResetColor();
                        }
                        else if (color == "Red") // 红方棋子
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(arrGamingBoard[(18 * i) + j]);
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write(arrForBoard[18 * i + j]);
                        Console.ResetColor();
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

        /*初始棋盘示意(代码坐标参考系）
          19" ┏━┳━┳━┳━┳━┳━┳━┳━┓\n" +
          18" ┃  ┃  ┃  ┃╲┃╱┃  ┃  ┃  ┃\n" +
          17" ┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
          16" ┃  ┃  ┃  ┃╱┃╲┃  ┃  ┃  ┃\n" +
          15" ┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
          14" ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃\n" +
          13" ┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
          12" ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃\n" +
          11" ┣━┻━┻━┻━┻━┻━┻━┻━┫\n" +
          10" ┃              ┃              ┃\n" +
          9 " ┣━┳━┳━┳━┳━┳━┳━┳━┫\n" +
          8 " ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃\n" +
          7 " ┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
          6 " ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃  ┃\n" +
          5 " ┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
          4 " ┃  ┃  ┃  ┃╲┃╱┃  ┃  ┃  ┃\n" +
          3 " ┣━╋━╋━╋━╋━╋━╋━╋━┫\n" +
          2 " ┃  ┃  ┃  ┃╱┃╲┃  ┃  ┃  ┃\n" +
          1 " ┗━┻━┻━┻━┻━┻━┻━┻━┛\n"
          0 " 1 2 3 4 5 6 7 8 9 1011121314151617);*/
        // 初始棋盘 （有棋子）展示
        public void InitialBoardForDisplay()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 18; j++)
                {
                    ///版本改动（view2)  
                    if ((i == 1 && j == 1) || (i == 1 && j == 17)) // 蓝方车
                    {
                        arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1);
                        arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, "车");
                    }
                    if ((i == 1 && j == 3) || (i == 1 && j == 15)) // 蓝方马
                    {
                        arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1);
                        arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, "马");
                    }
                    if ((i == 1 && j == 5) || (i == 1 && j == 13))// 蓝方象
                    {
                        arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1);
                        arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, "象");
                    }
                    if ((i == 1 && j == 7) || (i == 1 && j == 11))// 蓝方士                 
                    {
                        arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1);
                        arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, "士");
                    }
                    if (i == 1 && j == 9)// 蓝方将      
                    {
                        arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1);
                        arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, "将");
                    }

                    if ((i == 5 && j == 3) || (i == 5 && j == 15))// 蓝方炮              
                    {
                        arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1);
                        arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, "炮");
                    }

                    if ((i == 7 && j == 1) || (i == 7 && j == 5) || (i == 7 && j == 9) || (i == 7 && j == 13) || (i == 7 && j == 17))// 蓝方兵              
                    {
                        arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1);
                        arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, "兵");
                    }
                    if ((i == 13 && j == 1) || (i == 13 && j == 5) || (i == 13 && j == 9) || (i == 13 && j == 13) || (i == 13 && j == 17))// 红方兵              
                    {
                        arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1);
                        arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, "兵");
                    }

                    if ((i == 15 && j == 3) || (i == 15 && j == 15))// 红方炮                  
                    {
                        arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1);
                        arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, "炮");
                    }
                    if ((i == 19 && j == 7) || (i == 19 && j == 11))// 红方士      
                    {
                        arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1);
                        arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, "士");
                    }

                    if ((i == 19 && j == 5) || (i == 19 && j == 13))// 红方象    
                    {
                        arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1);
                        arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, "象");
                    }
                    if ((i == 19 && j == 3) || (i == 19 && j == 15)) // 红方马
                    {
                        arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1);
                        arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, "马");
                    }
                    if ((i == 19 && j == 1) || (i == 19 && j == 17)) // 红方车
                    {
                        arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1);
                        arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, "车");
                    }

                    if (i == 19 && j == 9) // 红方将
                    {
                        arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1);
                        arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, "将");
                    } ///版本改动（view2)  
                }
            }
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 18; j++)
                {


                    if (i <= 10)
                    {
                        if (arrGamingBoard[(18 * i) + j] == arrForPieces[0] || arrGamingBoard[(18 * i) + j] == arrForPieces[1] ||
                            arrGamingBoard[(18 * i) + j] == arrForPieces[2] || arrGamingBoard[(18 * i) + j] == arrForPieces[3] ||
                            arrGamingBoard[(18 * i) + j] == arrForPieces[4] || arrGamingBoard[(18 * i) + j] == arrForPieces[5] || arrGamingBoard[(18 * i) + j] == arrForPieces[6])
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(arrGamingBoard[(18 * i) + j]);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.Write(arrGamingBoard[(18 * i) + j]);
                        }
                    }
                    else
                    {
                        if (arrGamingBoard[(18 * i) + j] == arrForPieces[0] || arrGamingBoard[(18 * i) + j] == arrForPieces[1] ||
                            arrGamingBoard[(18 * i) + j] == arrForPieces[2] || arrGamingBoard[(18 * i) + j] == arrForPieces[3] ||
                            arrGamingBoard[(18 * i) + j] == arrForPieces[4] || arrGamingBoard[(18 * i) + j] == arrForPieces[5] || arrGamingBoard[(18 * i) + j] == arrForPieces[6])
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(arrGamingBoard[(18 * i) + j]);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.Write(arrGamingBoard[(18 * i) + j]);
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
        public int[] GetRead()
        {
            string spot = Console.ReadLine();

            //用标点分开
            string[] spotarr = spot.Split(',');

            //输出并转化为int数组
            int[] num = new int[spotarr.Length];
            for (int i = 0; i < spotarr.Length; i++)
            {
                num[i] = int.Parse(spotarr[i]);
            }
            return num;
        }
    }
}



