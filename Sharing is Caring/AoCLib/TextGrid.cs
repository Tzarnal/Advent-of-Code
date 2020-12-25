using System;
using System.Collections.Generic;
using System.Text;

namespace Advent
{
    public class TextGrid
    {
        public String[,] Grid;

        public static readonly (int x, int y) Right = (0, 1);
        public static readonly (int x, int y) Left = (0, -1);
        public static readonly (int x, int y) Down = (1, 0);
        public static readonly (int x, int y) DownRight = (1, 1);
        public static readonly (int x, int y) DownLeft = (1, -1);
        public static readonly (int x, int y) Up = (-1, 0);
        public static readonly (int x, int y) UpRight = (-1, 1);
        public static readonly (int x, int y) UpLeft = (-1, -1);

        public static readonly List<(int x, int y)> AdjacentDirections =
        new List<(int x, int y)> {
            Right,
            Left,
            Down,
            DownRight,
            DownLeft,
            Up,
            UpRight,
            UpLeft
        };

        public TextGrid(List<string> Data)
        {
            var width = Data[0].Length;
            var grid = new String[Data.Count, width];

            for (int x = 0; x < Data.Count; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    grid[x, y] = Data[x][y].ToString();
                }
            }

            Grid = grid;
        }

        public TextGrid(TextGrid inputGrid)
        {
            Grid = new String[inputGrid.Grid.GetLength(0),
                inputGrid.Grid.GetLength(1)];

            for (var x = 0; x < this.Grid.GetLength(0); x++)
            {
                for (var y = 0; y < this.Grid.GetLength(1); y++)
                {
                    this[x, y] = inputGrid[x, y];
                }
            }
        }

        public Dictionary<string, int> AdjacentCellsCountAll(int x, int y)
        {
            var output = new Dictionary<string, int>();

            foreach (var direction in AdjacentDirections)
            {
                var aX = x + direction.x;
                var aY = y + direction.y;

                if (aX >= Grid.GetLength(0)
                    || aY >= Grid.GetLength(1)
                    || aX < 0
                    || aY < 0)
                {
                    continue;
                }

                if (output.ContainsKey(Grid[aX, aY]))
                {
                    output[Grid[aX, aY]]++;
                }
                else
                {
                    output.Add(Grid[aX, aY], 1);
                }
            }

            return output;
        }
        public int AdjacentCellsCount(int x, int y, string Needle)
        {
            var count = 0;
            foreach (var direction in AdjacentDirections)
            {
                var aX = x + direction.x;
                var aY = y + direction.y;

                if (aX >= Grid.GetLength(0)
                    || aY >= Grid.GetLength(1)
                    || aX < 0
                    || aY < 0)
                {
                    continue;
                }

                if (Grid[aX, aY] == Needle)
                {
                    count++;
                }
            }

            return count;
        }

        public List<string> AdjacentCellsList(int x, int y)
        {
            var output = new List<string>();

            foreach (var direction in AdjacentDirections)
            {
                var aX = x + direction.x;
                var aY = y + direction.y;

                if (aX >= Grid.GetLength(0)
                    || aY >= Grid.GetLength(1)
                    || aX < 0
                    || aY < 0)
                {
                    continue;
                }

                output.Add(Grid[aX, aY]);
            }

            return output;
        }

        public Dictionary<(int x, int y), string> AdjacentCellsDict(int x, int y)
        {
            var output = new Dictionary<(int x, int y), string>();

            foreach (var direction in AdjacentDirections)
            {
                var aX = x + direction.x;
                var aY = y + direction.y;

                if (aX >= Grid.GetLength(0)
                 || aY >= Grid.GetLength(1)
                 || aX < 0
                 || aY < 0)
                {
                    continue;
                }

                output.Add(direction, Grid[aX, aY]);
            }

            return output;
        }

        public int CountInGrid(string Needle)
        {
            var count = 0;

            for (var x = 0; x < this.Grid.GetLength(0); x++)
            {
                for (var y = 0; y < this.Grid.GetLength(1); y++)
                {
                    if (this[x, y] == Needle)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public IEnumerable<string> CellsAlongPath(int x, int y, (int x, int y) Path)
        {
            var aX = x;
            var aY = y;

            while (true)
            {
                if (aX >= Grid.GetLength(0)
                    || aY >= Grid.GetLength(1)
                    || aX < 0
                    || aY < 0)
                {
                    yield break;
                }

                yield return this[aX, aY];

                aX += Path.x;
                aY += Path.y;
            }
        }

        public string StringAlongPath(int x, int y, (int x, int y) Path)
        {
            return string.Join("", CellsAlongPath(x, y, Path));
        }

        public void Grow(int i = 3)
        {
            var g = i * 2;
            var newGrid = new String[Grid.GetLength(0) + g, Grid.GetLength(1) + g];
            for (var x = 0; x < Grid.GetLength(0); x++)
            {
                for (var y = 0; y < Grid.GetLength(1); y++)
                {
                    newGrid[x + i, y + i] = Grid[x, y];
                }
            }

            Grid = newGrid;
        }

        public void ConsolePrint()
        {
            Console.WriteLine(ToString());
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (var x = 0; x < this.Grid.GetLength(0); x++)
            {
                for (var y = 0; y < this.Grid.GetLength(1); y++)
                {
                    sb.Append(Grid[x, y]);
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        public bool Equals(TextGrid inputGrid)
        {
            for (var x = 0; x < this.Grid.GetLength(0); x++)
            {
                for (var y = 0; y < this.Grid.GetLength(1); y++)
                {
                    if (this[x, y] != inputGrid[x, y])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public String this[int x, int y]
        {
            get { return Grid[x, y]; }

            set { Grid[x, y] = value; }
        }
    }
}