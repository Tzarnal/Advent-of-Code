using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Advent;
using System.Linq;

namespace Day_20
{
    public enum TileState
    {
        Empty, Filled
    };

    public class Tile
    {
        public TileState[,] Grid;
        public String TileID;
        public int TileIDNumber;

        public Tile(Tile grid)
        {
            Grid = new TileState[grid.Grid.GetLength(0),
                grid.Grid.GetLength(1)];

            for (var x = 0; x < this.Grid.GetLength(0); x++)
            {
                for (var y = 0; y < this.Grid.GetLength(1); y++)
                {
                    this[x, y] = grid[x, y];
                }
            }

            TileID = grid.TileID;
            TileIDNumber = grid.TileIDNumber;
        }

        public Tile(List<string> Data)
        {
            var width = Data[0].Length;
            var grid = new TileState[Data.Count, width];

            for (int x = 0; x < Data.Count; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    if (Data[x][y] == '#')
                    {
                        grid[x, y] = TileState.Filled;
                    }

                    if (Data[x][y] == '.')
                    {
                        grid[x, y] = TileState.Empty;
                    }
                }
            }

            Grid = grid;
        }

        public int AdjacentFilledVector(int x, int y)
        {
            var modifiers = new List<(int x, int y)>
            {
                (0,1),(0,-1),(1,0),(1,1),(1,-1),(-1,0),(-1,1),(-1,-1)
            };

            var adjacent = 0;
            foreach (var mod in modifiers)
            {
                var foundAdjacent = false;

                var aX = x;
                var aY = y;

                while (!foundAdjacent)
                {
                    aX += mod.x;
                    aY += mod.y;

                    if (aX >= Grid.GetLength(0)
                        || aY >= Grid.GetLength(1)
                        || aX < 0
                        || aY < 0)
                    {
                        break;
                    }

                    if (Grid[aX, aY] == TileState.Filled)
                    {
                        foundAdjacent = true;
                    }
                }

                if (foundAdjacent)
                {
                    adjacent++;
                }
            }

            return adjacent;
        }

        public int AdjacentFilled(int x, int y)
        {
            var modifiers = new List<(int x, int y)>
            {
                (0,1),(0,-1),(1,0),(1,1),(1,-1),(-1,0),(-1,1),(-1,-1)
            };

            var adjacent = 0;
            foreach (var mod in modifiers)
            {
                var aX = x + mod.x;
                var aY = y + mod.y;

                if (aX >= Grid.GetLength(0)
                    || aY >= Grid.GetLength(1)
                    || aX < 0
                    || aY < 0)
                {
                    continue;
                }

                if (Grid[aX, aY] == TileState.Filled)
                {
                    adjacent++;
                }
            }

            return adjacent;
        }

        public void Print()
        {
            Log.Verbose("{tileid}:", TileID);

            for (var x = 0; x < this.Grid.GetLength(0); x++)
            {
                var line = "";
                for (var y = 0; y < this.Grid.GetLength(1); y++)
                {
                    if (this[x, y] == TileState.Empty)
                    {
                        line += ".";
                    }

                    if (this[x, y] == TileState.Filled)
                    {
                        line += "#";
                    }
                }
                Log.Verbose("{line}", line);
            }
            Console.WriteLine();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{TileID}:");

            for (var x = 0; x < this.Grid.GetLength(0); x++)
            {
                for (var y = 0; y < this.Grid.GetLength(1); y++)
                {
                    if (this[x, y] == TileState.Empty)
                    {
                        sb.Append(".");
                    }

                    if (this[x, y] == TileState.Filled)
                    {
                        sb.Append("#");
                    }
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public List<int> GetEdgesMin()
        {
            string topEdge = "";
            string bottomEdge = "";

            for (var x = 0; x < Grid.GetLength(0); x++)
            {
                topEdge += StringPos(x, 0);
                bottomEdge += StringPos(x, Grid.GetLength(1) - 1);
            }

            string leftEdge = "";
            string rightEdge = "";
            for (var y = 0; y < Grid.GetLength(1); y++)
            {
                leftEdge += StringPos(0, y);
                rightEdge += StringPos(Grid.GetLength(0) - 1, y);
            }

            var topHash = Math.Min(topEdge.GetHashCode(), topEdge.Reverse().GetHashCode());
            var bottomHash = Math.Min(bottomEdge.GetHashCode(), bottomEdge.Reverse().GetHashCode());
            var leftHash = Math.Min(leftEdge.GetHashCode(), leftEdge.Reverse().GetHashCode());
            var rightHash = Math.Min(rightEdge.GetHashCode(), rightEdge.Reverse().GetHashCode());

            return new List<int>
            {
                topHash,
                leftHash,
                bottomHash,
                rightHash
            };
        }

        public int LeftEdge => GetTopEdge();

        public int GetLeftEdge()
        {
            string leftEdge = "";
            for (var y = 0; y < Grid.GetLength(1); y++)
            {
                leftEdge += StringPos(0, y);
            }
            return leftEdge.GetHashCode();
        }

        public int TopEdge => GetTopEdge();

        public int GetTopEdge()
        {
            string topEdge = "";
            for (var x = 0; x < Grid.GetLength(0); x++)
            {
                topEdge += StringPos(x, 0);
            }

            return topEdge.GetHashCode();
        }

        public (int Top, int Bottom, int Left, int Right) GetEdges()
        {
            string topEdge = "";
            string bottomEdge = "";

            for (var x = 0; x < Grid.GetLength(0); x++)
            {
                topEdge += StringPos(x, 0);
                bottomEdge += StringPos(x, Grid.GetLength(1) - 1);
            }

            string leftEdge = "";
            string rightEdge = "";
            for (var y = 0; y < Grid.GetLength(1); y++)
            {
                leftEdge += StringPos(0, y);
                rightEdge += StringPos(Grid.GetLength(0) - 1, y);
            }

            var topHash = topEdge.GetHashCode();
            var bottomHash = bottomEdge.GetHashCode();
            var leftHash = leftEdge.GetHashCode();
            var rightHash = rightEdge.GetHashCode();

            return (topHash, bottomHash, leftHash, rightHash);
        }

        public List<int> GetPotentialEdges()
        {
            string topEdge = "";
            string bottomEdge = "";

            for (var x = 0; x < Grid.GetLength(0); x++)
            {
                topEdge += StringPos(x, 0);
                bottomEdge += StringPos(x, Grid.GetLength(1) - 1);
            }

            string leftEdge = "";
            string rightEdge = "";
            for (var y = 0; y < Grid.GetLength(1); y++)
            {
                leftEdge += StringPos(0, y);
                rightEdge += StringPos(Grid.GetLength(0) - 1, y);
            }

            var topHash = topEdge.GetHashCode();
            var topHashR = topEdge.Reverse().GetHashCode();
            var bottomHash = bottomEdge.GetHashCode();
            var bottomHashR = bottomEdge.Reverse().GetHashCode();
            var leftHash = leftEdge.GetHashCode();
            var leftHashR = leftEdge.Reverse().GetHashCode();
            var rightHash = rightEdge.GetHashCode();
            var rightHashR = rightEdge.Reverse().GetHashCode();

            return new List<int> { topHash, topHashR,
                                   bottomHash, bottomHashR,
                                   leftHash, leftHashR,
                                   rightHash, rightHashR};
        }

        public bool IsTheSame(Tile grid)
        {
            for (var x = 0; x < grid.Grid.GetLength(0); x++)
            {
                for (var y = 0; y < grid.Grid.GetLength(1); y++)
                {
                    if (grid[x, y] != this[x, y])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool Equals(Tile grid)
        {
            return IsTheSame(grid);
        }

        public int FilledCount()
        {
            int count = 0;
            for (var x = 0; x < this.Grid.GetLength(0); x++)
            {
                for (var y = 0; y < this.Grid.GetLength(1); y++)
                {
                    if (this[x, y] == TileState.Filled)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public bool RotateEdgeUp(int Edge)
        {
            if (!GetPotentialEdges().Contains(Edge))
            {
                return false;
            }

            foreach (var _ in Enumerable.Range(0, 4))
            {
                if (this.TopEdge == Edge)
                {
                    return true;
                }
                Rotate();
            }

            Mirror();

            foreach (var _ in Enumerable.Range(0, 4))
            {
                if (this.TopEdge == Edge)
                {
                    return true;
                }
                Rotate();
            }

            return false;
        }

        public bool RotateEdgeLeft(int Edge)
        {
            if (!GetPotentialEdges().Contains(Edge))
            {
                return false;
            }

            foreach (var _ in Enumerable.Range(0, 4))
            {
                if (LeftEdge == Edge)
                {
                    return true;
                }
                Rotate();
            }

            Mirror();

            foreach (var _ in Enumerable.Range(0, 4))
            {
                if (LeftEdge == Edge)
                {
                    return true;
                }
                Rotate();
            }

            return false;
        }

        public void Mirror()
        {
            var gridMirrored = new TileState[Grid.GetLength(0), Grid.GetLength(1)];

            for (int x = 0; x < Grid.GetLength(0); x++)
            {
                for (int y = 0; y < Grid.GetLength(1); y++)
                {
                    var newX = Grid.GetLength(0) - (x + 1);
                    gridMirrored[newX, y] = Grid[x, y];
                }
            }

            Grid = gridMirrored;
        }

        public void Rotate()
        {
            var gridRotated = new TileState[Grid.GetLength(1), Grid.GetLength(0)];

            for (int x = 0; x < Grid.GetLength(0); x++)
            {
                for (int y = 0; y < Grid.GetLength(1); y++)
                {
                    var newX = y;
                    var newY = Grid.GetLength(0) - (x + 1);
                    gridRotated[newX, newY] = Grid[x, y];
                }
            }
            Grid = gridRotated;
        }

        public string StringPos(int x, int y)
        {
            if (this[x, y] == TileState.Empty)
            {
                return ".";
            }

            if (this[x, y] == TileState.Filled)
            {
                return "#";
            }

            return ".";
        }

        public TileState this[int x, int y]
        {
            get { return Grid[x, y]; }

            set { Grid[x, y] = value; }
        }
    }
}