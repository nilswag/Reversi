using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Game
{
    public class Game
    {
        private Board _board;

        public Game(Size windowSize)
        {
            /*
            [
            [null, null, null, null],
            [null, true, fals, null],
            [null, fals, true, null],
            [null, null, null, null]
            ]
            */

            // als [1][1] rood is en [2][1] blauw dan kan blauw plaatsen op [0][1]

            _board = new Board(windowSize, 500, 4);
            
        }

        public void Iterate()
        {
            /*
            for row in grid {
                for column in row {
                    if cell[row, column] is null {
                        Row loopen tot of een null value of de grid op is
                        Column loopen tot of een null value of de grid op is
                        Diagonaal omhoog loopen tot of een null value of de grid op is
                        Diagonaal omlaag loopen tot of een null value of de grid op is

                        Stel je bent zwart en je komt eerst wit dan zwart tegen in de zelfde, dan sluit je iemand in en voeg je deze aan je insluit array toe
                        Is aan het einde van alle loops je insluit array leeg is het geen valid move
                    }
                } 
            }
            */

            Piece[,] test =
            {
                { Piece.EMPTY, Piece.EMPTY, Piece.EMPTY, Piece.EMPTY },
                { Piece.EMPTY, Piece.PLAYER1, Piece.PLAYER2, Piece.EMPTY },
                { Piece.EMPTY, Piece.PLAYER2, Piece.PLAYER1, Piece.EMPTY },
                { Piece.EMPTY, Piece.EMPTY, Piece.EMPTY, Piece.EMPTY },
            };

            _board.Grid = test;

            List<((int, int), List<(int, int)>) > validMoves = new();
            Piece player = Piece.PLAYER1;

            for (int r = 0; r < _board.NCells; r++)
            {
                for (int c = 0; c < _board.NCells; c++)
                {
                    // Row
                    List<(int, int)> possibleValues = new();
                    for (int i = r; i < _board.NCells; i++)
                    {
                        if (_board.Grid[i, c] == Piece.EMPTY) break;
                        if (_board.Grid[r, c] != _board.Grid[i, c]) possibleValues.Add((i, c));
                        if (possibleValues.Count > 0 && _board.Grid[r, c] == player)
                        {
                            validMoves.Add(((r, c), possibleValues));
                            break;
                        }
                    }

                    possibleValues = new();
                    for (int i = r; i >= 0; i--)
                    {
                        if (_board.Grid[i, c] == Piece.EMPTY) break;
                        if (_board.Grid[r, c] != _board.Grid[i, c]) possibleValues.Add((i, c));
                        if (possibleValues.Count > 0 && _board.Grid[r, c] == player)
                        {
                            validMoves.Add(((r, c), possibleValues));
                            break;
                        }
                    }
                }

                foreach(var i in validMoves)
                {
                    string str = $"({i.Item1}, (";
                    foreach(var k in i.Item2)
                    {
                        str += $"{k}, ";
                    }
                    str += "))";
                    Console.WriteLine(str);
                }
            }
        }

    }
}
