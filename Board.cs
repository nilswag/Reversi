using System.CodeDom;
using System.Text.Json;

namespace Reversi
{
    public enum Piece
    { 
        EMPTY,
        PLAYER1,
        PLAYER2,
        VALIDMOVE
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

        private readonly Brush[] _brushes;

        /// <summary>Constructor of the board class.</summary>
        /// <param name="windowSize">The size of the parent control.</param>
        /// <param name="boardSize">The size of the board (in pixels).</param>
        /// <param name="nCells">The amount of cells in the board.</param>
        public Board(Size windowSize, int boardSize, int nCells)
        {
            BoardSize = boardSize;
            NCells = nCells;
            Grid = new Piece[nCells, nCells];

            JsonElement color1 = Program.CONFIG.GetProperty("Player1Color");
            JsonElement color2 = Program.CONFIG.GetProperty("Player2Color");
            JsonElement color3 = Program.CONFIG.GetProperty("ValidMoveColor");
            _brushes =
            [
                new SolidBrush(Color.FromArgb(color1[0].GetInt32(), color1[1].GetInt32(), color1[2].GetInt32())),
                new SolidBrush(Color.FromArgb(color2[0].GetInt32(), color2[1].GetInt32(), color2[2].GetInt32())),
                new SolidBrush(Color.FromArgb(color3[0].GetInt32(), color3[1].GetInt32(), color3[2].GetInt32())),
            ];

            Size = new Size(BoardSize + 1, BoardSize + 1);
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
                    switch (Grid[r, c])
                    {
                        case Piece.VALIDMOVE:
                            g.FillEllipse(_brushes[2], r * s + s / 4, c * s + s / 4, s / 2, s / 2);
                            break;
                        case Piece.PLAYER1:
                            g.FillEllipse(_brushes[0], r * s, c * s, s, s);
                            break;
                        case Piece.PLAYER2:
                            g.FillEllipse(_brushes[1], r * s, c * s, s, s);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

    }

}
