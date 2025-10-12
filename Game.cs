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
        }

        public void GetMoves(Piece player = Piece.PLAYER1)
        {
            Piece[,] test =
            {
                { Piece.EMPTY, Piece.EMPTY, Piece.EMPTY, Piece.EMPTY },
                { Piece.EMPTY, Piece.PLAYER1, Piece.PLAYER2, Piece.EMPTY },
                { Piece.EMPTY, Piece.PLAYER2, Piece.PLAYER1, Piece.EMPTY },
                { Piece.EMPTY, Piece.EMPTY, Piece.EMPTY, Piece.EMPTY },
            };

            _board.Grid = test;
            _mainForm.Refresh();

            List<((int, int), List<(int, int)>) > moves = new();

            for (int r = 0; r < _board.NCells; r++)
            {
                for (int c = 0; c < _board.NCells; c++)
                {
                    
                }
            }

        }

    }
}
