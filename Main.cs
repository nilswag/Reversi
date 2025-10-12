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

            string str = "";
            foreach (((int r, int c), var flips) in game.GetMoves(Piece.PLAYER1))
            {
                str = $"[{r}, {c}] = [";
                foreach ((int rf, int cf) in flips)
                    str += $"({rf}, {cf}), ";
                str = str.Remove(str.Length - 2);
                str += "]\n";
            }
            Console.WriteLine(str);

            Controls.Add(board);
        }

        public static void Main()
        {
            Application.Run(new Program());
        }
    }

}
