using Reversi.Game;
using Reversi.Util;


namespace Reversi
{

    internal class Program : Form
    {
        public Program()
        {
            ClientSize = new Size(800, 800);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

            Controls.Add(new Game.Board(ClientSize, 500, 6));

            new Game.Game(ClientSize).Iterate();

            //JSONParser json = new JSONParser("Resources/Config.json");
            //json.Load();
        }

        public static void Main()
        {
            Application.Run(new Program());
        }
    }

}
