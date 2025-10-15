using Reversi.Util;
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

        /// <summary>
        /// Constructor for the program class.
        /// </summary>
        public Program()
        {
            ClientSize = new Size(800, 800);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

            new Game.Game(this);
        }

        /// <summary>
        /// Main program entry.
        /// </summary>
        public static void Main()
        {
            Application.Run(new Program());
        }
    }

}
