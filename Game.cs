using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    public class Game
    {
        private readonly Form _mainForm;
        private readonly Board _board;

        public Game(Form mainForm, Board board)
        {
            _mainForm = mainForm;
            _board = board;

            Piece[,] test =
            {
                { Piece.EMPTY, Piece.EMPTY, Piece.EMPTY, Piece.EMPTY },
                { Piece.EMPTY, Piece.PLAYER1, Piece.PLAYER2, Piece.EMPTY },
                { Piece.EMPTY, Piece.PLAYER2, Piece.PLAYER1, Piece.EMPTY },
                { Piece.EMPTY, Piece.EMPTY, Piece.EMPTY, Piece.EMPTY },
            };

            _board.Grid = test;
            _mainForm.Refresh();
        }

        public bool IsValidMove(Piece player, Piece opponent, int r, int c, List<(int, int)> flips)
        {
            // If the current spot is not empty, just return since there is no need to check if its valid.
            if (_board.Grid[r, c] != Piece.EMPTY) return false;

            // Specify dr (=delta rows) and dc (=delta columns) in an array.
            (int, int)[] directions =
            {
                (-1, -1),   (0, -1),    (1, -1),
                (-1,  0),               (1,  0),
                (-1,  1),   (0,  1),    (1,  1)
            };

            List<(int, int)> tempFlips = []; 
            // Loop through each dr and dc
            foreach((int dr, int dc) in directions)
            {
                // nr = new row, nc = new column, apply the delta to the new values.
                int nr = r + dr, nc = c + dc;
                // Check for out of bounds and if the new spot is not empty (then the diagonal would not be outflanked).
                while (nr >= 0 && nc >= 0 && nr < _board.NCells && nc < _board.NCells && _board.Grid[nr, nc] != Piece.EMPTY)
                {
                    // If there are any opponent pieces in the way add them to-be-flipped list.
                    if (_board.Grid[nr, nc] == opponent) tempFlips.Add((nr, nc));
                    // If there is at least one opponent piece in the to-be-flipped list and a player piece is found (thus the opponent pieces are flanked),
                    // then the move is valid.
                    if (_board.Grid[nr, nc] == player && tempFlips.Count > 0)
                    {
                        flips.AddRange(tempFlips);
                        return true;
                    }
                    nr += dr;
                    nc += dc;
                }
            }

            return false;
        }

        public List<((int, int), List<(int, int)>)> GetMoves(Piece player)
        {
            // Basic logic for finding the opponent.
            Piece opponent = player != Piece.PLAYER1 ? Piece.PLAYER1 : Piece.PLAYER2; 
            List<((int, int), List<(int, int)>)> moves = [];

            // Loop through each position in the board.
            for (int r = 0; r < _board.NCells; r++)
            {
                for (int c = 0; c < _board.NCells; c++)
                {
                    List<(int, int)> flips = [];
                    // If the move would be valid add them to the possible moves.
                    if (IsValidMove(player, opponent, r, c, flips))
                        moves.Add(((r, c), flips));
                }
            }

            return moves;
        }

    }
}
