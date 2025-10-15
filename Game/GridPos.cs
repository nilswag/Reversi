using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Game
{
    /// <summary>
    /// Record for a grid position.
    /// </summary>
    /// <param name="R">Row index of grid.</param>
    /// <param name="C">Column index of grid.</param>
    public record GridPos(int R, int C);
}
