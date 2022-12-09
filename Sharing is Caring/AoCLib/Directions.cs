using System;
using System.Collections.Generic;
using System.Text;

namespace Advent.AoCLib
{
    public static class Directions
    {
        public static readonly (int x, int y) Right = (0, 1);
        public static readonly (int x, int y) Left = (0, -1);
        public static readonly (int x, int y) Down = (1, 0);
        public static readonly (int x, int y) DownRight = (1, 1);
        public static readonly (int x, int y) DownLeft = (1, -1);
        public static readonly (int x, int y) Up = (-1, 0);
        public static readonly (int x, int y) UpRight = (-1, 1);
        public static readonly (int x, int y) UpLeft = (-1, -1);
    }
}
