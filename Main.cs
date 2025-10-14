using Reversi.Properties;
using Reversi.Util;
using System.Text.Json;

namespace Reversi
{

    public class Program : Form
    {
        public static JsonElement CONFIG = new JSONParser(Resources.Config).Root;
        public static JsonElement GAME_HISTORY = new JSONParser(Resources.GameHistory).Root;

        public Program()
        {
            ClientSize = new Size(800, 800);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

            Game game = new(this);
        }

        public static void Main()
        {
            Application.Run(new Program());
        }
    }

}
