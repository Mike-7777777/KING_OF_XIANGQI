using System;
using System.Collections.Generic;
using System.Text;

namespace KING_OF_XIANGQI
{
    public abstract class Piece
    {

        string color = "";
        //member variable

        public Piece(string color)
        {
            this.color = color;
        }
        public string getColor()
        {
            return color;
        }
    }
    public class General : Piece //children class
    {
        public General(string color)
            : base(color)
        { }
    }
    public class Rook : Piece //children class
    {
        public Rook(string color)
            : base(color)
        { }
    }
    class Horse : Piece //children class
    {
        public Horse(string color)
            : base(color)
        { }
    }
    class Elephant : Piece //children class
    {
        public Elephant(string color)
            : base(color)
        { }
    }
    class Mandarin : Piece //children class
    {
        public Mandarin(string color)
            : base(color)
        { }
    }
    class Cannon : Piece //children class
    {
        public Cannon(string color)
            : base(color)
        { }
    }
    class Pawn : Piece //children class
    {
        public Pawn(string color)
            : base(color)
        { }
    }
}
