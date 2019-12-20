using System;

namespace KING_OF_XIANGQI
{
    class Program
    {
        public static void Main()
        {
            string red = "Red";
            string black = "Black";
            View view = new View();
            Table dataTable = new Table();
            Controller controller = new Controller(dataTable);

            view.InitialBoardForDisplay();
            dataTable.InitArr();

            while (Play(controller, dataTable, view, red) == false && Play(controller,dataTable, view, black) == false)
            {
                Play(controller,dataTable, view, red);
                Play(controller,dataTable, view, black);
            }

        }
        public static bool Play(Controller controller, Table table, View view, string color)
        {
            bool checkwin = false;
            if (checkwin == false)
            {
                table.InitColor();
                Console.WriteLine("It's " + color + "'s round, please Choose a piece in coordinate（ex. 1,2 for left cornor horse）");
                int[] locationNum = view.GetRead(); //store location in int[2].
                int x = locationNum[0];
                int y = locationNum[1];
                var departure = new Tuple<int, int>(x, y); //store x,y together into a tuple.
                if (table.arr[x, y] == null)
                {
                    Console.WriteLine("no piece in the position");
                    
                    Play(controller,table, view, color);
                }                
                else
                {
                controller.ChooseP(x, y, color);    //controller choose one pice to move.
                view.PossibleMovementPoint(table);  //view make the color change.
                Console.WriteLine("Please enter the coordinate that you want to go");
                locationNum = view.GetRead();
                int x1 = locationNum[0];
                int y1 = locationNum[1];
                var destination = new Tuple<int, int>(x1, y1);  //store destination coordintes with Tuple.
                int[,] colorTable = table.GetColor();
                    if (colorTable[x1, y1] == 0)
                    {
                         Console.WriteLine("wrong place");
                    
                         Play(controller,table,view,color);
                    }                
                    else
                    {
                        controller.MoveP(departure, destination);//调用View的possiblemove方法 //运行Controller-Move方法
                        view.PositionChangingDisplay(table); //Console.WriteLine("333棋子走动的位置改变"); 
                        if ((table.GetPiece(x, y) is General && table.GetPiece(x1, y1) is General))
                        {
                        Console.WriteLine("game is over!");
                        checkwin = true;
                        }
                    }
                }
            }
            return checkwin;
        }
        

    }
}
