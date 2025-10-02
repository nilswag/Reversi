
using System.CodeDom;

namespace Reversi
{

    public class Board : Panel
    {
        private const int GridSize = 6;
        private int[][] Grid;

        public Board()
        {
            Grid = new int[GridSize][];

            this.Paint += OnPaint;
        }

        private void OnPaint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int row = 0; row < Grid.Length; row++)
            {
                for (int col = 0; col < Grid[col].Length; col++)
                {

                }
            }

        }
    }

}
