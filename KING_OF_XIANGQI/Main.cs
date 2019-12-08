using System;
using System.Collections.Generic;
using System.Text;

namespace KING_OF_XIANGQI
{
    class Model
    {
        public abstract class Piece
        {
            int[,] location;
            string color;
            string name;
            Boolean dead;
            public void MovingRules()
            {

            }
        }
        public class General : Piece
        {

        }
        public class Rook : Piece
        {

        }
        class Horse : Piece
        {

        }
        class Elephant : Piece
        {

        }
        class Mandarin : Piece
        {

        }
        class Cannon : Piece
        {

        }
        class Pawn : Piece
        {

        }
    }
}
