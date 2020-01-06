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
        private List<int> rowlist;
        private List<int> conlist;
        private List<int> xBan;
        private List<int> yBan;
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
            this.rowlist = new List<int>();
            this.conlist = new List<int>();
        }
        public void ChooseP(int x, int y, string myColor)   //x,y is the location of piece that user chose. 
                                                            //This method is used to active one piece  
                                                            //and predict the point it could get.
                                                            //myColor string defined the rounding color, black & red.
        {

            this.myColor = myColor; //Store the player;

            Piece chosePiece; //define a Piece object chosePiece to store the Piece pass by database.

            chosePiece = refDataTable.GetPiece(x, y); //use getPiece method and pass two variables as     
                                                      //the location to get the object we need.

            //Distinguish pieces types
            refDataTable.SetChosePiece(x, y);
            switch (chosePiece.GetType().ToString())
            {
                case "KING_OF_XIANGQI.General":
                    xlist = new List<int> { x + 0, x + 0, x - 1, x + 1 }; // up, down, left, right.
                    ylist = new List<int> { y + 1, y - 1, y + 0, y + 0 }; //init two list, make it adapt it to the way the elephant walks.(following pieces is the same)
                    PieceRule(y, 5, 0, 1, 0);
                    break;
                case "KING_OF_XIANGQI.Mandarin":
                    xlist = new List<int> { x + 1, x + 1, x - 1, x - 1 }; // ↗, ↘, ↙, ↖.
                    ylist = new List<int> { y + 1, y - 1, y - 1, y + 1 };
                    PieceRule(y, 5, 0, 1, 0);
                    break;
                case "KING_OF_XIANGQI.Elephant":
                    xlist = new List<int> { x + 2, x + 2, x - 2, x - 2 };// ↗, ↘, ↙, ↖.
                    ylist = new List<int> { y + 2, y - 2, y - 2, y + 2 };
                    xBan = new List<int> { x + 1, x + 1, x - 1, x - 1 };//init two ban list used later to deletes the non-compliantelements.
                    yBan = new List<int> { y + 1, y - 1, y - 1, y + 1 };
                    PieceRule(y, 2, 1, 1, 1);//Deletes the elements in the List 
                                             //to meet the walking restrictions of the elephant.
                    break;
                case "KING_OF_XIANGQI.Horse":
                    //y↗ y↘ y↖ y↙, Y↗ Y↘ Y↖ Y↙
                    xlist = new List<int> { x + 2, x + 2, x - 2, x - 2, x + 1, x - 1, x + 1, x - 1 };
                    ylist = new List<int> { y + 1, y - 1, y + 1, y - 1, y + 2, y + 2, y - 2, y - 2 };
                    xBan = new List<int> { x + 1, x - 1, x, x };//→ ← ↑ ↓
                    yBan = new List<int> { y, y, y + 1, y - 1 };
                    PieceRule(y, 0, 0, 0, 1);
                    break;
                case "KING_OF_XIANGQI.Pawn":
                    xlist = new List<int> { x + 1, x - 1 };//Add possible paths for the pawn
                    ylist = new List<int> { y, y ,};
                    switch (refArrTable[x, y].GetColor())
                    {
                        case "Black":
                            xlist.Add(x);//Add the path forward
                            ylist.Add(y + 1);

                            if (y <= 4)//If the piece doesn't go over the edge removes the left and right possible paths
                            {
                                xlist.RemoveRange(0, 2);
                                ylist.RemoveRange(0, 2);
                            }
                            break;
                        case "Red":
                            xlist.Add(x);//Add the path forward
                            ylist.Add(y - 1);
                            if (y >= 5)//If the piece doesn't go over the edge removes the left and right possible paths
                            {
                                xlist.RemoveRange(0, 2);
                                ylist.RemoveRange(0, 2);
                            }
                            break;
                    }
                    RemoveOut(1);
                    PossibleMove(xlist, ylist);
                    break;
                case "KING_OF_XIANGQI.Cannon":
                    Reset(x, y);
                    GoStraight(x, x, y, xlist, ylist);
                    GoStraight(y, x, y, rowlist, conlist);
                    Reset(x, y);
                    CanonEat(x, x, y, xlist, ylist);
                    CanonEat(y, x, y, rowlist, conlist);
                    break;
                case "KING_OF_XIANGQI.Rook":
                    Reset(x, y);
                    GoStraight(x, x, y, xlist, ylist);
                    GoStraight(y, x, y, rowlist, conlist);
                    break;
            }
        }
        public void MoveP(Tuple<int, int> location_select, Tuple<int, int> location_move) //12.20 1：12 修改
        {
            refDataTable.SetArr(location_move.Item1, location_move.Item2, refArrTable[location_select.Item1, location_select.Item2]);    //由修改本地成员变量refArray
                                                                                                                                         //改为调用远端方法修改数据库
            refDataTable.NullArr(location_select.Item1, location_select.Item2);//空棋子或者空
            //return refArrTable;
        }// To move pieces.

        public void PieceRule(int y, int upDownMode, int banMode, int updown, int ban)
        {
            if (ban == 1)
            {
                Ban(xBan, yBan, banMode);
            }
            if (updown == 1)
            {
                if (y < 5) // separate the black and red general. 
                           //这里开始和下面士的方法其实完全一样，可以写在GoStraight外面，或者单独写方法调用。
                           //共同点是if(y<5) remove5, remove 6; possible();
                {
                    RemoveOut(upDownMode); //remove all points out of small 9 space up. 
                                           //'5' means a mode of RemoveOut method
                                           //(remove all points out of bottom board).
                }
                else
                {
                    RemoveOut(upDownMode + 1); //remove all points out of small 9 space down.
                                               //'6' means a mode of RemoveOut method.
                                               //(remove all points out of upper board)
                }
            }
            else
            {
                RemoveOut(1); // delete all points out of board.
            }
            PossibleMove(xlist, ylist); //可能没必要放里面，可以放switch的后（外）面。
        } //包括了possible move（）方法
        public void Ban(List<int> xBan, List<int> yBan, int mode) // mode 1 = ele, mode 0 = horse
        {
            int k = 0;
            for (int i = 0; i < 4; i++)//remove the points that cannot reach by piece elephant.
            {
                Boolean inRange = (xBan[i] < 9 && xBan[i] >= 0 && yBan[i] < 10 && yBan[i] >= 0);
                if (inRange && refArrTable[xBan[i], yBan[i]] != null)
                {
                    if (mode == 1)
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
            for (int i = 0; i < x.Count(); i++)
            {
                //if the destination is null, or the other player's piece, it will change color.
                if (refArrTable[x[i], y[i]] == null
                    || refArrTable[x[i], y[i]].GetColor() != myColor)
                {
                    refDataTable.TableChangeColorActive(x[i], y[i]);
                }
            }
            xlist.Clear();
            ylist.Clear();
        }
        public void PossibleMove(int x, int y) // if the list(not a list actually) only has one element.
        {
            List<int> xtempList = new List<int> { x };
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
                return yi > 4;
            }
            static bool topHalfBoard(int yi)
            {
                return yi < 5;
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
            {// 1 default, 2 elephant up, 3 ele down, 
             //4 nine space(x) 5 nine space(y down) 6 nine space(y up))
                case 1:
                    while ((this.xlist.FindIndex(xInBoard) != -1))
                    {
                        xindex = this.xlist.FindIndex(xInBoard);
                        this.xlist.RemoveAt(xindex);
                        this.ylist.RemoveAt(xindex);
                    }
                    while ((ylist.FindIndex(yInBoard) != -1))
                    {
                        xindex = ylist.FindIndex(yInBoard);
                        xlist.RemoveAt(xindex);
                        ylist.RemoveAt(xindex);
                    }
                    break;

                case 2:
                    RemoveOut(1);
                    while ((this.ylist.FindIndex(bottomHalfBoard) != -1))
                    {
                        xindex = this.ylist.FindIndex(bottomHalfBoard);
                        this.xlist.RemoveAt(xindex);
                        this.ylist.RemoveAt(xindex);
                    }
                    break;
                case 3:
                    RemoveOut(1);
                    while ((this.ylist.FindIndex(topHalfBoard) != -1))
                    {
                        xindex = this.ylist.FindIndex(topHalfBoard);
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
        
        public void GoStraight(int start, int x, int y, List<int> xlist, List<int> ylist)
        {
            int left = new int();
            int right = new int();
            List<int> templist = new List<int>();
            right = 9;
            for (int i = start - 1; i > -1; i--)//Check if there are any pieces on the left or under side of Rook or Cannon
            {
                if (refArrTable[xlist[i], ylist[i]] != null)//If a piece exists, save the position of the piece
                {
                    if (refArrTable[x, y].GetType() == typeof(Cannon))//If the chess piece that the user chooses is not a rook but a cannon, move the saved position back one bit
                    {
                        i = i + 1;
                    }
                    left = i;
                    break;
                }
            }
            for (int i = start + 1; i < xlist.Count; i++)//Check if there are any pieces on the left or under side of Rook or Cannon
            {
                if (refArrTable[xlist[i], ylist[i]] != null)//If a piece exists, save the position of the piece
                {
                    if (refArrTable[x, y].GetType() == typeof(Cannon))//If the chess piece that the user chooses is not a rook but a cannon, move the saved position back one bit
                    {
                        i = i - 1;
                    }
                    right = i;
                    break;
                }
            }
            if (xlist.Count == 9)
            {
                xlist = xlist.FindAll(delegate (int i) { return (i >= left && i <= right); });//Save the path between two pieces
                ylist.RemoveRange(0, ylist.Count - xlist.Count);
            }
            else
            {
                ylist = ylist.FindAll(delegate (int i) { return (i >= left && i <= right); });//Save the path between two pieces
                xlist.RemoveRange(0, xlist.Count - ylist.Count);
            }
            PossibleMove(xlist, ylist);
        }//Rule of go straightly of rook and cannon
        public void CanonEat(int judge,int x,int y,List<int> xlist, List<int> ylist)
        {
            for (int i = judge - 1; i > -1; i--)//Check if there are two pieces to the left or under cannon
            {
                if (refArrTable[xlist[i], ylist[i]] != null)
                {
                    for (int j = i - 1; j > -1; j--)
                    {
                        if (refArrTable[xlist[j], ylist[j]] != null)//Displays the position of the second piece
                        {
                            PossibleMove(xlist[j], ylist[j]);
                            Reset(x, y);
                            break;
                        }
                    }
                    break;
                }
            }
            for (int i = judge + 1; i < xlist.Count; i++)//Check if there are two pieces to the right or upper cannon
            {
                if (refArrTable[xlist[i], ylist[i]] != null)
                {
                    for (int j = i + 1; j < xlist.Count; j++)
                    {
                        if (refArrTable[xlist[j], ylist[j]] != null)//Displays the position of the second piece
                        {
                            PossibleMove(xlist[j], ylist[j]);
                            break;
                        }
                    }
                    break;
                }
            }
        }//Eat rules of Canon.
        public void Reset(int x, int y)
        {
            xlist.Clear();
            ylist.Clear();
            rowlist.Clear();
            conlist.Clear();
            for (int i = 0; i < 9; i++)//The row coordinates that either Rook or cannon might be in
            {
                xlist.Add(i);
                ylist.Add(y);
            }
            for (int i = 0; i < 10; i++)//The column coordinates that either Rook or cannon might be in
            {
                rowlist.Add(x);
                conlist.Add(i);
            }
        }//Reset the rook and canon list
    }
}
