using System.Windows.Forms;


namespace Reversi.Pages
{
    public class GamePage : UserControl
    {
        public GamePage(Action<string> navigate)
        {
            int buttonWidth = 187;
            int buttonHeight = 66;
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
            newGameButton.Click += (s, e) =>
            {
                var parent = Parent;
                if (parent != null)
                {
                    navigate("new-game");
                }
            };

            var hintButton = new RoundButton
            {
                Text = "Hint",
                Height = buttonHeight,
                Width = buttonWidth,
                Location = new Point(233, buttonRow)
            };



            Controls.Add(homeButton);
            Controls.Add(newGameButton);
            Controls.Add(hintButton);
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

            Load += (s, e) =>
            {
                var mainForm = FindForm();
                if (mainForm == null)
                {
                    MessageBox.Show("GamePage could not be added to the MainForm");
                    return;
                }

                new Game(this);
            };
        }
    }
}