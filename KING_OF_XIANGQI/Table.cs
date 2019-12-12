using System;
using System.Collections.Generic;
using System.Text;

namespace KING_OF_XIANGQI
{
    public class Table
    {
        private Piece[,] arr = new Piece[9,10];
        private int[,] Color = new int[9, 10];


        public void setArr(int a, int b) // set the length and height of the 2d array.
        {
            this.arr = new Piece[a, b];
        }
        public void initArr()
        {
            for (int i = 0; i < Color.GetLength(0); i++)
            {
                for (int j = 0; j < Color.GetLength(1); j++)
                {
                    Color[i, j] = 0;
                }
            }
            General general1 = new General("Black"); //jiang
            General general2 = new General("Red");
            Rook rook1 = new Rook("Black"); //che
            Rook rook2 = new Rook("Black");
            Rook rook3 = new Rook("Red");
            Rook rook4 = new Rook("Red");
            Horse horse1 = new Horse("Black");//ma
            Horse horse2 = new Horse("Black");
            Horse horse3 = new Horse("Red");
            Horse horse4 = new Horse("Red");
            Elephant elephant1 = new Elephant("Black");//xiang
            Elephant elephant2 = new Elephant("Black");
            Elephant elephant3 = new Elephant("Red");
            Elephant elephant4 = new Elephant("Red");
            Mandarin mandarin1 = new Mandarin("Black");//shi
            Mandarin mandarin2 = new Mandarin("Black");
            Mandarin mandarin3 = new Mandarin("Red");
            Mandarin mandarin4 = new Mandarin("Red");
            Cannon cannon1 = new Cannon("Black");//pao
            Cannon cannon2 = new Cannon("Black");
            Cannon cannon3 = new Cannon("Red");
            Cannon cannon4 = new Cannon("Red");
            Pawn pawn1 = new Pawn("Black");//zu
            Pawn pawn2 = new Pawn("Black");
            Pawn pawn3 = new Pawn("Black");
            Pawn pawn4 = new Pawn("Black");
            Pawn pawn5 = new Pawn("Black");
            Pawn pawn6 = new Pawn("Red");
            Pawn pawn7 = new Pawn("Red");
            Pawn pawn8 = new Pawn("Red");
            Pawn pawn9 = new Pawn("Red");
            Pawn pawn10 = new Pawn("Red");
            arr[4, 0] = general1;
            arr[4, 9] = general2;
            arr[0, 9] = rook1;
            arr[8, 9] = rook2;
            arr[0, 0] = rook3;
            arr[8, 0] = rook4;
            arr[1, 9] = horse1;
            arr[7, 9] = horse2;
            arr[1, 0] = horse3;
            arr[7, 0] = horse4;
            arr[2, 9] = elephant1;
            arr[6, 9] = elephant2;
            arr[2, 0] = elephant3;
            arr[6, 0] = elephant4;
            arr[3, 9] = mandarin1;
            arr[5, 9] = mandarin2;
            arr[3, 0] = mandarin3;
            arr[5, 0] = mandarin4;
            arr[0, 6] = pawn1;
            arr[2, 6] = pawn2;
            arr[4, 6] = pawn3;
            arr[6, 6] = pawn4;
            arr[8, 6] = pawn5;
            arr[0, 3] = pawn6;
            arr[2, 3] = pawn7;
            arr[4, 3] = pawn8;
            arr[6, 3] = pawn9;
            arr[8, 3] = pawn10;

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
        public int[,] getColor()
        {
            return Color;
        }
    }
}
