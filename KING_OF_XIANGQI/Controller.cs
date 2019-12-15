using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KING_OF_XIANGQI
{
    class Controller
    {
        private string myColor; //define the player this round.
        private List<int> xlist;
        private List<int> ylist;
        private Table refDataTable;   //To get the reference of input variable 'dataTable', 
                                        //so that other methods can use it without input it everytime.
        private Piece[,] refArrTable; //To get the reference of the property(the array made by pieces) 
                                        //of input variable 'dataTable', 
                                        //so that other methods can use it without input it everytime.

        public Controller(Table dataTable)
        {
            this.refDataTable = dataTable;  //recieve the data of board.
            this.refArrTable = dataTable.getArr();  //recieve the arrObject from database.
            this.xlist = new List<int>();
            this.ylist = new List<int>();
        }
        public void chooseP(int x, int y, string myColor)   //x,y is the location of piece that user chose. 
                                                            //This method is used to active one piece  
                                                            //and predict the point it could get.
                                                            //myColor string defined the rounding color, black & red.
        {
            //Store the player;
            this.myColor = myColor;
            //define a Piece object chosePiece to store the Piece pass by database.
            Piece chosePiece;
            int[] xarr;
            int[] yarr;

            List<int> xTemplist = new List<int>(); //x坐标list 临时
            List<int> yTemplist = new List<int>(); //y坐标list 临时
            List<int> xBan;
            List<int> yBan;

            //use getPiece method and pass two variables as     
            //the location to get the object we need.
            chosePiece = refDataTable.getPiece(x, y);
            //Distinguish pieces types
            refDataTable.setChosePiece(x, y);

            switch(chosePiece.GetType().ToString())
            {
                case "KING_OF_XIANGQI.General":
                    Console.WriteLine("In switch general"); //test
                    this.xlist = new List<int> { x + 0, x + 0, x - 1, x + 1 }; // up, down, left, right.
                    this.ylist = new List<int> { y + 1, y + 1, y + 0, y + 0 };
                    removeOut(4); //remove all points out of small 9 space.
                    break;
                case "KING_OF_XIANGQI.Mandarin":
                    this.xlist = new List<int> { x + 1, x + 1, x - 1, x - 1 }; // ↗, ↘, ↙, ↖.
                    this.ylist = new List<int> { y + 1, y - 1, y - 1, y + 1 };
                    removeOut(4); //remove all points out of small 9 space.
                    break;
                case "KING_OF_XIANGQI.Elephant":

                case ""
                case ""
                case ""
                case ""
            }

            //象
            if (chosePiece is Elephant)
            {
                Console.WriteLine("in Elephant");
                // ↗, ↘, ↙, ↖.
                //init two list, make it adapt it to the way the elephant walks.
                this.xlist = new List<int> { x + 2, x + 2, x - 2, x - 2 };
                this.ylist = new List<int> { y + 2, y - 2, y + 2, y - 2 };
                //init two ban list used later to deletes the non-compliantelements.
                xBan = new List<int> { x + 1, x + 1, x - 1, x - 1 };
                yBan = new List<int> { y + 1, y + 1, y - 1, y - 1 };
                //The following deletes the elements in the List 
                //to meet the walking restrictions of the elephant.
                switch(y > 5)
                {
                    case true: // in the upper half of board.
                        removeOut(2);
                        break;
                    case false: // in the lower half of board.
                        removeOut(3);
                        break;
                }
                
                if (y + 1 < 10 && x - 1 >= 0 && refArrTable[x - 1, y + 1] != null)
                {
                    removeBan(y + 1, "y");
                }//左上象腿
                if (y - 1 >= 0 && x - 1 >= 0 && refArrTable[x - 1, y - 1] != null)
                {
                    removeBan(y - 1, "y");
                }//左下象腿
                if (y + 1 < 10 && x + 1 < 10 && refArrTable[x + 1, y + 1] != null)
                {
                    removeBan(y + 1, "y");
                }//左上象腿
                if (x + 1 < 10 && y - 1 >= 0 && refArrTable[x + 1, y - 1] != null)
                {
                    removeBan(y - 1, "y");
                }//左上象腿
                possibleMove(xlist, ylist);
            }
            
            //马
            if (chosePiece is Horse)
            {
                Console.WriteLine("in horse");
                xTemplist = new List<int> { x + 2, x + 2, x - 2, x - 2, x + 1, x + 1, x - 1, x - 1 };
                yTemplist = new List<int> { y + 1, y - 1, y + 1, y - 1, y + 2, y - 2, y + 2, y - 2 };
                //以上初始化两个临时List，使其符合马的行走方式。
                //以下删减List中的元素，使其符合马的行走限制。
                this.xlist = xTemplist;
                this.ylist = yTemplist;
                removeOut(1);//把超出棋盘的坐标删掉

                if (x - 1 >= 0 && refArrTable[x - 1, y] != null)
                {
                    removeBan(x - 2,"x");
                }//左边马脚

                if (x + 1 < 10 && refArrTable[x + 1, y] != null)
                {
                    removeBan(x + 2,"x");
                }//右边马脚

                if (y + 1 < 10 && refArrTable[x, y + 1] != null)
                {
                    removeBan(y + 2, "y");
                }//上马脚
                
                if (y - 1 >= 0 && refArrTable[x, y - 1] != null)
                {
                    removeBan(y - 2, "y");
                }//下马脚
                possibleMove(xlist, ylist);
            }
            if(chosePiece is Pawn) // 兵
            {
                PossibleMove_pawn(x,y);
            }
            if(chosePiece is Cannon) // 炮
            {
                PossibleMove_Canon(x, y);
            }
            if (chosePiece is Rook) // 俥
            {
                PossibleMove_Rook(x, y);
            }
        }
        public void possibleMove(List<int> x, List<int> y) //用来确认空子+确认不是己方棋子，改变颜色
        {
            Console.WriteLine("in possible move");
            for (int i = 0; i < x.Count(); i++)
            {
                //如果目标点为空，或者为对方棋子，则变色。否则不变色。
                Console.WriteLine(x[i]);
                Console.WriteLine(y[i]);
                if (refArrTable[x[i], y[i]] == null) { Console.WriteLine("xi,yi == null"); }
                if (refArrTable[x[i], y[i]] == null 
                    || refArrTable[x[i], y[i]].getColor() != myColor)//原来是!= 但是改了就对了,不知道为什么
                {
                    refDataTable.tableChangeColorActive(x[i], y[i]);
                }
            }
            xlist.Clear();
            ylist.Clear();
        }
        public void possibleMove(int x, int y) //如果只有一个数
        {
            List<int> xtempList = new List<int>{ x };
            List<int> ytempList = new List<int> { y };
            possibleMove(xtempList, ytempList);
        }
        public void possibleMove(List<int> x, int y) //如果只有一个y
        {
            int i = x.Count();
            List<int> ytempList = new List<int>();
            for(int j=0; j<i; j++)
            {
                ytempList.Add(y);
            }
            possibleMove(x, ytempList);
        }
        public void possibleMove(int x, List<int> y) //如果只有一个x
        {
            int i = y.Count();
            List<int> xtempList = new List<int>();
            for (int j = 0; j < i; j++)
            {
                xtempList.Add(x);
            }
            possibleMove(xtempList, y);
        }
        public void removeOut(int mode)
        {
            int xindex = 0;
            Predicate<int> xInBoard = xi => //x-axis out of the board.
            {
                return xi < 0 || xi > 8;
            };
            Predicate<int> yInBoard = yi => //y-axis out of the board.
            {
                return yi < 0 || yi > 9;
            };
            Predicate<int> bottomHalfBoard = yi => //y-axis out of the bottom half-board.
            {
                return yi > 5;
            };
            Predicate<int> topHalfBoard = yi => //y-axis out of the top half-board.
            {
                return yi > 4;
            };
            Predicate<int> xInTNight = xi => //x-axis out of the Jiugongge(T9).
            {
                return xi < 3 || xi > 5;
            };
            Predicate<int> yInTNight = yi => //y-axis out of the Jiugongge(T9).
            {
                return yi > 2;
            };
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
                    while ((this.ylist.FindIndex(yInBoard) != -1))
                    {
                        xindex = this.ylist.FindIndex(yInBoard);
                        this.xlist.RemoveAt(xindex);
                        this.ylist.RemoveAt(xindex);
                    }
                    break;

                case 2:
                    removeOut(1);
                    while ((this.ylist.FindIndex(topHalfBoard) != -1))
                    {
                        xindex = this.ylist.FindIndex(topHalfBoard);
                        this.xlist.RemoveAt(xindex);
                        this.ylist.RemoveAt(xindex);
                    }
                    break;

                case 3:
                    removeOut(1);
                    while ((this.ylist.FindIndex(bottomHalfBoard) != -1))
                    {
                        xindex = this.ylist.FindIndex(bottomHalfBoard);
                        this.xlist.RemoveAt(xindex);
                        this.ylist.RemoveAt(xindex);
                    }
                    break;
                case 4:
                    while ((this.ylist.FindIndex(yInTNight) != -1))
                    {
                        xindex = this.ylist.FindIndex(yInTNight);
                        this.xlist.RemoveAt(xindex);
                        this.ylist.RemoveAt(xindex);
                    }
                    while ((this.xlist.FindIndex(xInTNight) != -1))
                    {
                        xindex = this.xlist.FindIndex(xInTNight);
                        this.xlist.RemoveAt(xindex);
                        this.ylist.RemoveAt(xindex);
                    }
                    break;

            }
        }
        public void removeBan(int objective,string mode)
        {
            int xindex = 0;
            Predicate<int> match = xi =>
            {
                return xi == objective;
            };
            if (mode == "x")
            {
                while (this.xlist.Contains(objective))
                {
                    xindex = this.xlist.FindIndex(match);
                    this.xlist.RemoveAt(xindex);
                    this.ylist.RemoveAt(xindex);
                }
            }
            else
            {
                while (this.ylist.Contains(objective))
                {
                    xindex = this.ylist.FindIndex(match);
                    this.ylist.RemoveAt(xindex);
                    this.xlist.RemoveAt(xindex);
                }
            }
            
            
        }

        public void PossibleMove_pawn(int x, int y)//小兵的可移动方式
        {
            List<int> xlist = new List<int> { x + 1, x - 1, x, x };
            List<int> ylist = new List<int> { y, y, y + 1, y - 1 };
            switch (refArrTable[x, y].getColor())
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
                    possibleMove(xlist, ylist);
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
                    for (int i = 0; i < ylist.Count; i++)
                    {

                        Console.WriteLine("y:" + ylist[i]);
                    }
                    for (int i = 0; i < xlist.Count; i++)
                    {
                        Console.WriteLine("x:" + xlist[i]);
                    }
                    possibleMove(xlist, ylist);
                    break;
            }

        }
        public void PossibleMoveBan_Rook(List<int> xlist, List<int> ylist, int judge, int x, int y)
        {

            List<int> templist = new List<int> { };
            templist = xlist.FindAll(delegate (int i) { return (i == judge); });
            for (int i = judge - 1; i > -1; i--)
            {
                if (refArrTable[xlist[i], ylist[i]] != null)
                {
                    if (refArrTable[x, y].GetType() == typeof(Cannon))
                    {
                        ylist.RemoveRange(0, i + 1);
                        xlist.RemoveRange(0, i + 1);
                    }
                    else
                    {
                        ylist.RemoveRange(0, i);
                        xlist.RemoveRange(0, i);
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
            j = xlist.FindIndex(match);
            k = ylist.FindIndex(match);
            if (templist.Count == 1)
            {
                k = j;
            }
            for (int i = k + 1; i < xlist.Count; i++)
            {
                if (refArrTable[xlist[i], ylist[i]] != null)
                {
                    if (refArrTable[x, y].GetType() == typeof(Cannon))
                    {
                        ylist.RemoveRange(i, ylist.Count - i);
                        xlist.RemoveRange(i, xlist.Count - i);
                    }
                    else
                    {
                        ylist.RemoveRange(i + 1, ylist.Count - i - 1);
                        xlist.RemoveRange(i + 1, xlist.Count - i - 1);
                    }
                    break;
                }
            }
            possibleMove(xlist, ylist);
        }//车和炮的行走规则
        public void PossibleMoveBan_canon(List<int> xlist, List<int> ylist, int judge, int x, int y)
        {

            List<int> templist = new List<int> { };
            List<int> xtemplist = new List<int> { };
            templist = xlist.FindAll(delegate (int i) { return (i == judge); });
            for (int i = judge - 1; i > -1; i--)
            {
                if (refArrTable[xlist[i], ylist[i]] != null)
                {
                    for (int j = i - 1; j > -1; j--)
                    {
                        if (refArrTable[xlist[j], ylist[j]] != null)
                        {
                            possibleMove(xlist[j], ylist[j]);
                            break;
                        }
                    }
                    break;
                }

            }
            for (int i = judge + 1; i < xlist.Count; i++)
            {
                if (refArrTable[xlist[i], ylist[i]] != null)
                {
                    for (int j = i + 1; j < xlist.Count; j++)
                    {
                        if (refArrTable[xlist[j], ylist[j]] != null)
                        {
                            possibleMove(xlist[j], ylist[j]);
                            break;
                        }
                    }
                    break;
                }

            }
        }//如何打炮
        public void PossibleMove_Rook(int x, int y)//车的可移动方式
        {
            List<int> xlist = new List<int> { };
            List<int> blist = new List<int> { };
            List<int> ylist = new List<int> { };
            List<int> alist = new List<int> { };

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
            PossibleMoveBan_Rook(xlist, ylist, x, x, y);
            PossibleMoveBan_Rook(alist, blist, y, x, y);
        }
        public void PossibleMove_Canon(int x, int y)//炮的可移动方式
        {
            List<int> xlist = new List<int> { };
            List<int> blist = new List<int> { };
            List<int> ylist = new List<int> { };
            List<int> alist = new List<int> { };

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
            PossibleMoveBan_canon(xlist, ylist, x, x, y);
            PossibleMoveBan_canon(alist, blist, y, x, y);
        }
        public Piece[,] MoveP(Tuple<int, int> location_select, Tuple<int, int> location_move)
        {
            refArrTable[location_move.Item1, location_move.Item2] = refArrTable[location_select.Item1, location_select.Item2];
            refArrTable[location_select.Item1, location_select.Item2] = null;//空棋子或者空
            return refArrTable;
        }//移动棋子
        public void moveandCheckWin()
        {


        }
    }
}
