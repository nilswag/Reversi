using System.CodeDom;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Reversi.Game
{
    /// <summary>
    /// Enum containing all possible board position states.
    /// </summary>
    public enum Piece
    { 
        EMPTY,
        PLAYER1,
        PLAYER2,
        VALIDMOVE
    }

    /// <summary>
    /// Board class containing UI logic.
    /// </summary>
    public class Board : Panel
    {
        /// <summary>
        /// Amount of pixels that the board is wide and high.
        /// </summary>
        public int BoardSize { get; private set; }

        /// <summary>
        /// The amount of cells the board is wide and high.
        /// </summary>
        public int NCells { get; private set; }

        /// <summary>
        /// The array that holds the state of the board.
        /// </summary>
        public Piece[,] Grid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether valid moves should be visually rendered.
        /// </summary>
        public bool RenderValidMoves { get; set; }

        private readonly Game _game;
        private readonly Brush[] _brushes;


        /// <summary>
        /// Constructor for board class.
        /// </summary>
        /// <param name="windowSize">The size of the main form window.</param>
        /// <param name="boardSize">The size of the board in pixels.</param>
        /// <param name="nCells">The amount of cells for the board.</param>
        /// <param name="game">A reference to the Game class.</param>
        public Board(Size windowSize, int boardSize, int nCells, Game game)
        {
            BoardSize = boardSize;
            NCells = nCells;
            Grid = new Piece[nCells, nCells];
            RenderValidMoves = false;
            _game = game;

            int[] color1 = Program.CONFIG.GetArray<int>("Player1Color");
            int[] color2 = Program.CONFIG.GetArray<int>("Player2Color");
            int[] color3 = Program.CONFIG.GetArray<int>("ValidMoveColor");
            _brushes = new SolidBrush[]
            { 
                new SolidBrush(Color.FromArgb(color1[0], color1[1], color1[2])),
                new SolidBrush(Color.FromArgb(color2[0], color2[1], color2[2])),
                new SolidBrush(Color.FromArgb(color3[0], color3[1], color3[2])),
            };

            Size = new Size(BoardSize + 1, BoardSize + 1);
            Location = new Point(
                windowSize.Width / 2 - BoardSize / 2,
                windowSize.Height / 2 - BoardSize / 2
            );

            Paint += OnPaint;
            MouseClick += OnMouseClick;
        }

        /// <summary>
        /// Event handler for if the board is clicked.
        /// </summary>
        private void OnMouseClick(object? sender, MouseEventArgs e)
        {
            _game.OnMove(new(e.X / (BoardSize / NCells), e.Y / (BoardSize / NCells)));
        }

        /// <summary>
        /// Event handler for when the board is marked as dirty.
        /// </summary>
        private void OnPaint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

             Piece[,] gridBuffer = (Piece[,])Grid.Clone();
            if (RenderValidMoves)
                // This only gets called every turn (when the screen gets invalidated, which is why this is not inherintly bad).
                foreach ((GridPos pos, List<GridPos> _) in _game.ValidMoves)
                    gridBuffer[pos.R, pos.C] = Piece.VALIDMOVE;

            int s = BoardSize / NCells;
            for (int x = 0; x < NCells; x++)
            {
                for (int y = 0; y < NCells; y++)
                {
                    int xPos = x * s;
                    int yPos = y * s;
                    g.DrawRectangle(Pens.Black, xPos, yPos, s, s);

                    switch (gridBuffer[x, y])
                    {
                        case Piece.PLAYER1:
                            g.FillEllipse(_brushes[0], xPos, yPos, s, s);
                            break;
                        case Piece.PLAYER2:
                            g.FillEllipse(_brushes[1], xPos, yPos, s, s);
                            break;
                        case Piece.VALIDMOVE:
                            g.FillEllipse(_brushes[2], xPos + s / 4, yPos + s / 4, s / 2, s / 2);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

    }

}
