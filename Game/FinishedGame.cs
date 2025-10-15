using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Game
{
    public record FinishedGame(string Winner, int[] WinnerColor, int P1Score, int P2Score);
}
