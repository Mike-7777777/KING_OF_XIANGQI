using System;

namespace KING_OF_XIANGQI
{
    class Program
    {
        public static void Main()
        {
            string red = "Red";

            View a = new View();
            Table dataTable = new Table();
            Controller control = new Controller();
            //Console.WriteLine("111展示棋盘（带棋子）");
            a.InitialBoardForDisplay();
            dataTable.initColor();
            int[] num = new int[2];
            //选择棋子 以 及 展示可以行走的棋子的possible movement（变色）
            // datatable 为 table 类型的初始棋盘

            //棋子走动的位置改变

            dataTable.initArr();
            while (true)
            {
                dataTable.initColor();
                Console.WriteLine("Red first. Please Choose a piece in coordinate");   //你好，红先黑后，请红方选择棋子。
                num = getRead();
                int redx = num[0];
                int redy = num[1];
                var red1 = new Tuple<int, int>(redx, redy);
                //接收坐标。
                Console.WriteLine(redx + redy);//test
                control.chooseP(redx, redy, dataTable, red); //运行Controller.chooseP（）方法 
                int[,] intarr = dataTable.getColor();
                foreach (int i in intarr)
                {
                    Console.Write(i);
                }
                Console.WriteLine();


                a.PossibleMovementPoint(dataTable);//Console.WriteLine("222选择棋子 以 及 展示可以行走的棋子的possible movement（变色）");

                Console.WriteLine("Please enter the coordinate that you want to go");
                num = getRead();
                int redx1 = num[0];
                int redy1 = num[1];
                var red2 = new Tuple<int, int>(redx1, redy1);//接收棋子目的地的坐标
                Piece choosePiece;
                choosePiece = dataTable.getPiece(redx1, redy1);
                if (choosePiece is General)
                {
                    control.MoveP(red1, red2, dataTable.getArr());//调用View的possiblemove方法 //运行Controller-Move方法
                    a.PositionChangingDisplay(dataTable);//Console.WriteLine("333棋子走动的位置改变"); 
                    Console.WriteLine("game is over!");
                    break;//检测是否游戏结束
                }
                else
                {
                    control.MoveP(red1, red2, dataTable.getArr());//调用View的possiblemove方法 //运行Controller-Move方法
                    a.PositionChangingDisplay(dataTable);//Console.WriteLine("333棋子走动的位置改变"); 
                }    //刷新View的刷新方法




                //black
                dataTable.initColor();
                Console.WriteLine("Black now. Please Choose a piece in coordinate");   //黑方选择棋子。
                num = getRead();
                int blackx = num[0];
                int blacky = num[1];
                var black1 = new Tuple<int, int>(blackx, blacky);
                //接收坐标。
                Console.WriteLine(blackx + blacky);//test
                control.chooseP(blackx, blacky, dataTable, "Black"); //运行Controller.chooseP（）方法 
                int[,] intbalckarr = dataTable.getColor();
                foreach (int i in intbalckarr)
                {
                    Console.Write(i);
                }
                Console.WriteLine();
                a.PossibleMovementPoint(dataTable);//Console.WriteLine("222选择棋子 以 及 展示可以行走的棋子的possible movement（变色）");

                Console.WriteLine("Please enter the coordinate that you want to go");
                num = getRead();
                int blackx1 = num[0];
                int blacky1 = num[1];
                var black2 = new Tuple<int, int>(blackx1, blacky1);//接收棋子目的地的坐标
                Piece choosePiece2;
                choosePiece2 = dataTable.getPiece(blackx1, blacky1);
                if (choosePiece is General)
                {
                    
                    control.MoveP(black1, black2, dataTable.getArr());//调用View的possiblemove方法 //运行Controller-Move方法
                    a.PositionChangingDisplay(dataTable);
                    Console.WriteLine("game is over!");
                    break;//Console.WriteLine("333棋子走动的位置改变"); //检测是否游戏结束
                }
                else
                {
                    control.MoveP(black1, black2, dataTable.getArr());//调用View的possiblemove方法 //运行Controller-Move方法
                    a.PositionChangingDisplay(dataTable);//Console.WriteLine("333棋子走动的位置改变"); 
                }
            }
            
        }
        public static int[] getRead()
        {
            string spot = Console.ReadLine();

            //用标点分开
            string[] spotarr = spot.Split(',');

            //输出并转化为int数组
            int[] num = new int[spotarr.Length];
            for (int i = 0; i < spotarr.Length; i++)
            {
                num[i] = int.Parse(spotarr[i]);
            }
            return num;
        }
    }
}
