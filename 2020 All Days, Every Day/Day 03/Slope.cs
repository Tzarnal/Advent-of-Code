using System;
using System.Collections.Generic;
using System.Text;

namespace Day_03
{
    public enum SlopeThing
    {
        Ground, Tree
    };

    public class Slope
    {
        public SlopeThing[,] SlopeData;

        public Slope(List<string> Data)
        {
            var width = Data[0].Length;
            var slopeData = new SlopeThing[Data.Count, width];

            for (int x = 0; x < Data.Count; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    if (Data[x][y] == '#')
                    {
                        slopeData[x, y] = SlopeThing.Tree;
                    }
                }
            }

            SlopeData = slopeData;
        }

        public SlopeThing this[int x, int y]
        {
            get => AdjustAccess(x, y);
        }

        private SlopeThing AdjustAccess(int x, int y)
        {
            y %= SlopeData.GetLength(1);

            return SlopeData[x, y];
        }
    }
}