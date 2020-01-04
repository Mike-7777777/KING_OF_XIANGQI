using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KING_OF_XIANGQI
{
    class Controller
    {
        private string myColor; //define the player this round.
        private List<int> xlist; //define a series of x coordinates used to store the possible moves of piece.
        private List<int> ylist; //define a series of y coordinates used to store the possible moves of piece.
        private List<int> alist; 
        private List<int> blist;
        private Table refDataTable;   //To get the reference of input variable 'dataTable', 
                                        //so that other methods can use it without input it everytime.
        private Piece[,] refArrTable; //To get the reference of the property(the array made by pieces) 
        //of input variable 'dataTable', 
        //so that other methods can use it without input it everytime.

        public Controller(Table dataTable)
        {
            this.refDataTable = dataTable;  //recieve the data of board.
            this.refArrTable = dataTable.GetArr();  //recieve the arrObject from database.
            this.xlist = new List<int>(); //init all the member variables.
            this.ylist = new List<int>();
            this.alist = new List<int>();
            this.blist = new List<int>();
        }
        public void ChooseP(int x, int y, string myColor)   //x,y is the location of piece that user chose. 
                                                            //This method is used to active one piece  
                                                            //and predict the point it could get.
                                                            //myColor string defined the rounding color, black & red.
        {
            //Store the player;
            this.myColor = myColor;
            //define a Piece object chosePiece to store the Piece pass by database.
            Piece chosePiece;
            List<int> xBan;
            List<int> yBan;
            //use getPiece method and pass two variables as     
            //the location to get the object we need.
            chosePiece = refDataTable.GetPiece(x, y);
            //Distinguish pieces types
            refDataTable.SetChosePiece(x, y);

            switch(chosePiece.GetType().ToString())
            {
                case "KING_OF_XIANGQI.General":
                    Console.WriteLine("In switch general"); //test
                    xlist = new List<int> { x + 0, x + 0, x - 1, x + 1 }; // up, down, left, right.
                    ylist = new List<int> { y + 1, y - 1, y + 0, y + 0 };
                    if (y < 5) 
                    {
                        RemoveOut(5); //remove all points out of small 9 space up.
                    }
                    else
                    {
                        RemoveOut(6); //remove all points out of small 9 space down.
                    }
                    PossibleMove(xlist, ylist);
                    break;
                case "KING_OF_XIANGQI.Mandarin":
                    xlist = new List<int> { x + 1, x + 1, x - 1, x - 1 }; // ↗, ↘, ↙, ↖.
                    ylist = new List<int> { y + 1, y - 1, y - 1, y + 1 };
                    if (y < 5)
                    {
                        RemoveOut(5); //remove all points out of small 9 space up.
                    }
                    else
                    {
                        RemoveOut(6); //remove all points out of small 9 space down.
                    }
                    RemoveOut(4); //remove all points out of small 9 space.
                    PossibleMove(xlist, ylist);
                    break;
                case "KING_OF_XIANGQI.Elephant":
                    Console.WriteLine("in Elephant");
                    // ↗, ↘, ↙, ↖.
                    //init two list, make it adapt it to the way the elephant walks.
                    xlist = new List<int> { x + 2, x + 2, x - 2, x - 2 };
                    ylist = new List<int> { y + 2, y - 2, y + 2, y - 2 };
                    //init two ban list used later to deletes the non-compliantelements.
                    xBan = new List<int> { x + 1, x + 1, x - 1, x - 1 };
                    yBan = new List<int> { y + 1, y - 1, y - 1, y + 1 };
                    //The following deletes the elements in the List 
                    //to meet the walking restrictions of the elephant.
                    if (y > 4)
                    {
                        RemoveOut(2);   // delete (make it null) 
                                        //the elements out of the upper half of board.
                    }
                    else
                    {
                        RemoveOut(3);// ... out of the lower half of board.
                    }
                    Ban(xBan,yBan,1);
                    PossibleMove(xlist, ylist);
                    break;
                case "KING_OF_XIANGQI.Horse":
                    Console.WriteLine("in horse");
                    //y↗ y↘ y↖ y↙, Y↗ Y↘ Y↖ Y↙
                    xlist = new List<int> { x + 2, x + 2, x - 2, x - 2, x + 1, x - 1, x + 1, x - 1 };
                    ylist = new List<int> { y + 1, y - 1, y + 1, y - 1, y + 2, y + 2, y - 2, y - 2 };
                    //init two list, make it adapt it to the way the elephant walks.
                    //→ ← ↑ ↓
                    xBan = new List<int> { x + 1, x - 1, x    , x    };
                    yBan = new List<int> { y    , y    , y + 1, y - 1 };
                    //The following deletes the elements in the List 
                    //to meet the walking restrictions of the elephant.
                    Ban(xBan, yBan, 0);
                    RemoveOut(1);   // delete the points out of board.
                    PossibleMove(xlist, ylist);
                    break;
                case "KING_OF_XIANGQI.Pawn":
                    PossibleMove_Pawn(x, y);
                    break;
                case "KING_OF_XIANGQI.Cannon":
                    PossibleMove_Cannon(x, y);
                    break;
                case "KING_OF_XIANGQI.Rook":
                    PossibleMove_Rook(x, y);
                    break;
            }
        }
        public void MoveP(Tuple<int, int> location_select, Tuple<int, int> location_move) //12.20 1：12 修改
        {
            refDataTable.SetArr(location_move.Item1, location_move.Item2,refArrTable[location_select.Item1, location_select.Item2]);    //由修改本地成员变量refArray
                                                                                                                                        //改为调用远端方法修改数据库
            refDataTable.NullArr(location_select.Item1, location_select.Item2);//空棋子或者空
            //return refArrTable;
        }// To move pieces.

        public void Ban(List<int> xBan, List<int> yBan, int mode) // mode 1 = ele, mode 0 = horse
        {
            int k = 0;
            for (int i = 0; i < 4; i++)//remove the points that cannot reach by piece elephant.
            {
                Boolean inRange = (xBan[i] < 10 && xBan[i] >= 0 && yBan[i] < 10 && yBan[i] >= 0);
                if (inRange && refArrTable[xBan[i], yBan[i]] != null)
                {
                    if(mode == 1)
                    {
                        xlist.RemoveAt(k);// four round: ↗, ↘, ↙, ↖.
                        ylist.RemoveAt(k);
                    }
                    else
                    {
                        xlist.RemoveAt(k);
                        ylist.RemoveAt(k);
                        xlist.RemoveAt(k);
                        ylist.RemoveAt(k);
                    }
                }
                else
                {
                    if (mode == 1)
                        k++;
                    else
                        k += 2;
                }
            }
        }
        public void PossibleMove(List<int> x, List<int> y) // To check the location is 'null' or not the friend piece, and change their color in database.
        {
            Console.WriteLine("in possible move");
            for (int i = 0; i < x.Count(); i++)
            {
                //如果目标点为空，或者为对方棋子，则变色。否则不变色。
                Console.WriteLine(x[i]);
                Console.WriteLine(y[i]);
                if (refArrTable[x[i], y[i]] == null) { Console.WriteLine("xi,yi == null"); }
                if (refArrTable[x[i], y[i]] == null 
                    || refArrTable[x[i], y[i]].GetColor() != myColor)//原来是!= 但是改了就对了,不知道为什么
                {
                    refDataTable.TableChangeColorActive(x[i], y[i]);
                }
            }
            xlist.Clear();
            ylist.Clear();
        }
        public void PossibleMove(int x, int y) // if the list(not a list actually) only has one element.
        {
            List<int> xtempList = new List<int>{ x };
            List<int> ytempList = new List<int> { y };
            PossibleMove(xtempList, ytempList);
        }
        public void RemoveOut(int mode)
        {
            int xindex = 0;
            static bool xInBoard(int xi)
            {
                return xi < 0 || xi > 8;
            }
            static bool yInBoard(int yi)
            {
                return yi < 0 || yi > 9;
            }
            static bool bottomHalfBoard(int yi)
            {
                return yi > 5;
            }
            static bool topHalfBoard(int yi)
            {
                return yi < 4;
            }
            static bool xInTNine(int xi)
            {
                return xi < 3 || xi > 5;
            }
            static bool yInTNineDown(int yi)
            {
                return yi > 2 || yi < 0;
            }
            static bool yInTNineUp(int yi)
            {
                return yi > 9 || yi < 7;
            }
            switch (mode)
            {// 1 default, 2 elephant up, 3 ele down(y<5, 4 general & mandarin 9 space)
             //
                case 1:
                    while ((this.xlist.FindIndex(xInBoard) != -1))
                    {
                        xindex = this.xlist.FindIndex(xInBoard);
                        this.xlist.RemoveAt(xindex);
                        this.ylist.RemoveAt(xindex);
                    }
                    Console.WriteLine(ylist.FindIndex(yInBoard));
                    while ((ylist.FindIndex(yInBoard) != -1))
                    {
                        xindex = ylist.FindIndex(yInBoard);
                        xlist.RemoveAt(xindex);
                        ylist.RemoveAt(xindex);
                    }
                    break;

                case 2:
                    RemoveOut(1);
                    while ((this.ylist.FindIndex(topHalfBoard) != -1))
                    {
                        xindex = this.ylist.FindIndex(topHalfBoard);
                        this.xlist.RemoveAt(xindex);
                        this.ylist.RemoveAt(xindex);
                    }
                    break;

                case 3:
                    RemoveOut(1);
                    while ((this.ylist.FindIndex(bottomHalfBoard) != -1))
                    {
                        xindex = this.ylist.FindIndex(bottomHalfBoard);
                        this.xlist.RemoveAt(xindex);
                        this.ylist.RemoveAt(xindex);
                    }
                    break;
                case 4:
                    while ((this.xlist.FindIndex(xInTNine) != -1))
                    {
                        xindex = this.xlist.FindIndex(xInTNine);
                        this.xlist.RemoveAt(xindex);
                        this.ylist.RemoveAt(xindex);
                    }
                    break;
                case 5:
                    RemoveOut(4);
                    while ((this.ylist.FindIndex(yInTNineDown) != -1))
                    {
                        xindex = this.ylist.FindIndex(yInTNineDown);
                        this.xlist.RemoveAt(xindex);
                        this.ylist.RemoveAt(xindex);
                    }
                    break;
                case 6:
                    RemoveOut(4);
                    while ((this.ylist.FindIndex(yInTNineUp) != -1))
                    {
                        xindex = this.ylist.FindIndex(yInTNineUp);
                        this.xlist.RemoveAt(xindex);
                        this.ylist.RemoveAt(xindex);
                    }
                    break;

            }
        } // delete the locations out of board in the xlist and ylist.
        public void PossibleMove_Pawn(int x, int y)//Pawn walk rules
        {
            List<int> xlist = new List<int> { x + 1, x - 1, x, x };
            List<int> ylist = new List<int> { y, y, y + 1, y - 1 };
            switch (refArrTable[x, y].GetColor())
            {
                case "Red":
                    xlist.RemoveAt(3);
                    ylist.RemoveAt(3);

                    if (y <= 4)
                    {

                        xlist.RemoveRange(0, 2);
                        ylist.RemoveRange(0, 2);

                    }
                    else if (y >= 5 && (x == 8 || x == 0))
                    {
                        ylist.RemoveAt(0);
                    }
                    break;
                case "Black":
                    xlist.RemoveAt(2);
                    ylist.RemoveAt(2);
                    if (y >= 5)
                    {
                        xlist.RemoveRange(0, 2);
                        ylist.RemoveRange(0, 2);
                    }
                    else if (y <= 4 && (x == 8 || x == 0))
                    {
                        ylist.RemoveAt(0);
                    }
                    break;
            }
            xlist = xlist.FindAll(

                            delegate (int i)
                            {
                                return (i >= 0 && i <= 8);
                            }
                            );
            ylist = ylist.FindAll(

                    delegate (int i)
                    {
                        return (i >= 0 && i <= 9);
                    }
                    );
            PossibleMove(xlist, ylist);
        }
        public void PossibleMove_Rook(int x, int y)//Rook walk.
        {
            Reset(x, y);
            for (int l = 0; l < 2; l++)
            {
                List<int> xtemplist = new List<int> { };
                List<int> ytemplist = new List<int> { };
                int judge = new int();
                if (l == 0)
                {
                    xtemplist = xlist;
                    ytemplist = ylist;
                    judge = x;
                }
                else
                {
                    xtemplist = alist;
                    ytemplist = blist;
                    judge = y;
                }
                List<int> templist = new List<int> { };
                templist = xtemplist.FindAll(delegate (int i) { return (i == judge); });
                for (int i = judge - 1; i > -1; i--)
                {
                    if (refArrTable[xtemplist[i], ytemplist[i]] != null)
                    {
                        if (refArrTable[x, y].GetType() == typeof(Cannon))
                        {
                            ytemplist.RemoveRange(0, i + 1);
                            xtemplist.RemoveRange(0, i + 1);
                        }
                        else
                        {
                            ytemplist.RemoveRange(0, i);
                            xtemplist.RemoveRange(0, i);
                        }
                        break;
                    }
                }
                Predicate<int> match = xi =>
                {
                    return xi == judge;
                };
                int j;
                int k;
                j = xtemplist.FindIndex(match);
                k = ytemplist.FindIndex(match);
                if (templist.Count == 1)
                {
                    k = j;
                }
                for (int i = k + 1; i < xtemplist.Count; i++)
                {
                    if (refArrTable[xtemplist[i], ytemplist[i]] != null)
                    {
                        if (refArrTable[x, y].GetType() == typeof(Cannon))
                        {
                            ytemplist.RemoveRange(i, ytemplist.Count - i);
                            xtemplist.RemoveRange(i, xtemplist.Count - i);
                        }
                        else
                        {
                            ytemplist.RemoveRange(i + 1, ytemplist.Count - i - 1);
                            xtemplist.RemoveRange(i + 1, xtemplist.Count - i - 1);
                        }
                        break;
                    }
                }
                PossibleMove(xtemplist, ytemplist);
                Reset(x, y);
            }
        }
        public void PossibleMove_Cannon(int x, int y)//Canon walk.
        {

            Reset(x, y);
            PossibleMove_Rook(x, y);
            for (int l = 0; l < 2; l++)
            {
                List<int> xtemplist = new List<int> { };
                List<int> ytemplist = new List<int> { };
                int judge = new int();
                if (l == 0)
                {
                    xtemplist = xlist;
                    ytemplist = ylist;
                    judge = x;
                }
                else
                {
                    xtemplist = alist;
                    ytemplist = blist;
                    judge = y;
                }
                List<int> templist = new List<int> { };
                templist = xtemplist.FindAll(delegate (int i) { return (i == judge); });
                for (int i = judge - 1; i > -1; i--)
                {
                    if (refArrTable[xtemplist[i], ytemplist[i]] != null)
                    {
                        for (int j = i - 1; j > -1; j--)
                        {
                            if (refArrTable[xtemplist[j], ytemplist[j]] != null)
                            {
                                PossibleMove(xtemplist[j], ytemplist[j]);
                                break;
                            }
                        }
                        break;
                    }
                }
                for (int i = judge + 1; i < xtemplist.Count; i++)
                {
                    if (refArrTable[xtemplist[i], ytemplist[i]] != null)
                    {
                        for (int j = i + 1; j < xtemplist.Count; j++)
                        {
                            if (refArrTable[xtemplist[j], ytemplist[j]] != null)
                            {
                                PossibleMove(xtemplist[j], ytemplist[j]);
                                break;
                            }
                        }
                        break;
                    }

                }
                Reset(x, y);
            }
        }
        public void Reset(int x, int y)
        {
            xlist.Clear();
            ylist.Clear();
            alist.Clear();
            blist.Clear();
            for (int i = 0; i < 9; i++)//车可能所在的x轴位置
            {
                xlist.Add(i);
                ylist.Add(y);

            }
            for (int i = 0; i < 10; i++)//车可能所在的y轴位置
            {
                alist.Add(x);
                blist.Add(i);

            }
        }//Reset the rook and canon list
    }
}
