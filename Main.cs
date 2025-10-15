using Reversi.Util;
using System.Resources;
using System.Text.Json;
using Reversi.Properties;

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
        public static JsonConfig CONFIG = new JsonConfig("Resources/Config.json");

        /// <summary>
        /// Root element of the game history.
        /// </summary>
        public static JsonConfig GAME_HISTORY = new JsonConfig("Resources/GameHistory.json");


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
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.UserPaint |
                         ControlStyles.DoubleBuffer, true);
            UpdateStyles();

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

        [STAThread]
        public static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Set global font
            Application.SetDefaultFont(new Font("Open Sans", 16, FontStyle.Regular));
            Application.Run(new Program());
        }
    }

}
