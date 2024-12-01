using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Advent
{
    public class TextGrid
    {
        public String[,] Grid;
        public int Width => Grid.GetLength(1);
        public int Heigth => Grid.GetLength(0);

        public static readonly (int x, int y) Right = (0, 1);
        public static readonly (int x, int y) Left = (0, -1);
        public static readonly (int x, int y) Down = (1, 0);
        public static readonly (int x, int y) DownRight = (1, 1);
        public static readonly (int x, int y) DownLeft = (1, -1);
        public static readonly (int x, int y) Up = (-1, 0);
        public static readonly (int x, int y) UpRight = (-1, 1);
        public static readonly (int x, int y) UpLeft = (-1, -1);

        public List<(int x, int y)> AdjacentDirections =
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

        public TextGrid(List<string> Data, string Split)
        {
            var t = Data[0].Split(Split);
            var width = Data[0].Split(Split).Length;

            var grid = new String[Data.Count, width];

            for (int x = 0; x < Data.Count; x++)
            {
                var splitData = Data[x].Split(Split);

                for (int y = 0; y < width; y++)
                {
                    grid[x, y] = splitData[y].ToString();
                }
            }

            Grid = grid;
        }

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

        public TextGrid(int DimensionX, int DimensionY, string DefaultValue = ".")
        {
            var grid = new String[DimensionX, DimensionY];

            for (int x = 0; x < DimensionX; x++)
            {
                for (int y = 0; y < DimensionY; y++)
                {
                    grid[x, y] = DefaultValue;
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

        public IEnumerable<(string value, int x, int y)> AdjacentCellsEnumerate(int x, int y)
        {
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

                yield return (this[aX, aY], aX, aY);
            }
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

        public IEnumerable<(string value, int x, int y)> AllCells()
        {
            for (var x = 0; x < Grid.GetLength(0); x++)
            {
                for (var y = 0; y < Grid.GetLength(1); y++)
                {
                    yield return (this[x, y], x, y);
                }
            }
        }

        public IEnumerable<(string value, int x, int y)> FindString(string needle)
        {
            for (var x = 0; x < Grid.GetLength(0); x++)
            {
                for (var y = 0; y < Grid.GetLength(1); y++)
                {
                    if (this[x, y] == needle)
                    {
                        yield return (this[x, y], x, y);
                    }
                }
            }
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

        public IEnumerable<string> CellsAlongPath(int x, int y, (int x, int y) Path, int PathLength)
        {
            var aX = x;
            var aY = y;

            while (PathLength >= 0)
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

                PathLength--;
            }
        }

        public string StringAlongPath(int x, int y, (int x, int y) Path)
        {
            return string.Join("", CellsAlongPath(x, y, Path));
        }

        public string StringAlongPath(int x, int y, (int x, int y) Path, int PathLength)
        {
            return string.Join("", CellsAlongPath(x, y, Path, PathLength));
        }

        public Dictionary<string, string> Edges()
        {
            return new Dictionary<string, string>
            {
                { "Top", StringAlongPath(0, 0, Right) },
                { "Right", StringAlongPath(0, Grid.GetLength(0)-1, Down) },
                { "Bottom", StringAlongPath(Grid.GetLength(0)-1, 0, Right) },
                { "Left", StringAlongPath(0, 0, Down) }
            };
        }

        public void Grow(int Radius = 3)
        {
            var g = Radius * 2;
            var newGrid = new String[Grid.GetLength(0) + g, Grid.GetLength(1) + g];
            for (var x = 0; x < Grid.GetLength(0); x++)
            {
                for (var y = 0; y < Grid.GetLength(1); y++)
                {
                    newGrid[x + Radius, y + Radius] = Grid[x, y];
                }
            }

            Grid = newGrid;
        }

        public void FlipHorizontal()
        {
            var newGrid = new String[Grid.GetLength(0), Grid.GetLength(1)];

            for (var x = 0; x < newGrid.GetLength(0); x++)
            {
                for (var y = 0; y < newGrid.GetLength(1); y++)
                {
                    var newY = newGrid.GetLength(1) - 1 - y;
                    newGrid[x, y] = this[x, newY];
                }
            }

            Grid = newGrid;
        }

        public void FlipVertical()
        {
            var newGrid = new String[Grid.GetLength(0), Grid.GetLength(1)];

            for (var x = 0; x < newGrid.GetLength(0); x++)
            {
                for (var y = 0; y < newGrid.GetLength(1); y++)
                {
                    var newX = newGrid.GetLength(0) - 1 - x;
                    newGrid[x, y] = this[newX, y];
                }
            }

            Grid = newGrid;
        }

        public void RotateRight()
        {
            var newGrid = new String[Grid.GetLength(1), Grid.GetLength(0)];

            for (var x = 0; x < newGrid.GetLength(0); x++)
            {
                for (var y = 0; y < newGrid.GetLength(1); y++)
                {
                    var newY = newGrid.GetLength(0) - 1 - y;

                    newGrid[x, y] = this[newY, x];
                }
            }

            Grid = newGrid;
        }

        public void RotateLeft()
        {
            var newGrid = new String[Grid.GetLength(1), Grid.GetLength(0)];

            for (var x = 0; x < newGrid.GetLength(0); x++)
            {
                for (var y = 0; y < newGrid.GetLength(1); y++)
                {
                    var newX = newGrid.GetLength(1) - 1 - x;

                    newGrid[x, y] = this[y, newX];
                }
            }

            Grid = newGrid;
        }

        public void ConsolePrint()
        {
            Console.WriteLine(ToString());
        }

        public void ReplaceValueAlongPath(string OldValue, string NewValue, int x, int y, (int x, int y) Path)
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
                    break;
                }

                if (this[aX, aY] == OldValue)
                {
                    this[aX, aY] = NewValue;
                }

                aX += Path.x;
                aY += Path.y;
            }
        }

        public void ReplaceValueAlongPath(string OldValue, string NewValue, int x, int y, (int x, int y) Path, int PathLength)
        {
            var aX = x;
            var aY = y;

            while (PathLength >= 0)
            {
                if (aX >= Grid.GetLength(0)
                    || aY >= Grid.GetLength(1)
                    || aX < 0
                    || aY < 0)
                {
                    break;
                }

                if (this[aX, aY] == OldValue)
                {
                    this[aX, aY] = NewValue;
                }

                aX += Path.x;
                aY += Path.y;
                PathLength--;
            }
        }

        public void ReplaceAllofValue(string OldValue, string NewValue)
        {
            for (var x = 0; x < this.Grid.GetLength(0); x++)
            {
                for (var y = 0; y < this.Grid.GetLength(1); y++)
                {
                    if (Grid[x, y] == OldValue)
                        Grid[x, y] = NewValue;
                }
            }
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

        public string[] ToStringArray()
        {
            return ToString().Split(Environment.NewLine);
        }

        public List<string> ToStringList()
        {
            return ToStringArray().ToList();
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

        public string this[int x]
        {
            get
            {
                return StringAlongPath(x, 0, Right);
            }
        }

        public String this[int x, int y]
        {
            get { return Grid[x, y]; }

            set { Grid[x, y] = value; }
        }
    }
}