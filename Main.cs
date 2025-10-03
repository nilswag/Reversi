using System;

namespace Reversi
{

    public class Game : Form
    {
        
        public Game()
        {
            ClientSize = new Size(800, 800);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

            Controls.Add(new Board(ClientSize, 500, 6));
        }

    }

    internal static class Program
    {
        public static void Main(string[] args)
        {
            Application.Run(new Game());
        }
    }

}
