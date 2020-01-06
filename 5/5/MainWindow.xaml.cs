using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {
        Tuple<int, int> departure;
        Tuple<int, int> destination;
        string red = "Red";
        string black = "Black";
        GameState gameState  = GameState.SelectPieceRed;
        Table table = new Table();
        Grid grid = new Grid();
        Grid grid2 = new Grid();
        Grid grid3 = new Grid();
        Grid grid4 = new Grid();
        TextBlock text1 = new TextBlock();
        TextBlock text2 = new TextBlock();
        TextBlock text3 = new TextBlock();

        public MainWindow()
        {
            InitializeComponent();
            table.InitArr();//init the arr which store the piece of start state.
            
            Create(); //创建按钮
            Draw();   //加载按钮图片
        }

        public Tuple<int,int> PossibleMovement(Button btn, string color) // 重新打印带有possible move的棋盘 print the gameboard with possible move
        {
            table.InitColor();        
            Controller controller = new Controller(table);         
            int col = Grid.GetColumn(btn);
            int row = Grid.GetRow(btn);
            var departure = new Tuple<int, int>(col, row); // button coordinate
            controller.ChooseP(Grid.GetColumn(btn), Grid.GetRow(btn), color);
             
            for (int i = 0; i < 90; i++)//循环每一个按钮 看是不是possible moves
            {
                Button btnSelected = (Button)grid.Children[i];
                if (table.GetPieceColor(Grid.GetColumn(btnSelected), Grid.GetRow(btnSelected)) == 1 && table.GetPiece(Grid.GetColumn(btnSelected), Grid.GetRow(btnSelected)) is null) //possible move 
                {
                    btnSelected.Background = Picture.PossibleMove;//possible move change color
                }
                if (table.GetPieceColor(Grid.GetColumn(btnSelected), Grid.GetRow(btnSelected)) == 1 && table.GetPiece(Grid.GetColumn(btnSelected), Grid.GetRow(btnSelected)) != null && table.GetPiece(Grid.GetColumn(btnSelected), Grid.GetRow(btnSelected)).GetColor() == red)
                {
                    SwitchPossiblemovePieceRed(btnSelected, Grid.GetColumn(btnSelected), Grid.GetRow(btnSelected));
                }
                if (table.GetPieceColor(Grid.GetColumn(btnSelected), Grid.GetRow(btnSelected)) == 1 && table.GetPiece(Grid.GetColumn(btnSelected), Grid.GetRow(btnSelected)) != null && table.GetPiece(Grid.GetColumn(btnSelected), Grid.GetRow(btnSelected)).GetColor() == black)
                {
                    SwitchPossiblemovePieceBlack(btnSelected, Grid.GetColumn(btnSelected), Grid.GetRow(btnSelected));
                }
            }  
            return departure;
        }

        public Tuple<int, int> PositionChange(Button btn) // 重新打印移动过后的棋盘 print moved gameboard
        {
            int col = Grid.GetColumn(btn);
            int row = Grid.GetRow(btn);
            var destination = new Tuple<int, int>(col, row); // button coordinate           

                for (int i = 0; i < 90; i++)
                {
                    Button btnSelected1 = (Button)grid.Children[i];
                    int col1 = Grid.GetColumn(btnSelected1);
                    int row1 = Grid.GetRow(btnSelected1);
                    if (btnSelected1.Background == Picture.PossibleMove && table.GetPiece(col1, row1) == null) // 没移动的possible move变背景(无棋子)
                    {
                    btnSelected1.SetValue(BackgroundProperty, Brushes.Transparent);
                    }
            }
            return destination;
        }

        private void Button_Click(object sender, RoutedEventArgs e)//click events
        {
            Button btn = sender as Button;       
            HandleClick(btn,red,black);   
        }

        public void HandleClick(Button btn,string color1,string color2)//相当于Play函数 处理点击事件以及黑红交互 handle button click
        {
            try
            {
                switch (gameState) // 4 gameState 
                {
                    case GameState.SelectPieceRed://选红棋子 choose red piece
                        if (table.GetPiece(Grid.GetColumn(btn), Grid.GetRow(btn)).GetColor() == color1) //check if it is the red piece
                        {
                            PossibleMovement(btn, color1);
                            departure = PossibleMovement(btn, color1);
                            ChangeState(GameState.SelectMoveRed);
                            //display possible move and change state
                        }
                        else
                        {
                            MessageBox.Show("Not the right piece of color. Please choose a red piece."); // not a right color piece and report error
                            Draw();
                            ChangeState(GameState.SelectPieceRed);
                        }
                        break;

                    case GameState.SelectPieceBlack: // 选黑棋子 choose black piece 
                        if (table.GetPiece(Grid.GetColumn(btn), Grid.GetRow(btn)).GetColor() == color2) //check if it is the black piece
                        {
                            PossibleMovement(btn, color2);                              
                            departure = PossibleMovement(btn, color2);
                            ChangeState(GameState.SelectMoveBlack);
                            //display possible move and change state
                        }
                        else
                        {                             
                            MessageBox.Show("Not the right piece of color. Please choose a black piece.");    // not a right color piece and report error
                            Draw();
                            ChangeState(GameState.SelectPieceBlack);
                        }                        
                        break;
                    
                    case GameState.SelectMoveRed: //移动红棋子 select a red move 
                        destination = PositionChange(btn);
                        if (table.GetPieceColor(Grid.GetColumn(btn), Grid.GetRow(btn)) == 0) // check if the chose move is the possible move red // 我在table加了个GetPieceColor 返回1 or 0就是红或黑 console里也能用
                        {
                            MessageBox.Show("It is not a possible move. Please choose a new red piece.");
                            Draw();
                            ChangeState(GameState.SelectPieceRed);   
                        }
                        else
                        {
                            CheckWinandMove(btn,red);
                            ChangeState(GameState.SelectPieceBlack);
                            //checkwin and display moved board
                        }
                        break;

                    case GameState.SelectMoveBlack: //移动黑棋子 select a black move
                        destination = PositionChange(btn);
                        if (table.GetPieceColor(Grid.GetColumn(btn), Grid.GetRow(btn)) == 0 ) //// check if the chose move is the possible move black
                        {
                            MessageBox.Show("It is not a possible move. Please choose a new black piece.");
                            Draw();
                            ChangeState(GameState.SelectPieceBlack);
                            
                        }
                        else
                        {
                            CheckWinandMove(btn,black);
                            ChangeState(GameState.SelectPieceRed);
                            //checkwin and display moved board
                        }
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("There is no piece in the place! Please choose a piece.");
            }

        }
        public void CheckWinandMove(Button btn, string color) //检验游戏结束 并移动棋子 check if the game is over and move the piece
        {
            Controller controller = new Controller(table);
            if (table.GetPiece(Grid.GetColumn(btn), Grid.GetRow(btn)) is General)//检测游戏结束
            {
                controller.MoveP(departure, destination);
                Draw();
                ChangePrePieceBackground();
                PositionChange(btn);
                MessageBox.Show("Gameover! "+ color +" wins.");
                Close();
            }
            else
            {
                controller.MoveP(departure, destination);
                Draw();
                ChangePrePieceBackground();
                PositionChange(btn);
            }
        }
        public void ChangePrePieceBackground() //change the background of the moved piece
        {
            for (int i = 0; i < 90; i++)//循环每一个按钮 看是不是possible moves
            {
                Button btnSelected = (Button)grid.Children[i];
                if (Grid.GetColumn(btnSelected) == departure.Item1 && Grid.GetRow(btnSelected) == departure.Item2) //possible move 
                {
                    btnSelected.SetValue(BackgroundProperty, Brushes.Transparent);//原先棋子变背景
                }
            }
        }
        public void SwitchPieceRed(Button btn,int col1, int row1)//one to one coorespondence, the piece and the picture
        {
            switch (table.GetPiece(col1, row1).GetType().ToString())
            {
                case "_5.Horse":
                    btn.Background = Picture.Horse_Red;
                    break;
                case "_5.General":
                    btn.Background = Picture.General_Red;
                    break;
                case "_5.Rook":
                    btn.Background = Picture.Rook_Red;
                    break;
                case "_5.Elephant":
                    btn.Background = Picture.Elephant_Red;
                    break;
                case "_5.Mandarin":
                    btn.Background = Picture.Mandarin_Red;
                    break;
                case "_5.Pawn":
                    btn.Background = Picture.Pawn_Red;
                    break;
                case "_5.Cannon":
                    btn.Background = Picture.Cannon_Red;
                    break;
            }
        }
        public void SwitchPieceBlack(Button btn,int col1,int row1)//one to one coorespondence, the piece and the picture
        {
            switch (table.GetPiece(col1, row1).GetType().ToString())
            {
                case "_5.Horse":
                    btn.Background = Picture.Horse_Black;
                    break;
                case "_5.General":
                    btn.Background = Picture.General_Black;
                    break;
                case "_5.Rook":
                    btn.Background = Picture.Rook_Black;
                    break;
                case "_5.Elephant":
                    btn.Background = Picture.Elephant_Black;
                    break;
                case "_5.Mandarin":
                    btn.Background = Picture.Mandarin_Black;
                    break;
                case "_5.Pawn":
                    btn.Background = Picture.Pawn_Black;
                    break;
                case "_5.Cannon":
                    btn.Background = Picture.Cannon_Black;
                    break;
            }
        }
        public void SwitchPossiblemovePieceRed(Button btn,int col1, int row1)//if it is the possible with eat piece, the picture will change
        {
            switch(table.GetPiece(col1, row1).GetType().ToString())
            {
                case "_5.Horse":
                    btn.Background = Picture.Horse1;
                    break;
                case "_5.General":
                    btn.Background = Picture.General_Red1;
                    break;
                case "_5.Rook":
                    btn.Background = Picture.Rook1;
                    break;
                case "_5.Elephant":
                    btn.Background = Picture.Elephant_Red1;
                    break;
                case "_5.Mandarin":
                    btn.Background = Picture.Mandarin_Red1;
                    break;
                case "_5.Pawn":
                    btn.Background = Picture.Pawn_Red1;
                    break;
                case "_5.Cannon":
                    btn.Background = Picture.Cannon1;
                    break;
            }
        }
        public void SwitchPossiblemovePieceBlack(Button btn, int col1, int row1)//if it is the possible with eat piece, the picture will change
        {
            switch (table.GetPiece(col1, row1).GetType().ToString())
            {
                case "_5.Horse":
                    btn.Background = Picture.Horse1;
                    break;
                case "_5.General":
                    btn.Background = Picture.General_Black1;
                    break;
                case "_5.Rook":
                    btn.Background = Picture.Rook1;
                    break;
                case "_5.Elephant":
                    btn.Background = Picture.Elephant_Black1;
                    break;
                case "_5.Mandarin":
                    btn.Background = Picture.Mandarin_Black1;
                    break;
                case "_5.Pawn":
                    btn.Background = Picture.Pawn_Black1;
                    break;
                case "_5.Cannon":
                    btn.Background = Picture.Cannon1;
                    break;
            }
        } 
        public void Draw()//画棋盘 图片 draw the gameboard with the picture
        {
            int i = 0;
            Piece[,] arrTable = table.GetArr();
            foreach (Piece piece in arrTable)
            {
                Button btn = (Button)grid.Children[i];
                int col1 = Grid.GetColumn(btn);
                int row1 = Grid.GetRow(btn);
                if (arrTable[col1,row1] != null && arrTable[col1, row1].GetColor() == black) //black piece picture
                {
                    SwitchPieceBlack(btn, col1, row1);
                }
                else if(arrTable[col1, row1] != null && arrTable[col1, row1].GetColor() == red) //red piece picture
                {
                    SwitchPieceRed(btn, col1, row1);
                }
                i++;
            }
        }
        private void Create()//创建grid 以及button    create grid, button and textblock
        {
            this.Content = grid;
            grid.Background = new ImageBrush 
            {
                ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/table4.png?raw=true"))
            };//whole grid background, the picture of the gameboard without piece
            grid.ShowGridLines = false;
            for (int i = 0; i < 11; i++)
            {
                RowDefinition row = new RowDefinition();
                grid.RowDefinitions.Add(row);
                
            }
            for (int i = 0; i < 9; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                grid.ColumnDefinitions.Add(col);
                
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Button b = new Button();
                    b.Click += new RoutedEventHandler(this.Button_Click); //b.Click += new RoutedEventHandler(this.Button_Click);
                    Grid.SetRow(b, i);
                    Grid.SetColumn(b, j);
                    b.Background = Brushes.Transparent;              
                    grid.ShowGridLines = false;
                    grid.Children.Add(b);
                    b.SetValue(BorderThicknessProperty,BorderThickness);  
                }
            }
            //to create 90 buttons

            Grid.SetRow(grid2, 10);
            Grid.SetColumn(grid2, 3);
            text1.Text = "Red time"; 
            text1.FontSize = 20;
            text1.Foreground = Brushes.Red;
            grid2.Children.Add(text1);
            grid.Children.Add(grid2);
            
            Grid.SetRow(grid3, 10);              
            Grid.SetColumn(grid3, 4);
            text2.Text = ", Select a";
            text2.FontSize = 20;
            text2.Foreground = Brushes.Red;
            grid3.Children.Add(text2);
            grid.Children.Add(grid3);

            Grid.SetRow(grid4, 10);
            Grid.SetColumn(grid4, 5);
            text3.Text = "piece";
            text3.FontSize = 20;
            text3.Foreground = Brushes.Red;
            grid4.Children.Add(text3);
            grid.Children.Add(grid4);      
            //three textblock to display which gameState

        }
        public enum GameState //create four gamestate.  it's a enum class
        {
            SelectPieceRed = 1,
            SelectMoveRed,
            SelectPieceBlack,
            SelectMoveBlack,
        }
        public void ChangeState(GameState newState) // change state and change the words of state which display on the board
        {
            gameState = newState;
            switch (newState)
            {
                case GameState.SelectPieceRed:
                    text1.Text = "Red time"; text2.Text = ", Select a"; text3.Text = "piece";
                    break;
                case GameState.SelectMoveRed:
                    text1.Text = "Red time"; text2.Text = ", Select a"; text3.Text = "move";
                    break;
                case GameState.SelectPieceBlack:
                    text1.Text = "Black tim"; text2.Text = "e,  Select"; text3.Text = " a piece";
                    break;
                case GameState.SelectMoveBlack:
                    text1.Text = "Black tim"; text2.Text = "e,  Select"; text3.Text = " a move";
                    break;
            }
        }
    }
}
