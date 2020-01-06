using System;
using System.Collections.Generic;
using System.Text;

namespace KING_OF_XIANGQI
{
    using System;
    public class View
    {
        private string arrForPieces = "车马象士将炮兵"; //储存棋子的一维数组 //Array for storing pieces
        private readonly string arrForBoard =

 "　零　一　二　三　四　五　六　七　八" +
 "零┏─┳─┳─┳─┳─┳─┳─┳─┓" +
 "　┃　┃　┃　┃╲┃╱┃　┃　┃　┃" +
 "一┣─╋─╋─╋─╋─╋─╋─╋─┫" +
 "　┃　┃　┃　┃╱┃╲┃　┃　┃　┃" +
 "二┣─╋─╋─╋─╋─╋─╋─╋─┫" +
 "　┃　┃　┃　┃　┃　┃　┃　┃　┃" +
 "三┣─╋─╋─╋─╋─╋─╋─╋─┫" +
 "　┃　┃　┃　┃　┃　┃　┃　┃　┃" +
 "四┣─┻─┻─┻─┻─┻─┻─┻─┫" +
 "　┃　　　楚河　　┃　　汉界　　　┃" +
 "五┣─┳─┳─┳─┳─┳─┳─┳─┫" +
 "　┃　┃　┃　┃　┃　┃　┃　┃　┃" +
 "六┣─╋─╋─╋─╋─╋─╋─╋─┫" +
 "　┃　┃　┃　┃　┃　┃　┃　┃　┃" +
 "七┣─╋─╋─╋─╋─╋─╋─╋─┫" +
 "　┃　┃　┃　┃╲┃╱┃　┃　┃　┃" +
 "八┣─╋─╋─╋─╋─╋─╋─╋─┫" +
 "　┃　┃　┃　┃╱┃╲┃　┃　┃　┃" +
 "九┗─┻─┻─┻─┻─┻─┻─┻─┛";
        // 棋盘二维数组
        // Checkerboard two-dimensional array
        private string arrGamingBoard =

 "　零　一　二　三　四　五　六　七　八" +
 "零┏─┳─┳─┳─┳─┳─┳─┳─┓" +
 "　┃　┃　┃　┃╲┃╱┃　┃　┃　┃" +
 "一┣─╋─╋─╋─╋─╋─╋─╋─┫" +
 "　┃　┃　┃　┃╱┃╲┃　┃　┃　┃" +
 "二┣─╋─╋─╋─╋─╋─╋─╋─┫" +
 "　┃　┃　┃　┃　┃　┃　┃　┃　┃" +
 "三┣─╋─╋─╋─╋─╋─╋─╋─┫" +
 "　┃　┃　┃　┃　┃　┃　┃　┃　┃" +
 "四┣─┻─┻─┻─┻─┻─┻─┻─┫" +
 "　┃　　　楚河　　┃　　汉界　　　┃" +
 "五┣─┳─┳─┳─┳─┳─┳─┳─┫" +
 "　┃　┃　┃　┃　┃　┃　┃　┃　┃" +
 "六┣─╋─╋─╋─╋─╋─╋─╋─┫" +
 "　┃　┃　┃　┃　┃　┃　┃　┃　┃" +
 "七┣─╋─╋─╋─╋─╋─╋─╋─┫" +
 "　┃　┃　┃　┃╲┃╱┃　┃　┃　┃" +
 "八┣─╋─╋─╋─╋─╋─╋─╋─┫" +
 "　┃　┃　┃　┃╱┃╲┃　┃　┃　┃" +
 "九┗─┻─┻─┻─┻─┻─┻─┻─┛";
        // 棋盘二维数组
        // Checkerboard two-dimensional array

        public void PossibleMovementPoint(Table dataTable)
        {
            Console.Clear();//刷新console //Refresh console
            // 获取getcolor 数组 
            // Get the getcolor array
            int[,] colorTable = dataTable.GetColor();
            Piece[,] arrTable = dataTable.GetArr();

            int ChoosenX = dataTable.GetChosePiece()[0];////chosen pieces OF coordinate X;
            int ChoosenY = dataTable.GetChosePiece()[1];////chosen pieces OF coordinate X


            // 重新打印带有possible movement 的棋盘
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 18; j++)
                {

                    int CoordX, CoordXJudge, CoordY, CoordYJudge;
                    CoordX = (j + 1) / 2 - 1;// Coordinate X transform 
                    CoordXJudge = (j + 1) % 2; //Coordinate X transform for judge in the loop
                    CoordY =  (i - 1) / 2; //Coordinate Y transform 
                    CoordYJudge = (i - 1) % 2; //Coordinate Y transform for judge in the loop
                    if (i == 2 * ChoosenY + 1 && j == 2 * ChoosenX + 1) //choose point 
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.ForegroundColor = ConsoleColor.Green;//the choosed piece become green.
                        Console.Write(arrGamingBoard[18 * i + j]);
                        Console.ResetColor();
                    }
                    //Exclude decimals from algorithm   //Exclude negatives from algorithm   //Get the position need to be colored
                    else if (CoordXJudge == 0 && CoordYJudge == 0 && CoordX >= 0 && CoordY >= 0 && colorTable[CoordX, CoordY] == 1) // possible move
                    {
                        if (arrTable[CoordX, CoordY] != null)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow; // background color 
                            Console.ForegroundColor = ConsoleColor.Blue;//可移动棋子显示为blue色 //the possible move pieces become blue.
                            Console.Write(arrGamingBoard[18 * i + j]);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow; // background color 
                            Console.ForegroundColor = ConsoleColor.Blue;//可移动路径显示为blue色 //the possible move path become blue.
                            Console.Write(arrForBoard[18 * i + j]);
                            Console.ResetColor();
                        }
                    }

                    else if (CoordXJudge == 0 && CoordYJudge == 0 && arrTable[CoordX, CoordY] != null)
                    {
                        string color;
                        color = arrTable[CoordX, CoordY].GetColor();// get color (Red or Black)
                        string PiecesPos = ArrTableGetType(arrTable[CoordX, CoordY].GetType().ToString()); /// get the position for pieces
                        arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1); ///remove the element previous
                        arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, PiecesPos);///insert the element(piece we need)in it 
                        if (color == "Black")//  获取数组getArr() 数组里面元素为1 的坐标 //Get the array getArr () embeds the coordinates of element 1 inside
                        {

                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Black;//the  pieces become black.
                            Console.Write(arrGamingBoard[(18 * i) + j]);
                            Console.ResetColor();
                        }
                        else if (color == "Red") // 红方棋子
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Red;//the  pieces become black.
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
                        Console.WriteLine("");//Newline

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
                    int CoordX = (j + 1) / 2 - 1;// (j - 1) / 2;
                    int CoordXJudge = (j + 1) % 2; //(j - 1) % 2; 
                    int CoordY = (i - 1) / 2; //(19 - i) / 2;
                    int CoordYJudge = (i - 1) % 2; //(19 - i) % 2                    // color 获取color为黑或红的棋子
                    if (CoordXJudge == 0 && CoordYJudge == 0 && arrTable[CoordX, CoordY] != null)
                    {
                        string color;
                        color = arrTable[CoordX, CoordY].GetColor();

                        string PiecesPos = ArrTableGetType(arrTable[CoordX, CoordY].GetType().ToString());/// get the position for pieces
                        arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1);///remove the element previous
                        arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, PiecesPos);///insert the element(piece we need)in it 


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
            // for 循环用于插入棋子 ；//for loop For inserting pieces 
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 18; j++)
                {
                    int caseSwitch = (18 * i) + j;
                    switch (caseSwitch)
                    {
                        ///黑方车坐标 coordinate of Black square car
                        case 19:
                        case 35:
                        ///红方车坐标  coordinate of Red square car
                        case 343:
                        case 359:
                            arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1);
                            arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, "车");
                            break;

                        ///黑方🐎坐标 coordinate of Black square horse
                        case 21:
                        case 33:
                        ///红方🐎坐标 coordinate of Red square horse
                        case 345:
                        case 357:
                            arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1);
                            arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, "马");
                            break;

                        ///黑方象坐标 coordinate of Black square elephant
                        case 23:
                        case 31:
                        ///红方象坐标 coordinate of Red square elephant
                        case 347:
                        case 355:
                            arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1);
                            arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, "象");
                            break;

                        ///黑方士坐标 coordinate of Black square mandarin
                        case 25:
                        case 29:
                        ///红方士坐标 coordinate of Red square mandarin
                        case 349:
                        case 353:
                            arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1);
                            arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, "士");
                            break;

                        ///黑方将坐标 coordinate of Black square general
                        case 27:
                        ///红方将坐标 coordinate of Black square general
                        case 351:
                            arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1);
                            arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, "将");
                            break;

                        ///黑方炮坐标 coordinate of Black square cannon
                        case 93:
                        case 105:
                        ///红方炮坐标 coordinate of Red square cannon
                        case 273:
                        case 285:
                            arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1);
                            arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, "炮");
                            break;

                        ///黑方兵坐标 coordinate of Black square pawn
                        case 127:
                        case 131:
                        case 135:
                        case 139:
                        case 143:
                        ///红方兵坐标 coordinate of Red square pawn
                        case 235:
                        case 239:
                        case 243:
                        case 247:
                        case 251:
                            arrGamingBoard = arrGamingBoard.Remove((18 * i) + j, 1);
                            arrGamingBoard = arrGamingBoard.Insert((18 * i) + j, "兵");
                            break;

                    }
                }
            }
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 18; j++)
                {

                    // divide into black or red square
                    if (i <= 10) // black square
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
                    else // red square
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

            //用标点分开 //Separate with punctuation
            string[] spotarr = spot.Split(',');

            //输出并转化为int数组 
            //Output and convert to int array
            int[] num = new int[spotarr.Length];
            for (int i = 0; i < spotarr.Length; i++)
            {
                num[i] = int.Parse(spotarr[i]);
            }
            return num;
        }
    }
}



