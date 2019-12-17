﻿using System;
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
            this.refArrTable = dataTable.GetArr();  //recieve the arrObject from database.
            this.xlist = new List<int>();
            this.ylist = new List<int>();
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
                    ylist = new List<int> { y + 1, y + 1, y + 0, y + 0 };
                    RemoveOut(4); //remove all points out of small 9 space.
                    PossibleMove(xlist, ylist);
                    break;
                case "KING_OF_XIANGQI.Mandarin":
                    xlist = new List<int> { x + 1, x + 1, x - 1, x - 1 }; // ↗, ↘, ↙, ↖.
                    ylist = new List<int> { y + 1, y - 1, y - 1, y + 1 };
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
                    if (y > 5)
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
                    //y↗ y↗ y↖ y↙, Y↗ Y↗ Y↖ Y↙
                    xlist = new List<int> { x + 2, x + 2, x - 2, x - 2, x + 1, x + 1, x - 1, x - 1 };
                    ylist = new List<int> { y + 1, y - 1, y + 1, y - 1, y + 2, y - 2, y + 2, y - 2 };
                    //init two list, make it adapt it to the way the elephant walks.
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
        public Piece[,] MoveP(Tuple<int, int> location_select, Tuple<int, int> location_move)
        {
            refArrTable[location_move.Item1, location_move.Item2] = refArrTable[location_select.Item1, location_select.Item2];
            refArrTable[location_select.Item1, location_select.Item2] = null;//空棋子或者空
            return refArrTable;
        }// To move pieces.
        public void MoveandCheckWin()
        {


        }
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
                return yi < 4;
            };
            Predicate<int> xInTNine = xi => //x-axis out of the Jiugongge(T9).
            {
                return xi < 3 || xi > 5;
            };
            Predicate<int> yInTNine = yi => //y-axis out of the Jiugongge(T9).
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
                    while ((this.ylist.FindIndex(yInTNine) != -1))
                    {
                        xindex = this.ylist.FindIndex(yInTNine);
                        this.xlist.RemoveAt(xindex);
                        this.ylist.RemoveAt(xindex);
                    }
                    while ((this.xlist.FindIndex(xInTNine) != -1))
                    {
                        xindex = this.xlist.FindIndex(xInTNine);
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
                    PossibleMove(xlist, ylist);
                    break;
            }

        }
        public void PossibleMove_Rook(int x, int y)//Rook walk.
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
        public void PossibleMove_Cannon(int x, int y)//Canon walk.
        {
            List<int> blist = new List<int> { };
            List<int> alist = new List<int> { };

            for (int i = 0; i < 9; i++)//车可能所在的x轴位置
            {
                xlist.Add(i);
                blist.Add(y);

            }
            for (int i = 0; i < 10; i++)//车可能所在的y轴位置
            {
                alist.Add(x);
                ylist.Add(i);
            }
            PossibleMoveBan_Cannon(xlist, blist, x, x, y); // Mike 修改过 把alist变成了xlsit，blist变成了ylist。
            PossibleMoveBan_Cannon(alist, ylist, y, x, y);
        }
        public void PossibleMoveBan_Rook(List<int> xlist, List<int> ylist, int judge, int x, int y)
        {
            List<int> templist = new List<int> { };
            templist = xlist.FindAll(delegate (int i)
            {
                return (i == judge);
            });
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
            PossibleMove(xlist, ylist);
        }//Ban rules of Rook and Cannon.
        public void PossibleMoveBan_Cannon(List<int> xlist, List<int> ylist, int judge, int x, int y)
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
                            PossibleMove(xlist[j], ylist[j]);
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
                            PossibleMove(xlist[j], ylist[j]);
                            break;
                        }
                    }
                    break;
                }

            }
        }//Eat rules of Canon.
        public bool SelectandMove(Table table, View view, string color)
        {
            bool checkwin = false;
            if (checkwin == false)
            {
                Console.WriteLine("It's " + color + " time, please Choose a piece in coordinate");
                int[] number = getRead();
                int x = number[0];
                int y = number[1];
                var position1 = new Tuple<int, int>(x, y);
                ChooseP(x, y, color);
                view.PossibleMovementPoint(table); //选择棋子 以及展示可以行走的棋子的possible movement（变色）.
                Console.WriteLine("Please enter the coordinate that you want to go");
                int[] num = getRead();
                int x1 = num[0];
                int y1 = num[1];
                var position2 = new Tuple<int, int>(x1, y1);//接收棋子目的地的坐标
                Piece choosePiece;
                choosePiece = table.GetPiece(x1, y1);
                MoveP(position1, position2);//调用View的possiblemove方法 //运行Controller-Move方法
                view.PositionChangingDisplay(table); //Console.WriteLine("333棋子走动的位置改变"); 
                if (choosePiece is General)
                {
                    Console.WriteLine("game is over!");
                    checkwin = true;
                }
            }
            return checkwin;
        }
        public static int[] getRead()
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
