using System.Windows.Forms;


namespace Reversi.Pages
{
    public class HomePage : UserControl
    {
        public HomePage(Action<string> navigate)
        {
            int buttonWidth = 374;
            int buttonHeight = 66;
            int buttonColumn = 213;
             
            var title = new Label
            {
                Text = "Reversi",
                Font = new Font("Open Sans", 64, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false,
                Width = 800,
                Height = 100,
                UseCompatibleTextRendering = true,
                Location = new Point(0, 90)
            };

            var newGameButton = new RoundButton
            {
                Text = "Nieuw Spel",
                Width = buttonWidth,
                Height = buttonHeight,
                Location = new Point(buttonColumn, 303)
            };
            newGameButton.Click += (s, e) => navigate("new-game");

            var gameHistoryButton = new RoundButton
            {
                Text = "Spel Geschiedenis",
                Width = buttonWidth,
                Height = buttonHeight,
                Location = new Point(buttonColumn, 391)
            };
            gameHistoryButton.Click += (s, e) => navigate("game-history");

            var settingsButton = new RoundButton
            {
                Text = "Instellingen",
                Width = buttonWidth,
                Height = buttonHeight,
                Location = new Point(buttonColumn, 479)
            };
            settingsButton.Click += (s, e) => navigate("settings");

            Controls.Add(newGameButton);
            Controls.Add(gameHistoryButton);
            Controls.Add(settingsButton);
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