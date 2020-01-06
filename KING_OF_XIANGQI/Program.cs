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
               try
                {
                    L1: table.InitColor();
                        Console.WriteLine("It's " + color + "'s round, please choose a piece in coordinate ( ex. 4,9 for Red General ) ");
                        int[] locationNum = view.GetRead(); //store location in int[2].
                        int x = locationNum[0];
                        int y = locationNum[1];
                        var departure = new Tuple<int, int>(x, y);//store x,y together into a tuple.
                        int[,] colorTable = table.GetColor();
                        Piece[,] arrTable = table.GetArr();
                    if (arrTable[x, y].GetColor() == color) //check if the color of chose piece is right
                    {
                        controller.ChooseP(x, y, color);    //controller choose one pice to move.
                        view.PossibleMovementPoint(table);  //display the possible move with different color.
                    }
                    else
                    {
                        Console.WriteLine("It is not a "+ color + " piece. Please choose a "+ color + " piece."); 
                        goto L1;    //chosen piece is wrong, make user to choose a piece again
                    }
                    //view make the color change.
                    
                    Console.WriteLine("Please enter the coordinate that you want to go");                       
                    locationNum = view.GetRead();
                    int x1 = locationNum[0];
                    int y1 = locationNum[1];
                    var destination = new Tuple<int, int>(x1, y1);  //store destination coordintes with Tuple.
                    colorTable = table.GetColor();
                    if (colorTable[x1, y1] == 0) // selected piece is not a vaild move.
                        {
                            Console.WriteLine("Selected place is not a vaild move.Please re-enter the coordinate for a piece."); 
                            goto L1; //display the wrong message and jump back to the selection of the user.
                        }
                    else
                    {
                        if ( table.GetPiece(x1, y1) is General) // selected piece is general. 
                        {
                            controller.MoveP(departure, destination);//make the arr of piece change
                            view.PositionChangingDisplay(table); //draw the board with the changed arr of piece    
                            Console.WriteLine("Game is over!" + color + " wins!");
                            checkwin = true; // make the boolean checkwin to true and end the if loop.
                        }
                        else
                        {
                            controller.MoveP(departure, destination);//make the arr of piece change
                            view.PositionChangingDisplay(table); //draw the board with the changed arr of piece    
                        }
                    }
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("There is no piece in this place");
                    Play(controller, table, view, color);
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("The coordinate is out of index");
                    Play(controller, table, view, color);
                }
            }
            return checkwin;
        }
    }
}
