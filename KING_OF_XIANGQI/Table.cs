using System;
using System.Collections.Generic;
using System.Text;

namespace KING_OF_XIANGQI
{
    public class Table
    {
        public Piece[,] arr;
        private int[,] Color;
        ////版本改动(view1)
        private int[] chosePiece;

        public Table()
        {
            this.arr = new Piece[9, 10];
            this.Color = new int[9, 10];
            this.chosePiece = new int[2];
        }
        public void SetChosePiece(int x, int y)
        {
            chosePiece[0] = x;
            chosePiece[1] = y;
        }
        public int[] GetChosePiece()
        {
            return chosePiece;
        }
        ////版本改动(view1)
        public void SetArr(int a, int b, Piece piece) // set the length and height of the 2d array.
        {
            arr[a, b] = piece;
        }
        public void NullArr(int a, int b)
        {
            arr[a, b] = null;
        }
        public void InitArr()
        {

            arr[4, 9] = new General("Black");
            arr[4, 0] = new General("Red");
            arr[0, 9] = new Rook("Black");
            arr[8, 9] = new Rook("Black");
            arr[0, 0] = new Rook("Red");
            arr[8, 0] = new Rook("Red");
            arr[1, 9] = new Horse("Black");
            arr[7, 9] = new Horse("Black");
            arr[1, 0] = new Horse("Red");
            arr[7, 0] = new Horse("Red");
            arr[2, 9] = new Elephant("Black");
            arr[6, 9] = new Elephant("Black");
            arr[2, 0] = new Elephant("Red");
            arr[6, 0] = new Elephant("Red");
            arr[3, 9] = new Mandarin("Black");
            arr[5, 9] = new Mandarin("Black");
            arr[3, 0] = new Mandarin("Red");
            arr[5, 0] = new Mandarin("Red");
            arr[0, 6] = new Pawn("Black");
            arr[2, 6] = new Pawn("Black");
            arr[4, 6] = new Pawn("Black");
            arr[6, 6] = new Pawn("Black");
            arr[8, 6] = new Pawn("Black");
            arr[0, 3] = new Pawn("Red");
            arr[2, 3] = new Pawn("Red");
            arr[4, 3] = new Pawn("Red");
            arr[6, 3] = new Pawn("Red");
            arr[8, 3] = new Pawn("Red");
            arr[1, 7] = new Cannon("Black");
            arr[7, 7] = new Cannon("Black");
            arr[1, 2] = new Cannon("Red");
            arr[7, 2] = new Cannon("Red");
        }
        public Piece[,] GetArr() //get the 2d array of the chessboard.
        {
            return this.arr;
        }
        public Piece GetPiece(int c, int d) //get the piece in the  (c,d) coordinate
        {
            return arr[c, d];
        }
        public void TableChangeColorActive(int x, int y) //list the coordinate which have changed color.
        {
            Color[x, y] = 1;

        }
        public int[,] GetColor()
        {
            return Color;
        }
        public void InitColor()
        {
            for (int i = 0; i < Color.GetLength(0); i++)
            {
                for (int j = 0; j < Color.GetLength(1); j++)
                {
                    Color[i, j] = 0;
                }
            }
        }
    }
}
