using Reversi.Util;
using System.Resources;
using System.Text.Json;

namespace Reversi
{
    /// <summary>
    /// Class holding the Reversi game.
    /// </summary>
    public class Program : Form
    {
        private UserControl? currentPage;
     
        /// <summary>
        /// Root element of the config.
        /// </summary>
        public static JsonElement CONFIG = new JSONParser(Resources.Config).Root;

        /// <summary>
        /// Root element of the game history.
        /// </summary>
        public static JsonElement GAME_HISTORY = new JSONParser(Resources.GameHistory).Root;

        /// <summary>
        /// Constructor for the program class.
        /// </summary>
        public Program()
        {
            ClientSize = new Size(800, 800);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Text = "Reversi";
            BackColor = Color.FromArgb(0, 0, 0);
            ForeColor = Color.FromArgb(255, 255, 255);


            NavigateTo("home");

        }

        private void NavigateTo(string page)
        {
            if (currentPage != null)
            {
                Controls.Remove(currentPage);
                currentPage.Dispose();
            }

            currentPage = page switch
            {
                "home" => new HomePage(NavigateTo),
                "new-game" => new GamePage(NavigateTo),
                "settings" => new SettingsPage(NavigateTo),
                _ => new HomePage(NavigateTo)
            };

            currentPage.Dock = DockStyle.Fill;

            Controls.Add(currentPage);
        }

        public static void Main()
        {
            Application.Run(new Program());
        }
    }

}
