using System;
using System.Collections.Generic;
using System.Text;

namespace Day_03
{
    public enum Thing
    {
        Ground, Tree
    };

    public class Slope
    {
        public Thing[,] SlopeData;

        public Thing this[int x, int y]
        {
            get => AdjustAccess(x, y);
        }

        private Thing AdjustAccess(int x, int y)
        {
            y %= SlopeData.GetLength(1);

            return SlopeData[x, y];
        }
    }
}