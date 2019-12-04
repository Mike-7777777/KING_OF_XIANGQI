using System;

namespace KING_OF_XIANGQI
{
    public abstract class Chess
    {
        int[,] location;
        string color;
        string name;
        Boolean dead;
        public void MovingRules()
        {

        }
        public void Initialization()
        {

        }
    }
    class General : Chess
    {

    }
    class Rook : Chess
    {

    }
    class Horse : Chess
    {

    }
    class Elephant : Chess
    {

    }
    class Mandarin : Chess
    {

    }
    class Cannon : Chess
    {

    }
    class Pawn : Chess
    {

    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Hello Bart!");
            Console.WriteLine9("Hello TYL!"); 
        }
    }
}
