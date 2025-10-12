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

        public bool IsValidMove(Piece player, int r, int c)
        {
            // If the current spot is not empty, just return since there is no need to check if its valid.
            if (_board.Grid[r, c] != Piece.EMPTY) return false;

            // Specify dr (=delta rows) and dc (=delta columns) in an array.
            (int, int)[] directions = new (int, int)[]
            {
                (-1, -1),   (0, -1),    (1, -1),
                (-1,  0),               (1,  0),
                (-1,  1),   (0,  1),    (1,  1)
            };

            // Loop through each dr and dc
            foreach((int dr, int dc) in directions)
            {
                // nr = new row, nc = new column, apply the delta to the new values.
                int nr = r + dr, nc = c + dc;
                // Check for out of bounds and if the new spot is not empty (then the diagonal would not be outflanked).
                while (nr > 0 && nc > 0 && nr < _board.NCells && nc < _board.NCells && _board.Grid[nr, nc] != Piece.EMPTY)
                {
                    
                }
            }

            return true;
        }

        public void GetMoves(Piece player = Piece.PLAYER1)
        { }

    }
}
