using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TronCell.Game.Utility
{
    public static class GameMath
    {
        public static double SquaredDistance(double x1, double y1, double x2, double y2)
        {
            return ((x2 - x1) * (x2 - x1)) + ((y2 - y1) * (y2 - y1));
        }
    }
}
