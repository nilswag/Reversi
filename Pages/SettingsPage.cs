using Reversi.Pages;
using Reversi.Util;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using ComboBox = System.Windows.Forms.ComboBox;

namespace Reversi
{
    public class SettingsPage : UserControl
    {
        public SettingsPage(Action<string> navigate)
        {
            int columnWidth = 214;
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
                Width = columnWidth,
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
                Text = "6"
            };

            // Add boardsize items from config file to dropdown
            foreach (var item in Program.CONFIG.Root["BoardSizes"]!.AsArray())
            {
                selectGridSizeComboBox.Items.Add(item!.ToString());
            }

            // Set default of dropdown to current board size
            selectGridSizeComboBox.SelectedItem = Program.CONFIG.Root["CurrentBoardSize"]!.GetValue<int>().ToString(); // MUST exist in Items

            // Draw black background for dropdown menu
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
            };

            selectGridSizeComboBox.SelectedIndexChanged += (sender, e) =>
            {
                Program.CONFIG.Root["CurrentBoardSize"] = Int32.Parse((string)selectGridSizeComboBox.SelectedItem);
                Program.CONFIG.Save();
            };

            // Retreive player colors from config file
            int[] color1 = Program.CONFIG.Deserialize<int[]>("Player1Color")!;
            int[] color2 = Program.CONFIG.Deserialize<int[]>("Player2Color")!;


            var player1ColorLabel = new Label
            {
                Text = "Kleur Speler 1",
                Location = new Point(214, 391),
                Width = columnWidth,
                Height = rowHeight,
                TextAlign = ContentAlignment.MiddleLeft,
            };

            var player1ColorButton = new RoundButton
            {
                BackColor = Color.FromArgb(color1[0], color1[1], color1[2]),
                Width = 107,
                Height = rowHeight,
                Location = new Point(479, 391)
            };

            var player2ColorLabel = new Label
            {
                Text = "Kleur Speler 2",
                Location = new Point(214, 479),
                Width = columnWidth,
                Height = rowHeight,
                TextAlign = ContentAlignment.MiddleLeft,
            };

            var player2ColorButton = new RoundButton
            {
                BackColor = Color.FromArgb(color2[0], color2[1], color2[2]),
                Width = 107,
                Height = rowHeight,
                Location = new Point(479, 479)
            };

            var homeButton = new RoundButton
            {
                Text = "Terug naar huis",
                Width = buttonWidth,
                Height = rowHeight,
                Location = new Point(214, 567)
            };
            homeButton.Click += (s, e) => navigate("home");

            // Handle color picking for player 1
            // --> Open dialog
            // --> Check if a color was chosen
            // --> Update color picking button
            // --> Store color in config
            player1ColorButton.Click += (s, e) =>
            {
                using (var playerColorDialog = new ColorDialog())
                {
                    if (playerColorDialog.ShowDialog() == DialogResult.OK)
                    {
                        player1ColorButton.BackColor = playerColorDialog.Color;
                        int[] color = new int[] { playerColorDialog.Color.R, playerColorDialog.Color.G, playerColorDialog.Color.B };
                        Program.CONFIG.Root["Player1Color"] = JsonConfig.Serialize(color);
                        Program.CONFIG.Save();
                    }
                }
            };

            // Handle color picking for player 2
            // --> Open dialog
            // --> Check if a color was chosen
            // --> Update color picking button
            // --> Store color in config
            player2ColorButton.Click += (s, e) =>
            {
                using (var playerColorDialog = new ColorDialog())
                {
                    if (playerColorDialog.ShowDialog() == DialogResult.OK)
                    {
                        player2ColorButton.BackColor = playerColorDialog.Color;
                        player1ColorButton.BackColor = playerColorDialog.Color;
                        int[] color = new int[] { playerColorDialog.Color.R, playerColorDialog.Color.G, playerColorDialog.Color.B };
                        Program.CONFIG.Root["Player2Color"] = JsonConfig.Serialize(color);
                        Program.CONFIG.Save();
                    }
                }
            };

            // Add all elements to the UI
            Controls.Add(homeButton);
            Controls.Add(gridSizeLabel);
            Controls.Add(selectGridSizeComboBox);
            Controls.Add(player1ColorLabel);
            Controls.Add(player1ColorButton);
            Controls.Add(player2ColorLabel);
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