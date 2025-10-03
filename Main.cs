using Reversi.Game;

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

            Controls.Add(new Game.Board(ClientSize, 500, 6));
        }

        public static void Main()
        {
            Application.Run(new Program());
        }
    }

}
