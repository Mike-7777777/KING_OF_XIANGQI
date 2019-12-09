using System;
using System.Collections.Generic;
using System.Text;

namespace KING_OF_XIANGQI
{
    class Controller
    {
        private int prop1 = 0;
        private string myColor;
        private List<int> xlist;
        private List<int> ylist;
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

            List<int> xlist = new List<int>(); //x坐标list
            List<int> ylist = new List<int>(); //y坐标list

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
            if (chosePiece is General) //如果这个棋子是“将”
            {
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
                if (y == 0 || y == 2 || y == 9 || y == 7)
                {
                    //如果坐标为3或5，只能去4
                    possibleMove(x, 1, dataTable);
                }
                else if (y == 1 || y == 8)
                {
                    //如果坐标为4，35都能去
                    possibleMove(x, 0, dataTable);
                    possibleMove(x, 2, dataTable);

                }


            }
            //士 3~5，0~2
            if (chosePiece is Mandarin)
            {
                if (x == 4)
                {
                    //n = 4; 
                    xarr = new int[4] { 3, 3, 5, 5 };
                    xlist.AddRange(xarr);
                    if (y != 1)
                    {
                        yarr = new int[4] { 7, 9, 7, 9 };
                        ylist.AddRange(yarr);
                    }
                    else
                    {
                        yarr = new int[4] { 2, 0, 2, 0 };
                        ylist.AddRange(yarr);
                    }
                    possibleMove(xlist, ylist, dataTable);
                }
                else
                {
                    if(y != 1)
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
                // 右上、右下、左上、左下。
                xTemplist = new List<int> { x + 2, x + 2, x - 2, x - 2 };
                yTemplist = new List<int> { y + 2, y - 2, y + 2, y - 2 };
                List<int> xBan = new List<int> { x + 1, x + 1, x - 1, x - 1 };
                List<int> yBan = new List<int> { y + 1, y + 1, y - 1, y - 1 };
                //以上初始化两个临时List，使其符合象的行走方式。
                //以下删减List中的元素，使其符合象的行走限制。
                if (x == 4)
                {
                    possibleMove(xlist, ylist, dataTable);
                }
                else if (y < 5) //在下半边
                {
                    //将符合条件（y坐标在0-4中）的坐标存入新ylist
                    ylist = yTemplist.FindAll(
                        delegate(int i)
                        {
                            //限制X坐标范围在0-4之间。
                            return (i>=0 && i<=4);
                        }
                        );
                }
                else //在上半边
                {
                    //将符合条件（x坐标在5-9中）的坐标存入新ylist
                    ylist = yTemplist.FindAll(
                        delegate (int i)
                        {
                            //限制X坐标范围在0-4之间。
                            return (i >= 5 && i <= 8);
                        }
                        );
                }
                //将符合条件（y坐标在0-8中）的坐标存入新xlist
                //(由于对于y是否大于5，x的有效范围都不变所以写在if外)
                xlist = xTemplist.FindAll(
                    delegate (int i)
                    {
                            //限制X坐标范围在0-4之间。
                            return (i >= 0 && i <= 8);
                    }
                    );
                if (arrPieces[x - 1, y + 1] != null)
                {
                    removeBan(y + 1, "y");
                }//左上象腿
                if (arrPieces[x - 1, y - 1] != null)
                {
                    removeBan(y - 1, "y");
                }//左下象腿
                if (arrPieces[x + 1, y + 1] != null)
                {
                    removeBan(y + 1, "y");
                }//左上象腿
                if (arrPieces[x + 1, y - 1] != null)
                {
                    removeBan(y - 1, "y");
                }//左上象腿
                possibleMove(xlist, ylist, dataTable);
            }
            
            //马
            if (chosePiece is Horse)
            {
                xTemplist = new List<int> { x + 2, x + 2, x - 2, x - 2, x + 1, x + 1, x - 1, x - 1 };
                yTemplist = new List<int> { y + 1, y - 1, y + 1, y - 1, y + 2, y - 2, y + 2, y - 2 };
                //以上初始化两个临时List，使其符合马的行走方式。
                //以下删减List中的元素，使其符合马的行走限制。
                ylist = yTemplist.FindAll(
                    delegate (int i)
                    {
                        return (i >= 0 && i <= 8);
                    }
                    );
                xlist = xTemplist.FindAll(
                    delegate (int i)
                    {
                        return (i >= 0 && i <= 8);
                    }
                    );
                //把超出棋盘的坐标删掉
                this.xlist = xlist; //写入类属性，使得后面的removeBan函数使用更方便
                this.ylist = ylist;
                if (arrPieces[x - 1, y] != null)
                {
                    removeBan(x - 2,"x");
                }//左边马脚
                if (arrPieces[x + 1, y] != null)
                {
                    removeBan(x + 2,"x");
                }//右边马脚
                if (arrPieces[x, y + 1] != null)
                {
                    removeBan(y + 2, "y");
                }//上马脚
                if (arrPieces[x, y - 1] != null)
                {
                    removeBan(y - 2, "y");
                }//下马脚
                possibleMove(xlist, ylist, dataTable);
            }
        }
        public void possibleMove(List<int> x, List<int> y, Table dataTable) //用来确认空子+确认不是己方棋子，改变颜色
        {
            Piece[,] arrPieces = new Piece[9, 10];
            arrPieces = dataTable.getArr();
            foreach (int i in x)
            {
                //如果目标点为空，或者为对方棋子，则变色。否则不变色。
                if (arrPieces[x[i], y[i]] == null 
                    || arrPieces[x[i], y[i]].getColor() != myColor)
                {
                    dataTable.tableChangeColorActive(x[i], y[i]);
                }
            }
        }
        public void possibleMove(int x, int y, Table dataTable) //如果只有一个数
        {
            List<int> xtempList = new List<int>{ x };
            List<int> ytempList = new List<int> { y };
            possibleMove(xtempList, ytempList, dataTable);
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
        public int[] filterValue(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (删选条件)
                {
                    arr.splice(i, 1)
                  i = i - 1
                }
            }
            return arr
        }
    }
}
