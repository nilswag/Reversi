using Reversi.Properties;
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

            Game game = new(this);
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
