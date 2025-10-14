using System.Windows.Forms;

namespace Reversi
{
    public class SettingsPage : UserControl
    {
        public SettingsPage(Action<string> navigate)
        {
            var title = new Label
            {
                Text = "Settings",
                Font = new Font("Open Sans", 64, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false,
                Width = 800,
                Height = 100,
                UseCompatibleTextRendering = true,
                Location = new Point(0, 90)
            };

            var homeButton = new RoundButton
            {
                Text = "Home",
                Height = 50
            };
            homeButton.Click += (s, e) => navigate("home");



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