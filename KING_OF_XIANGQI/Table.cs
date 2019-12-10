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
    }
}
