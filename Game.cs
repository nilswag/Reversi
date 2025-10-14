using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{

    /// <summary>
    /// Struct for a grid position inside Reversi game grid.
    /// </summary>
    /// <param name="r">Row index of grid</param>
    /// <param name="c">Column index of grid</param>
    public struct GridPos(int r, int c)
    {
        /// <summary>
        /// Row index of grid position.
        /// </summary>
        public int R { get; set; } = r;

        /// <summary>
        /// Column index of grid position.
        /// </summary>
        public int C { get; set; } = c;

        public override bool Equals(object obj)
        {
            if (obj is GridPos other)
            {
                return R == other.R && C == other.C;
            }
            return false;
        }

        /// <summary>
        /// Overloads == operator to check if two grid positions are equal.
        /// </summary>
        /// <param name="left">Left grid position of ==.</param>
        /// <param name="right">Right grid position of ==.</param>
        /// <returns>true if equal; false if not.</returns>
        public static bool operator ==(GridPos left, GridPos right) { return left.Equals(right); }

        /// <summary>
        /// Overloads != operator to check if two grid positions are not equal.
        /// </summary>
        /// <param name="left">Left grid position of !=.</param>
        /// <param name="right">Right grid position of !=.</param>
        /// <returns>true if not equal; false if equal.</returns>
        public static bool operator !=(GridPos left, GridPos right) { return !left.Equals(right); }

    }

    /// <summary>
    /// Class containing game logic for Reversi.
    /// </summary>
    public class Game
    {
        private readonly Board _board;
        private Piece _turn;

        /// <summary>
        /// List holding all possible valid moves for the current board position.
        /// </summary>
        public List<(GridPos, List<GridPos>)> ValidMoves { get; private set; }

        /// <summary>
        /// Constructor for Game class.
        /// </summary>
        /// <param name="mainForm">Main form object of the application.</param>
        public Game(Form mainForm)
        {
            _board = new Board(
                mainForm.ClientSize, 
                Program.CONFIG.GetProperty("BoardSizePx").GetInt32(),
                Program.CONFIG.GetProperty("BoardSizes")[1].GetInt32(),
                this
            );
            mainForm.Controls.Add(_board);

            _board.Grid[_board.NCells / 2 - 1, _board.NCells / 2 - 1] = _board.Grid[_board.NCells / 2, _board.NCells / 2]     = Piece.PLAYER1;
            _board.Grid[_board.NCells / 2 - 1, _board.NCells / 2 ]    = _board.Grid[_board.NCells / 2, _board.NCells / 2 - 1] = Piece.PLAYER2;            
            _turn = Piece.PLAYER1;

            ValidMoves = GetMoves(_turn);
        }

        /// <summary>
        /// Event handler callback for if the board gets clicked.
        /// </summary>
        /// <param name="movePos">Grid position of the clicked field.</param>
        public void OnMove(GridPos movePos)
        {
            (GridPos, List<GridPos>) move = ValidMoves.FirstOrDefault(i => i.Item1 == movePos);
            if (move == default) return; // Since nothing was clicked return.

            _board.Grid[movePos.R, movePos.C] = _turn;
            foreach (GridPos pos in move.Item2)
                _board.Grid[pos.R, pos.C] = _turn;

            Piece newTurn = _turn == Piece.PLAYER1 ? Piece.PLAYER2 : Piece.PLAYER1;
            ValidMoves = GetMoves(newTurn);

            if (ValidMoves.Count < 1) // No valid moves
            {
                ValidMoves = GetMoves(_turn);
                if (ValidMoves.Count < 1)
                {
                    int p1Score = 0, p2Score = 0;
                    foreach (Piece piece in _board.Grid)
                    {
                        if (piece == Piece.PLAYER1) p1Score++;
                        else if (piece == Piece.PLAYER2) p2Score++;
                    }
                    string str = "";
                    if (p1Score == p2Score) str = "Draw";
                    else if (p1Score > p2Score) str = "P1 Won";
                    else str = "P2 Won";
                    Console.WriteLine(str);
                }
            }
            else _turn = newTurn;

            _board.Invalidate();
        }

        /// <summary>
        /// Checks if a move on a certain grid position is valid or not.
        /// </summary>
        /// <param name="player">The player who makes the move.</param>
        /// <param name="opponent">The opponent of the player.</param>
        /// <param name="move">The grid position of the move to be made.</param>
        /// <param name="flips">A reference to the list of flips to be made.</param>
        /// <returns>true if the move is valid; false if not.</returns>
        public bool IsValidMove(Piece player, Piece opponent, GridPos move, List<GridPos> flips)
        {
            // If the move is not empty, just return since there is no need to check if its valid.
            if (_board.Grid[move.R, move.C] != Piece.EMPTY) return false;

            // Specify dr (=delta rows) and dc (=delta columns) in an array.
            (int, int)[] directions = new (int, int)[]
            {
                (-1, -1),   (0, -1),    (1, -1),
                (-1,  0),               (1,  0),
                (-1,  1),   (0,  1),    (1,  1)
            };

            // Boolean to mark move as valid or not.
            bool valid = false;

            // Loop through each dr and dc
            foreach((int dr, int dc) in directions)
            {
                List<GridPos> tempFlips = new List<GridPos>();
             
                // nr = new row, nc = new column, apply the delta to the new values.
                // This also makes sure that the first piece checkes is not the move but one step next to it in the current direction.
                int nr = move.R + dr;
                int nc = move.C + dc;
                
                // If the move is out of bounds go to next direction.
                if (nr < 0 || nc < 0 || nr >= _board.NCells || nc >= _board.NCells)
                    continue;

                // If the very next spot is not the opponent there is nothing to flank we can move to the next direction.
                if (_board.Grid[nr, nc] != opponent)
                    continue;

                // Since the very next spot is the opponent we can add it to the flips list.
                tempFlips.Add(new GridPos(nr, nc));

                // Check for out of bounds and if the new spot is not empty (then the diagonal would not be outflanked).
                while (true)
                {
                    // Move one step in the current direction.
                    nr += dr;
                    nc += dc;

                    if (nr < 0 || nc < 0 || nr >= _board.NCells || nc >= _board.NCells)
                        break;

                    // Stop checking the direction if there is an empty spot.
                    if (_board.Grid[nr, nc] == Piece.EMPTY) break;

                    // If there are any opponent pieces in the way add them to-be-flipped list.
                    if (_board.Grid[nr, nc] == opponent) tempFlips.Add(new GridPos(nr, nc));

                    // If there is at least one opponent piece in the to-be-flipped list and a player piece is found (thus the opponent pieces are flanked),
                    // then the move is valid.
                    if (_board.Grid[nr, nc] == player && tempFlips.Count > 0)
                    {
                        flips.AddRange(tempFlips);
                        valid = true;
                        break;
                    }
                }
            }

            return valid;
        }

        /// <summary>
        /// Returns a list of all possible moves for a specific player in a turn.
        /// </summary>
        /// <param name="player">The player to make a move.</param>
        /// <returns>A list of possible moves with a list of flips for that move.</returns>
        public List<(GridPos, List<GridPos>)> GetMoves(Piece player)
        {
            // Basic logic for finding the opponent.
            Piece opponent = player != Piece.PLAYER1 ? Piece.PLAYER1 : Piece.PLAYER2; 
            List<(GridPos, List<GridPos>)> moves = new List<(GridPos, List<GridPos>)>();

            // Loop through each position in the board.
            for (int r = 0; r < _board.NCells; r++)
            {
                for (int c = 0; c < _board.NCells; c++)
                {
                    List<GridPos> flips = new List<GridPos>();
                    // If the move would be valid add them to the possible moves.
                    if (IsValidMove(player, opponent, new GridPos(r, c), flips))
                        moves.Add((new GridPos(r, c), flips));
                }
            }

            return moves;
        }

    }
}
