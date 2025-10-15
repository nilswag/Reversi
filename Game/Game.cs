using Reversi.Util;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Reversi.Game
{
    /// <summary>
    /// Class containing game logic for Reversi.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Game board.
        /// </summary>
        public readonly Board Board;

        /// <summary>
        /// List holding all possible valid moves for the current board position.
        /// </summary>
        public List<(GridPos, List<GridPos>)> ValidMoves { get; private set; }

        /// <summary>
        /// Event thats triggered when a move is made so that the score board is kept uptodate
        /// </summary>
        public event Action<int, int>? ScoreUpdated;

        private Piece _turn;

        /// <summary>
        /// Constructor for Game class.
        /// </summary>
        /// <param name="mainForm">Main form object of the application.</param>
        public Game(Control parent)
        {
            Board = new Board(
                parent.ClientSize,
                Program.CONFIG.Root["BoardSizePx"]!.GetValue<int>(),
                Program.CONFIG.Root["CurrentBoardSize"]!.GetValue<int>(),
                this
            );
            parent.Controls.Add(Board);

            Board.Grid[Board.NCells / 2 - 1, Board.NCells / 2 - 1] = Board.Grid[Board.NCells / 2, Board.NCells / 2]     = Piece.PLAYER1;
            Board.Grid[Board.NCells / 2 - 1, Board.NCells / 2 ]    = Board.Grid[Board.NCells / 2, Board.NCells / 2 - 1] = Piece.PLAYER2;            
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

            Board.Grid[movePos.R, movePos.C] = _turn;
            foreach (GridPos pos in move.Item2)
                Board.Grid[pos.R, pos.C] = _turn;

            Piece newTurn = _turn == Piece.PLAYER1 ? Piece.PLAYER2 : Piece.PLAYER1;
            ValidMoves = GetMoves(newTurn);

            if (ValidMoves.Count < 1) // No valid moves
            {
                ValidMoves = GetMoves(_turn);
                if (ValidMoves.Count < 1)
                {
                    int p1Score = 0, p2Score = 0;
                    foreach (Piece piece in Board.Grid)
                    {
                        if (piece == Piece.PLAYER1) p1Score++;
                        else if (piece == Piece.PLAYER2) p2Score++;
                    }

                    string winner = "draw";
                    if (p1Score > p2Score) winner = "Player1";
                    else if (p1Score < p2Score) winner = "Player2";

                    FinishedGame finishedGame = new FinishedGame(
                        winner,
                        Program.CONFIG.Deserialize<int[]>($"{winner}Color")!,
                        p1Score,
                        p2Score
                    );

                    List<FinishedGame> games = Program.GAME_HISTORY.Deserialize<List<FinishedGame>>("Games") ?? new List<FinishedGame>();
                    games.Add(finishedGame);
                    Program.GAME_HISTORY.Root["Games"] = JsonConfig.Serialize<List<FinishedGame>>(games);
                    Program.GAME_HISTORY.Save();
                }
            }
            else _turn = newTurn;

            Board.Invalidate();

            // Count the current score and raise the event ScoreUpdated
            int player1Score = 0, player2Score = 0;
            foreach (Piece piece in Board.Grid)
            {
                if (piece == Piece.PLAYER1) player1Score++;
                else if (piece == Piece.PLAYER2) player2Score++;
            }

            // Raise event
            ScoreUpdated?.Invoke(player1Score, player2Score);
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
            if (Board.Grid[move.R, move.C] != Piece.EMPTY) return false;

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
                if (nr < 0 || nc < 0 || nr >= Board.NCells || nc >= Board.NCells)
                    continue;

                // If the very next spot is not the opponent there is nothing to flank we can move to the next direction.
                if (Board.Grid[nr, nc] != opponent)
                    continue;

                // Since the very next spot is the opponent we can add it to the flips list.
                tempFlips.Add(new GridPos(nr, nc));

                // Check for out of bounds and if the new spot is not empty (then the diagonal would not be outflanked).
                while (true)
                {
                    // Move one step in the current direction.
                    nr += dr;
                    nc += dc;

                    if (nr < 0 || nc < 0 || nr >= Board.NCells || nc >= Board.NCells)
                        break;

                    // Stop checking the direction if there is an empty spot.
                    if (Board.Grid[nr, nc] == Piece.EMPTY) break;

                    // If there are any opponent pieces in the way add them to-be-flipped list.
                    if (Board.Grid[nr, nc] == opponent) tempFlips.Add(new GridPos(nr, nc));

                    // If there is at least one opponent piece in the to-be-flipped list and a player piece is found (thus the opponent pieces are flanked),
                    // then the move is valid.
                    if (Board.Grid[nr, nc] == player && tempFlips.Count > 0)
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
            for (int r = 0; r < Board.NCells; r++)
            {
                for (int c = 0; c < Board.NCells; c++)
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
