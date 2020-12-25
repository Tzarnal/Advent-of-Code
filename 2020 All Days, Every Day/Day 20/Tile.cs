using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Advent;
using System.Linq;

namespace Day_20
{
    public class Tile : TextGrid
    {
        public String TileID;
        public int TileIDNumber;

        public HashSet<int> PossibleEdges;

        public Tile(List<string> Data) : base(Data)
        {
            TileID = "";
            TileIDNumber = 0;

            SetEdges();
        }

        public Tile(Tile Tile) : base(Tile)
        {
            TileID = Tile.TileID;
            TileIDNumber = Tile.TileIDNumber;

            SetEdges();
        }

        public List<int> MinEdges()
        {
            var edges = Edges();
            var top = edges["Top"];
            var right = edges["Right"];
            var bottom = edges["Bottom"];
            var left = edges["Left"];

            var topReversed = top.Reverse();
            var rightReversed = right.Reverse();
            var bottomReversed = bottom.Reverse();
            var leftReversed = left.Reverse();

            return new List<int>
            {
                Math.Min(EdgetoInt(top), EdgetoInt(topReversed)),
                Math.Min(EdgetoInt(right), EdgetoInt(rightReversed)),
                Math.Min(EdgetoInt(bottom), EdgetoInt(bottomReversed)),
                Math.Min(EdgetoInt(left), EdgetoInt(leftReversed))
            };
        }

        public void SetEdges()
        {
            var edges = Edges();
            var top = edges["Top"];
            var right = edges["Right"];
            var bottom = edges["Bottom"];
            var left = edges["Left"];

            var topReversed = top.Reverse();
            var rightReversed = right.Reverse();
            var bottomReversed = bottom.Reverse();
            var leftReversed = left.Reverse();

            PossibleEdges = new HashSet<int>
            {
                EdgetoInt(top), EdgetoInt(right), EdgetoInt(bottom), EdgetoInt(left),
                EdgetoInt(topReversed), EdgetoInt(rightReversed), EdgetoInt(bottomReversed), EdgetoInt(leftReversed)
            };
        }

        public int EdgetoInt(string edge)
        {
            edge = edge.Replace('#', '1').Replace('.', '0');

            return Convert.ToInt32(edge, 2);
        }

        public int LeftEdgeInt()
        {
            var leftEdge = StringAlongPath(0, 0, Down);
            return EdgetoInt(leftEdge);
        }

        public int RightEdgeInt()
        {
            var rightEdge = StringAlongPath(0, Grid.GetLength(0) - 1, Down);
            return EdgetoInt(rightEdge);
        }

        public int TopEdgeInt()
        {
            var topEdge = StringAlongPath(0, 0, Right);
            return EdgetoInt(topEdge);
        }

        public int BottomEdgeInt()
        {
            var bottomEdge = StringAlongPath(Grid.GetLength(0) - 1, 0, Right);
            return EdgetoInt(bottomEdge);
        }

        new public void ConsolePrint()
        {
            Console.WriteLine(TileID);
            Console.WriteLine(ToString());
        }

        public bool RotateEdgeToLeft(int desiredEdge)
        {
            if (LeftEdgeInt() == desiredEdge)
                return true;

            if (!PossibleEdges.Contains(desiredEdge))
                return false;

            for (int i = 0; i <= 4; i++)
            {
                RotateLeft();

                if (LeftEdgeInt() == desiredEdge)
                    return true;
            }

            FlipHorizontal();

            if (LeftEdgeInt() == desiredEdge)
                return true;

            for (int i = 0; i <= 4; i++)
            {
                RotateLeft();

                if (LeftEdgeInt() == desiredEdge)
                    return true;
            }

            return false;
        }

        public bool RotateEdgeToTop(int desiredEdge)
        {
            if (TopEdgeInt() == desiredEdge)
                return true;

            if (!PossibleEdges.Contains(desiredEdge))
                return false;

            for (int i = 0; i <= 4; i++)
            {
                RotateLeft();

                if (TopEdgeInt() == desiredEdge)
                    return true;
            }

            FlipHorizontal();

            if (TopEdgeInt() == desiredEdge)
                return true;

            for (int i = 0; i <= 4; i++)
            {
                RotateLeft();

                if (TopEdgeInt() == desiredEdge)
                    return true;
            }

            return false;
        }

        new public string ToString()
        {
            var sb = new StringBuilder();

            for (var x = 0; x < this.Grid.GetLength(0); x++)
            {
                for (var y = 0; y < this.Grid.GetLength(1); y++)
                {
                    sb.Append(Grid[x, y] + " ");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}