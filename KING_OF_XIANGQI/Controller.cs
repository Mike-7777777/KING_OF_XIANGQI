using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KING_OF_XIANGQI
{
    class Controller
    {
        private int prop1 = 0;
        private string myColor;
        private List<int> xlist = new List<int>();
        private List<int> ylist = new List<int>();
        public void chooseP(int x, int y, Table dataTable, string myColor)
        //x,y is the location of piece that user chose. 
        //dataTable is a gameboard object.
        //This method is used to active one piece  
        //and predict the point it could get.
        //颜色string为了定义本局行走的颜色。1是黑色，0是红色。
        {
            //储存本次操作的颜色。
            this.myColor = myColor;
            //new a piece array that save the array got from database.
            Piece[,] arrPieces = new Piece[9, 10];//如果最后不需要可以删掉
            //define a Piece object chosePiece to store the Piece pass by database.
            Piece chosePiece;
            int n = 0; //数组大小
            int[] xarr = new int[n];//x坐标数组
            int[] yarr = new int[n];//y坐标数组

            List<int> xTemplist = new List<int>(); //x坐标list 临时
            List<int> yTemplist = new List<int>(); //y坐标list 临时

            //recieve the arrObject from database.
            arrPieces = dataTable.getArr(); //如果最后不需要可以删掉
            //use getPiece method and pass two variables as     
            //the location to get the object we need.
            //使用getPiece方法，传入两个表示坐标的变量，收到对象棋子。
            chosePiece = dataTable.getPiece(x, y);
            //问题是这里的Piece的类型是什么，本来定义的是Piece，但显然传输过来的应该是子类（具体棋子）。
            //写好Table以后需要再看一下。
            //分辨棋子类型改变数据库中棋盘的颜色。
            ////版本改动(view1)
            dataTable.setChosePiece(x, y);
            ////版本改动(view1)
            if (chosePiece.GetType() == typeof(General)) //如果这个棋子是“将”
            {
                Console.WriteLine("this is general");
                //1. 找到相应的位置
                //2. 将对应位置改变属性，再用View令其变色。

                //左右走
                if (x == 3 || x == 5)
                {
                    //如果坐标为3或5，只能去4，且需要4为空
                    possibleMove(4, y,dataTable);
                }
                else if (x == 4) 
                {
                    //如果坐标为4，35如果为空，则能去
                    possibleMove(3, y, dataTable);
                    possibleMove(5, y, dataTable);

                }
                //上下走
                if (y == 0 || y == 2)
                {
                    //如果坐标为3或5，只能去4
                    Console.WriteLine("in if y == 0 or 2");
                    possibleMove(x, 1, dataTable);
                }
                else if (y == 9 || y == 7)
                {
                    //如果坐标为3或5，只能去4
                    possibleMove(x, 8, dataTable);
                }
                else if (y == 1)
                {
                    //如果坐标为4，35都能去
                    possibleMove(x, 0, dataTable);
                    possibleMove(x, 2, dataTable);

                }
                else if (y == 8)
                {
                    //如果坐标为4，35都能去
                    possibleMove(x, 9, dataTable);
                    possibleMove(x, 7, dataTable);

                }


            }
            //士 3~5，0~2
            if (chosePiece is Mandarin)
            {
                
                if (x == 4)
                {
                    //n = 4; 
                    Console.WriteLine("in if == 4");
                    xarr = new int[4] { 3, 3, 5, 5 };
                    xlist.AddRange(xarr);
                    if (y > 2)
                    {
                        Console.WriteLine("in if y > 2");
                        yarr = new int[4] { 7, 9, 7, 9 };
                        ylist.AddRange(yarr);
                    }
                    else
                    {
                        Console.WriteLine("in if y !> 2 else");
                        yarr = new int[4] { 2, 0, 2, 0 };
                        ylist.AddRange(yarr);
                    }
                    possibleMove(xlist, ylist, dataTable);
                }
                else
                {
                    if(y > 1)
                    {
                        possibleMove(4, 8, dataTable);
                    }
                    else
                    {
                        possibleMove(4, 1, dataTable);

                    }
                }
            }

            //象
            if (chosePiece is Elephant)
            {
                Console.WriteLine("in Elephant");
                // 右上、右下、左上、左下。
                this.xlist = new List<int> { x + 2, x + 2, x - 2, x - 2 };
                this.ylist = new List<int> { y + 2, y - 2, y + 2, y - 2 };
                List<int> xBan = new List<int> { x + 1, x + 1, x - 1, x - 1 };
                List<int> yBan = new List<int> { y + 1, y + 1, y - 1, y - 1 };
                //以上初始化两个临时List，使其符合象的行走方式。
                //以下删减List中的元素，使其符合象的行走限制。
                if (x == 4)
                {
                    Console.WriteLine("in elephant x == 4");
                    possibleMove(this.xlist, this.ylist, dataTable);
                }
                else if (y < 5) //在下半边
                {
                    Console.WriteLine("in elephant y<5");
                    //将符合条件（y坐标在0-4中）的坐标存入新ylist
                    //将符合条件（y坐标在0-8中）的坐标存入新xlist
                    removeOut(2);
                    foreach(int i in xlist)
                    {
                        Console.Write(i);
                    }
                    Console.WriteLine();
                    foreach (int i in ylist)
                    {
                        Console.Write(i);
                    }
                    Console.WriteLine();
                }
                else if (y > 4) //在上半边
                {
                    Console.WriteLine("in elephant y>4");
                    //将符合条件（x坐标在5-9中）的坐标存入新ylist
                    //将符合条件（y坐标在0-8中）的坐标存入新xlist
                    removeOut(1);
                }
                
                if (y + 1 < 10 && x - 1 >= 0 && arrPieces[x - 1, y + 1] != null)
                {
                    removeBan(y + 1, "y");
                }//左上象腿
                if (y - 1 >= 0 && x - 1 >= 0 && arrPieces[x - 1, y - 1] != null)
                {
                    removeBan(y - 1, "y");
                }//左下象腿
                if (y + 1 < 10 && x + 1 < 10 && arrPieces[x + 1, y + 1] != null)
                {
                    removeBan(y + 1, "y");
                }//左上象腿
                if (x + 1 < 10 && y - 1 >= 0 && arrPieces[x + 1, y - 1] != null)
                {
                    removeBan(y - 1, "y");
                }//左上象腿
                possibleMove(xlist, ylist, dataTable);
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

                if (x - 1 >= 0 && arrPieces[x - 1, y] != null)
                {
                    removeBan(x - 2,"x");
                }//左边马脚

                if (x + 1 < 10 && arrPieces[x + 1, y] != null)
                {
                    removeBan(x + 2,"x");
                }//右边马脚

                if (y + 1 < 10 && arrPieces[x, y + 1] != null)
                {
                    removeBan(y + 2, "y");
                }//上马脚
                
                if (y - 1 >= 0 && arrPieces[x, y - 1] != null)
                {
                    removeBan(y - 2, "y");
                }//下马脚
                possibleMove(xlist, ylist, dataTable);
            }
            if(chosePiece is Pawn) // 兵
            {
                PossibleMove_pawn(x,y,dataTable);
            }
            if(chosePiece is Cannon) // 炮
            {
                PossibleMove_Canon(x, y, dataTable);
            }
            if (chosePiece is Rook) // 俥
            {
                PossibleMove_Rook(x, y, dataTable);
            }
        }
        public void possibleMove(List<int> x, List<int> y, Table dataTable) //用来确认空子+确认不是己方棋子，改变颜色
        {
            Piece[,] arrPieces = dataTable.getArr();
            Console.WriteLine("in possible move");
            for (int i = 0; i < x.Count(); i++)
            {
                //如果目标点为空，或者为对方棋子，则变色。否则不变色。
                Console.WriteLine(x[i]);
                Console.WriteLine(y[i]);
                if (arrPieces[x[i], y[i]] == null) { Console.WriteLine("xi,yi == null"); }
                if (arrPieces[x[i], y[i]] == null 
                    || arrPieces[x[i], y[i]].getColor() != myColor)//原来是!= 但是改了就对了,不知道为什么
                {
                    dataTable.tableChangeColorActive(x[i], y[i]);
                }
            }
            xlist.Clear();
            ylist.Clear();
        }
        public void possibleMove(int x, int y, Table dataTable) //如果只有一个数
        {
            List<int> xtempList = new List<int>{ x };
            List<int> ytempList = new List<int> { y };
            possibleMove(xtempList, ytempList, dataTable);
        }
        public void possibleMove(List<int> x, int y, Table dataTable) //如果只有一个y
        {
            int i = x.Count();
            List<int> ytempList = new List<int>();
            for(int j=0; j<i; j++)
            {
                ytempList.Add(y);
            }
            possibleMove(x, ytempList, dataTable);
        }
        public void possibleMove(int x, List<int> y, Table dataTable) //如果只有一个x
        {
            int i = y.Count();
            List<int> xtempList = new List<int>();
            for (int j = 0; j < i; j++)
            {
                xtempList.Add(x);
            }
            possibleMove(xtempList, y, dataTable);
        }
        public void removeOut(int mode)
        {
            int xindex = 0;
            Predicate<int> matchOutX = xi =>
            {
                return xi < 0 || xi > 8;
            };
            Predicate<int> matchOutY = yi =>
            {
                return yi < 0 || yi > 9;
            };
            Predicate<int> matchOutYDown = yi =>
            {
                return yi > 5;
            };
            Predicate<int> matchOutYUp = yi =>
            {
                return yi > 4;
            };
            switch (mode)
            {// 1 default, 2 elephant up, 3 ele down(y<5)
             //
                case 1:
                    while ((this.xlist.FindIndex(matchOutX) != -1))
                    {
                        xindex = this.xlist.FindIndex(matchOutX);
                        this.xlist.RemoveAt(xindex);
                        this.ylist.RemoveAt(xindex);
                    }
                    while ((this.ylist.FindIndex(matchOutY) != -1))
                    {
                        xindex = this.ylist.FindIndex(matchOutY);
                        this.xlist.RemoveAt(xindex);
                        this.ylist.RemoveAt(xindex);
                    }
                    break;

                case 2:
                    removeOut(1);
                    while ((this.ylist.FindIndex(matchOutYUp) != -1))
                    {
                        xindex = this.ylist.FindIndex(matchOutYUp);
                        this.xlist.RemoveAt(xindex);
                        this.ylist.RemoveAt(xindex);
                    }
                    break;

                case 3:
                    removeOut(1);
                    while ((this.ylist.FindIndex(matchOutYDown) != -1))
                    {
                        xindex = this.ylist.FindIndex(matchOutYDown);
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

        public void PossibleMove_pawn(int x, int y, Table Table)//小兵的可移动方式
        {
            Piece[,] arrPieces = new Piece[9, 10];
            arrPieces = Table.getArr();
            List<int> xlist = new List<int> { x + 1, x - 1, x, x };
            List<int> ylist = new List<int> { y, y, y + 1, y - 1 };
            switch (arrPieces[x, y].getColor())
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
                    possibleMove(xlist, ylist, Table);
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
                    possibleMove(xlist, ylist, Table);
                    break;
            }

        }
        public void PossibleMoveBan_Rook(List<int> xlist, List<int> ylist, int judge, int x, int y, Piece[,] arrPieces, Table Table)
        {

            List<int> templist = new List<int> { };
            templist = xlist.FindAll(delegate (int i) { return (i == judge); });
            for (int i = judge - 1; i > -1; i--)
            {
                if (arrPieces[xlist[i], ylist[i]] != null)
                {
                    if (arrPieces[x, y].GetType() == typeof(Cannon))
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
                if (arrPieces[xlist[i], ylist[i]] != null)
                {
                    if (arrPieces[x, y].GetType() == typeof(Cannon))
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
            possibleMove(xlist, ylist, Table);
        }//车和炮的行走规则
        public void PossibleMoveBan_canon(List<int> xlist, List<int> ylist, int judge, int x, int y, Piece[,] arrPieces, Table Table)
        {

            List<int> templist = new List<int> { };
            List<int> xtemplist = new List<int> { };
            templist = xlist.FindAll(delegate (int i) { return (i == judge); });
            for (int i = judge - 1; i > -1; i--)
            {
                if (arrPieces[xlist[i], ylist[i]] != null)
                {
                    for (int j = i - 1; j > -1; j--)
                    {
                        if (arrPieces[xlist[j], ylist[j]] != null)
                        {
                            possibleMove(xlist[j], ylist[j], Table);
                            break;
                        }
                    }
                    break;
                }

            }
            for (int i = judge + 1; i < xlist.Count; i++)
            {
                if (arrPieces[xlist[i], ylist[i]] != null)
                {
                    for (int j = i + 1; j < xlist.Count; j++)
                    {
                        if (arrPieces[xlist[j], ylist[j]] != null)
                        {
                            possibleMove(xlist[j], ylist[j], Table);
                            break;
                        }
                    }
                    break;
                }

            }
        }//如何打炮
        public void PossibleMove_Rook(int x, int y, Table Table)//车的可移动方式
        {
            List<int> xlist = new List<int> { };
            List<int> blist = new List<int> { };
            List<int> ylist = new List<int> { };
            List<int> alist = new List<int> { };
            Piece[,] arrPieces = new Piece[9, 10];
            arrPieces = Table.getArr();

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
            PossibleMoveBan_Rook(xlist, ylist, x, x, y, arrPieces, Table);
            PossibleMoveBan_Rook(alist, blist, y, x, y, arrPieces, Table);
        }
        public void PossibleMove_Canon(int x, int y, Table Table)//炮的可移动方式
        {
            List<int> xlist = new List<int> { };
            List<int> blist = new List<int> { };
            List<int> ylist = new List<int> { };
            List<int> alist = new List<int> { };
            Piece[,] arrPieces = new Piece[9, 10];
            arrPieces = Table.getArr();

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
            PossibleMoveBan_canon(xlist, ylist, x, x, y, arrPieces, Table);
            PossibleMoveBan_canon(alist, blist, y, x, y, arrPieces, Table);
        }
        public Piece[,] MoveP(Tuple<int, int> location_select, Tuple<int, int> location_move, Piece[,] arrPieces)
        {
            arrPieces[location_move.Item1, location_move.Item2] = arrPieces[location_select.Item1, location_select.Item2];
            arrPieces[location_select.Item1, location_select.Item2] = null;//空棋子或者空
            return arrPieces;
        }//移动棋子
        public void moveandCheckWin()
        {


        }
    }
}
