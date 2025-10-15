using System.Windows.Forms;


namespace Reversi
{
    public class GamePage : UserControl
    {
        public GamePage(Action<string> navigate)
        {
            // Define width height of the buttons
            int buttonWidth = 187;
            int buttonHeight = 66;
            // Define the Y axis of the buttons
            int buttonRow = 113;

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
                Height = buttonHeight,
                Width = buttonWidth,
                Location = new Point(434, buttonRow)
            };
            homeButton.Click += (s, e) => navigate("home");

            var newGameButton = new RoundButton 
            { 
                Text = "Nieuw Spel",
                Height = buttonHeight,
                Width = buttonWidth,
                Location = new Point(32, buttonRow)
            };
            newGameButton.Click += (s, e) => navigate("new-game");

            var hintButton = new RoundButton
            {
                Text = "Hint",
                Height = buttonHeight,
                Width = buttonWidth,
                Location = new Point(233, buttonRow)
            };

            // Draw game board
            Game game = new Game(this);

            hintButton.Click += (s, e) =>
            {
                game._board.ShowValidMoves = !game._board.ShowValidMoves;

                switch (game._board.ShowValidMoves)
                {
                    case true:
                        hintButton.Text = "Verwijder Hints";
                        break;
                    case false:
                        hintButton.Text = "Hint";
                        break;
                }
                game._board.Invalidate();
            };


            // Retreive player colors from config file
            int[] color1 = Program.CONFIG.GetArray<int>("Player1Color");
            int[] color2 = Program.CONFIG.GetArray<int>("Player2Color");

            var player1ColorButton = new RoundButton
            {
                BackColor = Color.FromArgb(color1[0], color1[1], color1[2]),
                Width = 107,
                Height = 66,
                Location = new Point(16, 391),
                Text = 2.ToString()
            };

            var player2ColorButton = new RoundButton
            {
                BackColor = Color.FromArgb(color2[0], color2[1], color2[2]),
                Width = 107,
                Height = 66,
                Location = new Point(16, 479),
                Text = 2.ToString()
            };

            game.ScoreUpdated += (p1, p2) =>
            {
                player1ColorButton.Text = p1.ToString();
                player2ColorButton.Text = p2.ToString();
            };

            // Add all elements to the UI
            Controls.Add(homeButton);
            Controls.Add(newGameButton);
            Controls.Add(hintButton);
            Controls.Add(player1ColorButton);
            Controls.Add(player2ColorButton);
            Controls.Add(title);




            // Update all buttons to have the same style except for when a different background color has been chosen
            foreach (Control c in Controls)
            {
                if (c is Button b)
                {
                    if (b.BackColor == SystemColors.Control)
                    {
                        b.BackColor = Color.FromArgb(26, 26, 26);
                    }
                    b.ForeColor = Color.FromArgb(255, 255, 255);
                    b.FlatStyle = FlatStyle.Flat;
                    b.FlatAppearance.BorderSize = 0;
                }
            }
        }
    }
}