using Reversi.Util;

namespace Reversi
{

    internal class Program : Form
    {
        public Program()
        {
            ClientSize = new Size(800, 800);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

            Board board = new Board(ClientSize, 500, 4);
            Game game = new Game(this, board);
            game.GetMoves();

            Controls.Add(board);
        }

        public static void Main()
        {
            Application.Run(new Program());
        }
    }

}
