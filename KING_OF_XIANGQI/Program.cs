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
            Controller control = new Controller(dataTable);
            view.InitialBoardForDisplay();
            dataTable.InitColor();
            dataTable.InitArr();
            while (control.SelectandMove(dataTable, view, red) == false && control.SelectandMove(dataTable, view, black) == false)
            {
                dataTable.InitColor();
                control.SelectandMove(dataTable, view, red);
                control.SelectandMove(dataTable, view, black);
            }

        }

    }
}