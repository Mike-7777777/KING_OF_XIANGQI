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
            dataTable.InitColor();
            dataTable.InitArr();
            while (Play(controller, dataTable, view, red) == false && Play(controller,dataTable, view, black) == false)
            {
                dataTable.InitColor();
                Play(controller,dataTable, view, red);
                Play(controller,dataTable, view, black);
            }

        }
        public static bool Play(Controller controller, Table table, View view, string color)
        {
            bool checkwin = false;
            if (checkwin == false)
            {
                Console.WriteLine("It's " + color + "'s round, please Choose a piece in coordinate");
                int[] number = view.GetRead();
                int x = number[0];
                int y = number[1];
                var position1 = new Tuple<int, int>(x, y);
                controller.ChooseP(x, y, color);
                view.PossibleMovementPoint(table); //选择棋子 以及展示可以行走的棋子的possible movement（变色）.
                Console.WriteLine("Please enter the coordinate that you want to go");
                int[] num = view.GetRead();
                int x1 = num[0];
                int y1 = num[1];
                var position2 = new Tuple<int, int>(x1, y1);//接收棋子目的地的坐标
                Piece choosePiece;
                choosePiece = table.GetPiece(x1, y1);
                controller.MoveP(position1, position2);//调用View的possiblemove方法 //运行Controller-Move方法
                view.PositionChangingDisplay(table); //Console.WriteLine("333棋子走动的位置改变"); 
                if (choosePiece is General)
                {
                    Console.WriteLine("game is over!");
                    checkwin = true;
                }
            }
            return checkwin;
        }
        

    }
}