using Reversi.Util;
using Reversi.Pages;
using System.Text.Json;

namespace Reversi
{

    /// <summary>
    /// Class holding the Reversi game.
    /// </summary>
    public class Program : Form
    {

        /// <summary>
        /// Root element of the config.
        /// </summary>
        public static JsonConfig CONFIG = new JsonConfig("Resources/Config.json");

        /// <summary>
        /// Root element of the game history.
        /// </summary>
        public static JsonConfig GAME_HISTORY = new JsonConfig("Resources/GameHistory.json");

        private UserControl? _currentPage;

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
            if (_currentPage != null)
            {
                Controls.Remove(_currentPage);
                _currentPage.Dispose();
            }

            _currentPage = page switch
            {
                "home" => new HomePage(NavigateTo),
                "new-game" => new GamePage(NavigateTo),
                "settings" => new SettingsPage(NavigateTo),
                _ => new HomePage(NavigateTo)
            };

            _currentPage.Dock = DockStyle.Fill;

            Controls.Add(_currentPage);
        }

        /// <summary>
        /// Main program entry.
        /// </summary>
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
