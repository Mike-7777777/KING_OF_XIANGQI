using System;

namespace KING_OF_XIANGQI
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hello World!111");
            Console.WriteLine("Hello Bart!");
            Console.WriteLine("Hello TYL!");
            Table dataTable = new Table();
            Piece[,] arrTable = dataTable.getArr();
            dataTable.initArr();
            int X = 1;
            int Y = 1;
            string type = arrTable[X, Y].GetType().ToString();
            Console.WriteLine(type);
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
