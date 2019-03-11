using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public enum Diretions       //directions for movement
    {
        Left,
        Right,
        Up,
        Down
    };

    class Settings          // class for setting properties
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Speed { get; set; }
        public static int Score { get; set; }
        public static int Points { get; set; }
        public static bool GameOver { get; set; }
        public static Diretions direction { get; set; }

        public Settings()           //setting default 
        {
            Width = 16;
            Height = 16;
            Speed = 20;
            Score = 0;
            Points = 100;
            GameOver = false;
            direction = Diretions.Down;
        }
            
    }
}
