using System;
using System.Collections.Generic;
using System.Text;

namespace KING_OF_XIANGQI
{
    public class Table
    {
        private Piece[,] arr;
        private ConsoleColor BackgroundColor = ConsoleColor.Yellow;
        private int[,] Color;
        private (int,int) PrePoint = (0,0); 

        public void setArr(int a, int b) // set the length and height of the 2d array.
        {
            this.arr = new Piece[a, b];
        }
        public Piece[,] getArr() //get the 2d array of the chessboard.
        {
            return this.arr;
        }
        public Piece getPiece(int c, int d) //get the piece in the  (c,d) coordinate
        {
            return arr[c, d];
        }
        public void tableChangeColorActive(int x, int y) //list the coordinate which have changed color.
        {
            Color[x, y] = 1;

        }
        public int[] getColor()
        {
            return Color;
        }
        public void setPreviousPoint(int x,int y)
        {
            this.PrePoint = (x,y);
        }
        public (int,int) getPreviousPoint()
        {
            return PrePoint;
        }
    }
}
