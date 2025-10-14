using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    public class Game
    {
        private readonly Board _board;
        private Piece _turn;
        public List<((int, int), List<(int, int)>)> ValidMoves { get; private set; }

        public Game(Form mainForm)
        {
            _board = new(
                mainForm.ClientSize, Program.CONFIG.GetProperty("BoardSizePx").GetInt32(),
                Program.CONFIG.GetProperty("BoardSizes")[0].GetInt32(),
                this
            );
            mainForm.Controls.Add(_board);

            _board.Grid[_board.NCells / 2 - 1, _board.NCells / 2 - 1] = _board.Grid[_board.NCells / 2, _board.NCells / 2] = Piece.PLAYER1;
            _board.Grid[_board.NCells / 2 - 1, _board.NCells / 2 ]   = _board.Grid[_board.NCells / 2, _board.NCells / 2 - 1] = Piece.PLAYER2;            
            _turn = Piece.PLAYER1;

            ValidMoves = GetMoves(_turn);
        }

        public void OnMove(int r, int c)
        {
            if (!ValidMoves.Any(i => i.Item1 == (r, c))) return; // Since nothing was clicked return.
            var move = ValidMoves.First(i => i.Item1 == (r, c));

            _board.Grid[r, c] = _turn;
            foreach ((int rp, int cp) in move.Item2)
                _board.Grid[rp, cp] = _turn;

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
                        if (piece == Piece.PLAYER2) p2Score++;
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

        public bool IsValidMove(Piece player, Piece opponent, int r, int c, List<(int, int)> flips)
        {
            // If the move is not empty, just return since there is no need to check if its valid.
            if (_board.Grid[r, c] != Piece.EMPTY) return false;

            // Specify dr (=delta rows) and dc (=delta columns) in an array.
            (int, int)[] directions =
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
                List<(int, int)> tempFlips = [];
             
                // nr = new row, nc = new column, apply the delta to the new values.
                // This also makes sure that the first piece checkes is not the move but one step next to it in the current direction.
                int nr = r + dr;
                int nc = c + dc;
                
                // If the move is out of bounds go to next direction.
                if (nr < 0 || nc < 0 || nr >= _board.NCells || nc >= _board.NCells)
                    continue;

                // If the very next spot is not the opponent there is nothing to flank we can move to the next direction.
                if (_board.Grid[nr, nc] != opponent)
                    continue;

                // Since the very next spot is the opponent we can add it to the flips list.
                tempFlips.Add((nr, nc));

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
                    if (_board.Grid[nr, nc] == opponent) tempFlips.Add((nr, nc));

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
