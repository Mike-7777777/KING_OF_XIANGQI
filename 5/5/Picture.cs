using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace _5
{
    public class Picture // 存棋子图片的类 // a class to store the picture that we needed in this program.
    {
        public static ImageBrush PossibleMove = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/eatable/pieces-eat12.png?raw=true"))
        };
        public static ImageBrush General_Black = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/pieces-black-jiang.jpg?raw=true"))
        };
        public static ImageBrush Rook_Black = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/pieces-black-che.jpg?raw=true"))
        };
        public static ImageBrush Horse_Black = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/pieces-black-ma.jpg?raw=true"))
        };
        public static ImageBrush Elephant_Black = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/pieces-black-xiang.jpg?raw=true"))
        };
        public static ImageBrush Mandarin_Black = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/pieces-black-shi.jpg?raw=true"))
        };
        public static ImageBrush Pawn_Black = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/pieces-black-zu.jpg?raw=true"))
        };
        public static ImageBrush Cannon_Black = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/pieces-black-pao.jpg?raw=true"))
        };
        public static ImageBrush General_Red = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/pieces-red-shuai.jpg?raw=true"))
        };
        public static ImageBrush Rook_Red = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/pieces-red-che.jpg?raw=true"))
        };
        public static ImageBrush Horse_Red = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/pieces-red-ma.jpg?raw=true"))
        };
        public static ImageBrush Elephant_Red = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/pieces-red-xiang.jpg?raw=true"))
        };
        public static ImageBrush Mandarin_Red = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/pieces-red-shi.jpg?raw=true"))
        };
        public static ImageBrush Pawn_Red = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/pieces-red-bin.jpg?raw=true"))
        };
        public static ImageBrush Cannon_Red = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/pieces-red-pao.jpg?raw=true"))
        };
        public static ImageBrush General_Black1 = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/eatable/pieces-black-jiang-eat.png?raw=true"))
        };
        public static ImageBrush Rook1 = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/eatable/pieces-red-che.png?raw=true"))
        };
        public static ImageBrush Horse1 = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/eatable/pieces-red-ma.png?raw=true"))
        };
        public static ImageBrush Elephant_Black1 = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/eatable/pieces-black-xiang.png?raw=true"))
        };
        public static ImageBrush Mandarin_Black1 = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/eatable/pieces-black-shi.png?raw=true"))
        };
        public static ImageBrush Pawn_Black1 = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/eatable/pieces-black-zu.png?raw=true"))
        };
        public static ImageBrush Cannon1 = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/eatable/pieces-red-pao.png?raw=true"))
        };
        public static ImageBrush General_Red1 = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/eatable/pieces-red-shuai.png?raw=true"))
        };
        public static ImageBrush Elephant_Red1 = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/eatable/pieces-red-xiang.png?raw=true"))
        };
        public static ImageBrush Mandarin_Red1 = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/eatable/pieces-red-shi.png?raw=true"))
        };
        public static ImageBrush Pawn_Red1 = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("https://github.com/Mike-7777777/KING_OF_XIANGQI/blob/master/KING_OF_XIANGQI/src/eatable/pieces-red-bin.png?raw=true"))
        };

    }
}
