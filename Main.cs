using System;

namespace Reversi
{

    public class MainForm : Form
    {

        public MainForm()
        {
            this.ClientSize = new Size(800, 800);

            this.Controls.Add(new Board());
        }

    }

    internal static class Program
    {
        public static void Main(string[] args)
        {
            Application.Run(new MainForm());
        }
    }

}
