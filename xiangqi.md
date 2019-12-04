board 2d-array [9]\[10] (type : string(name of pieces))

how to print a board : use 2 for loop to print the board every time.

0 0 0 0 0 0 0 0 0 0;

1 2 3 4 5 6 7 8 9 0;

0 0 0 0 0 0 0 0 0 0;

......

how to choose a move : enter the location of any piece.

how to show the possible moves. -> write a method. 

pieces abstract pieces class -> inherit-> specific pieces class (horse, pawn, rook,  leader, cannon, king, guard, elephant)

abstract pieces class : 

property of pieces: 

location int x; int y;

color string "red"; "black";

name (horse .....)

moving_rules method(abstract)

died int 1/0;

specific class:

need to rewrite the method (moving rules method for different pieces) (need : board[9]\[10], location)

possible movement method (rules, location) -> output: possible locations



main method:

initialize the board(); // initialize method;

loop { player A , player B, Check if win, ...}

play method: 

1. select the piece(location, board[]\[]) using the possible movement method belongs to the moving rules method;
2. choose the determination (location(d), ) -> decide eat or not & change the location of piece.
3. eat(or not);
4. reload the board;





Main 

method : 