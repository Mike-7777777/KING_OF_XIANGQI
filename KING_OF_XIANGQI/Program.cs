using System;

namespace KING_OF_XIANGQI
{
    class Program
    {
        public static void Main()
        {
            View a = new View();
            Table dataTable = new Table();
            Controller control = new Controller();
            Console.WriteLine("111展示棋盘（带棋子）");
            a.InitialBoardForDisplay();

            Console.WriteLine("222选择棋子 以 及 展示可以行走的棋子的possible movement（变色）");
            //选择棋子 以 及 展示可以行走的棋子的possible movement（变色）
            a.PossibleMovementPoint(dataTable);  // datatable 为 table 类型的初始棋盘

            //棋子走动的位置改变
            Console.WriteLine("333棋子走动的位置改变");
            a.PositionChangingDisplay(dataTable);
            //你好，红先黑后，请红方选择棋子。
            //执行play(red),接收坐标。
            //play由dataTable判断，
            //如果坐标指向本方棋子，则继续运行，否则报错。
            //一切正常，则运行Controller.chooseP（）方法
            //调用View的possiblemove方法
            //运行Controller-Move方法
            //刷新View的刷新方法
        }
    }
}
