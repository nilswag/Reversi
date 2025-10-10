
using System.CodeDom;

namespace Reversi.Game
{
    public enum Piece
    { 
        PLAYER1,
        PLAYER2,
        EMPTY
    }

    /// <summary>Class that contains and controls the main board of the game.</summary>
    public class Board : Panel
    {
        /// <summary>The size of the board (in pixels).</summary>
        public int BoardSize { get; private set; }

        /// <summary>The amount of cells in the board.</summary>
        public int NCells { get; private set; }

        /// <summary>The array that holds the current situation on the board.</summary>
        public Piece[,] Grid { get; set; }

        /// <summary>Constructor of the board class.</summary>
        /// <param name="windowSize">The size of the parent control.</param>
        /// <param name="boardSize">The size of the board (in pixels).</param>
        /// <param name="nCells">The amount of cells in the board.</param>
        public Board(Size windowSize, int boardSize, int nCells)
        {
            BoardSize = boardSize;
            NCells = nCells;
            Grid = new Piece[nCells, nCells];

            Size = new Size(BoardSize, BoardSize);
            Location = new Point(
                windowSize.Width / 2 - BoardSize / 2,
                windowSize.Height / 2 - BoardSize / 2
            );

            Paint += OnPaint;
        }

        /// <summary>Event handler which gets called each time the control is refreshed/redrawn.</summary>
        private void OnPaint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int s = BoardSize / NCells;
            for (int r = 0; r < NCells; r++)
            {
                for (int c = 0; c < NCells; c++)
                {
                    g.DrawRectangle(Pens.Black, c * s, r * s, s, s);

                    if (Grid[r, c] == Piece.EMPTY) continue;
                    Brush color = Grid[r, c] == Piece.PLAYER1 ? Brushes.Red : Brushes.Blue;
                    g.FillEllipse(color, r * s, c * s, s, s);
                }
            }
        }

    }

}
