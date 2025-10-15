using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using ComboBox = System.Windows.Forms.ComboBox;

namespace Reversi.Pages
{
    public class SettingsPage : UserControl
    {
        public SettingsPage(Action<string> navigate)
        {
            int startColumn = 214;
            int rowHeight = 66;
            int buttonWidth = 374;

            var title = new Label
            {
                Text = "Instellingen",
                Font = new Font("Open Sans", 64, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false,
                Width = 800,
                Height = 100,
                UseCompatibleTextRendering = true,
                Location = new Point(0, 90)
            };

            var gridSizeLabel = new Label
            {
                Text = "Bord Afmeting",
                Location = new Point(214, 301),
                Width = startColumn,
                Height = rowHeight,
                TextAlign = ContentAlignment.MiddleLeft,
            };

            var selectGridSizeComboBox = new ComboBox
            {
                Location = new Point(479, 303),
                DropDownStyle = ComboBoxStyle.DropDownList,
                DrawMode = DrawMode.OwnerDrawFixed,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(26, 26, 26),
                Width = 107,
                Height = rowHeight,
            };

            // Add items
            selectGridSizeComboBox.Items.Add("4x4");
            selectGridSizeComboBox.Items.Add("6x6");
            selectGridSizeComboBox.Items.Add("8x8");
            selectGridSizeComboBox.Items.Add("10x10");
            selectGridSizeComboBox.Items.Add("12x12");

            // Handle drawing
            selectGridSizeComboBox.DrawItem += (sender, e) =>
            {
                if (e.Index < 0) return;

                // Draw background
                e.Graphics.FillRectangle(
                    new SolidBrush(selectGridSizeComboBox.BackColor), e.Bounds);

                // Draw text
                e.Graphics.DrawString(
                    selectGridSizeComboBox.Items[e.Index].ToString(),
                    e.Font,
                    new SolidBrush(selectGridSizeComboBox.ForeColor),
                    e.Bounds,
                    StringFormat.GenericDefault);

                // Draw focus rectangle if needed
                e.DrawFocusRectangle();
            };


            var player1ColorLabel = new Label
            {
                Text = "Kleur Speler 1",
                Location = new Point(214, 391),
                Width = startColumn,
                Height = rowHeight,
                TextAlign = ContentAlignment.MiddleLeft,
            };

            var player1ColorButton = new RoundButton
            {
                BackColor = Color.FromArgb(255, 0, 0),
                Width = 107,
                Height = rowHeight,
                Location = new Point(479, 391)
            };

            var player2ColorLabel = new Label
            {
                Text = "Kleur Speler 2",
                Location = new Point(214, 479),
                Width = startColumn,
                Height = rowHeight,
                TextAlign = ContentAlignment.MiddleLeft,
            };

            var player2ColorButton = new RoundButton
            {
                BackColor = Color.FromArgb(0, 255, 0),
                Width = 107,
                Height = rowHeight,
                Location = new Point(479, 479)
            };

            var homeButton = new RoundButton
            {
                Text = "Terug naar huis",
                Width = buttonWidth,
                Height = rowHeight,
                Location = new Point(startColumn, 567)
            };
            homeButton.Click += (s, e) => navigate("home");


            Controls.Add(homeButton);
            Controls.Add(gridSizeLabel);
            Controls.Add(selectGridSizeComboBox);
            Controls.Add(player1ColorLabel);
            Controls.Add(player1ColorButton);
            Controls.Add(player2ColorLabel);
            Controls.Add(player2ColorButton);
            Controls.Add(title);

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