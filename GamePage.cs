using System.Windows.Forms;


namespace Reversi
{
    public class GamePage : UserControl
    {
        public GamePage(Action<string> navigate)
        {
            var title = new Label
            {
                Text = "Reversi",
                Font = new Font("Open Sans", 64, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false,
                Width = 800,
                Height = 100,
                UseCompatibleTextRendering = true,
                Location = new Point(0, 8)
            };

            var homeButton = new RoundButton
            {
                Text = "Home",
                Height = 50
            };
            homeButton.Click += (s, e) => navigate("home");

            Board board = new Board(ClientSize, 562, 4);
            Game game = new Game(board);

            string str = "";
            foreach (((int r, int c), var flips) in game.GetMoves(Piece.PLAYER1))
            {
                str += $"[{r}, {c}] = [";
                foreach ((int rf, int cf) in flips)
                    str += $"({rf}, {cf}), ";
                str = str.Remove(str.Length - 2);
                str += "]\n";
            }
            Console.WriteLine(str);

            Controls.Add(board);



            Controls.Add(homeButton);
            Controls.Add(title);

            


            foreach (Control c in Controls)
            {
                if (c is Button b)
                {
                    b.BackColor = Color.FromArgb(26, 26, 26);
                    b.ForeColor = Color.FromArgb(255, 255, 255);
                    b.FlatStyle = FlatStyle.Flat;
                    b.FlatAppearance.BorderSize = 0;
                }
            }
        }
    }
}