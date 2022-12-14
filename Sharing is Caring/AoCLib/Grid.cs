using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using Advent.AoCLib;

namespace Advent
{
    public class Grid<T>
    {
        public T[,] InternalGrid;

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

        public Grid(T[,] inputArray)
        {
            InternalGrid = new T[inputArray.GetLength(0), inputArray.GetLength(1)];

            for (var x = 0; x < this.InternalGrid.GetLength(0); x++)
            {
                for (var y = 0; y < this.InternalGrid.GetLength(1); y++)
                {
                    this[x, y] = inputArray[x, y];
                }
            }
        }

        public Grid(int DimensionX, int DimensionY, T DefaultValue)
        {
            var grid = new T[DimensionX, DimensionY];

            for (int x = 0; x < DimensionX; x++)
            {
                for (int y = 0; y < DimensionY; y++)
                {
                    grid[x, y] = DefaultValue;
                }
            }

            InternalGrid = grid;
        }

        public Grid(Grid<T> inputGrid)
        {
            InternalGrid = new T[inputGrid.InternalGrid.GetLength(0),
                inputGrid.InternalGrid.GetLength(1)];

            for (var x = 0; x < this.InternalGrid.GetLength(0); x++)
            {
                for (var y = 0; y < this.InternalGrid.GetLength(1); y++)
                {
                    this[x, y] = inputGrid[x, y];
                }
            }
        }

        public Dictionary<T, int> AdjacentCellsCountAll(int x, int y)
        {
            var output = new Dictionary<T, int>();

            foreach (var direction in AdjacentDirections)
            {
                var aX = x + direction.x;
                var aY = y + direction.y;

                if (aX >= InternalGrid.GetLength(0)
                    || aY >= InternalGrid.GetLength(1)
                    || aX < 0
                    || aY < 0)
                {
                    continue;
                }

                if (output.ContainsKey(InternalGrid[aX, aY]))
                {
                    output[InternalGrid[aX, aY]]++;
                }
                else
                {
                    output.Add(InternalGrid[aX, aY], 1);
                }
            }

            return output;
        }
        public int AdjacentCellsCount(int x, int y, T Needle)
        {
            var count = 0;
            foreach (var direction in AdjacentDirections)
            {
                var aX = x + direction.x;
                var aY = y + direction.y;

                if (aX >= InternalGrid.GetLength(0)
                    || aY >= InternalGrid.GetLength(1)
                    || aX < 0
                    || aY < 0)
                {
                    continue;
                }

                if (InternalGrid[aX, aY].Equals(Needle))
                {
                    count++;
                }
            }

            return count;
        }

        public List<T> AdjacentCellsList(int x, int y)
        {
            var output = new List<T>();

            foreach (var direction in AdjacentDirections)
            {
                var aX = x + direction.x;
                var aY = y + direction.y;

                if (aX >= InternalGrid.GetLength(0)
                    || aY >= InternalGrid.GetLength(1)
                    || aX < 0
                    || aY < 0)
                {
                    continue;
                }

                output.Add(InternalGrid[aX, aY]);
            }

            return output;
        }

        public IEnumerable<(T value, int x, int y)> AdjacentCellsEnumerate(int x, int y)
        {
            foreach (var direction in AdjacentDirections)
            {
                var aX = x + direction.x;
                var aY = y + direction.y;

                if (aX >= InternalGrid.GetLength(0)
                    || aY >= InternalGrid.GetLength(1)
                    || aX < 0
                    || aY < 0)
                {
                    continue;
                }

                yield return (this[aX, aY], aX, aY);
            }
        }

        public Dictionary<(int x, int y), T> AdjacentCellsDict(int x, int y)
        {
            var output = new Dictionary<(int x, int y), T>();

            foreach (var direction in AdjacentDirections)
            {
                var aX = x + direction.x;
                var aY = y + direction.y;

                if (aX >= InternalGrid.GetLength(0)
                 || aY >= InternalGrid.GetLength(1)
                 || aX < 0
                 || aY < 0)
                {
                    continue;
                }

                output.Add(direction, InternalGrid[aX, aY]);
            }

            return output;
        }

        public int CountInGrid(T Needle)
        {
            var count = 0;

            for (var x = 0; x < this.InternalGrid.GetLength(0); x++)
            {
                for (var y = 0; y < this.InternalGrid.GetLength(1); y++)
                {
                    if (this[x, y].Equals(Needle))
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public IEnumerable<(T value, int x, int y)> AllCells()
        {
            for (var x = 0; x < InternalGrid.GetLength(0); x++)
            {
                for (var y = 0; y < InternalGrid.GetLength(1); y++)
                {
                    yield return (this[x, y], x, y);
                }
            }
        }

        public IEnumerable<(T value, int x, int y)> CellsWithValue(T needle)
        {
            for (var x = 0; x < InternalGrid.GetLength(0); x++)
            {
                for (var y = 0; y < InternalGrid.GetLength(1); y++)
                {
                    if (this[x, y].Equals(needle))
                    {
                        yield return (this[x, y], x, y);
                    }
                }
            }
        }

        public IEnumerable<T> CellsAlongPath(int x, int y, (int x, int y) Path)
        {
            var aX = x;
            var aY = y;

            while (true)
            {
                if (aX >= InternalGrid.GetLength(0)
                    || aY >= InternalGrid.GetLength(1)
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

        public IEnumerable<T> CellsAlongPath(int x, int y, (int x, int y) Path, int PathLength)
        {
            var aX = x;
            var aY = y;

            while (PathLength >= 0)
            {
                if (aX >= InternalGrid.GetLength(0)
                    || aY >= InternalGrid.GetLength(1)
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

        public Dictionary<string, string> StringEdges()
        {
            return new Dictionary<string, string>
            {
                { "Top", StringAlongPath(0, 0, Right) },
                { "Right", StringAlongPath(0, InternalGrid.GetLength(0)-1, Down) },
                { "Bottom", StringAlongPath(InternalGrid.GetLength(0)-1, 0, Right) },
                { "Left", StringAlongPath(0, 0, Down) }
            };
        }

        public void Grow(int Radius = 3)
        {
            var g = Radius * 2;
            var newGrid = new T[InternalGrid.GetLength(0) + g, InternalGrid.GetLength(1) + g];
            for (var x = 0; x < InternalGrid.GetLength(0); x++)
            {
                for (var y = 0; y < InternalGrid.GetLength(1); y++)
                {
                    newGrid[x + Radius, y + Radius] = InternalGrid[x, y];
                }
            }

            InternalGrid = newGrid;
        }

        public void FlipHorizontal()
        {
            var newGrid = new T[InternalGrid.GetLength(0), InternalGrid.GetLength(1)];

            for (var x = 0; x < newGrid.GetLength(0); x++)
            {
                for (var y = 0; y < newGrid.GetLength(1); y++)
                {
                    var newY = newGrid.GetLength(1) - 1 - y;
                    newGrid[x, y] = this[x, newY];
                }
            }

            InternalGrid = newGrid;
        }

        public void FlipVertical()
        {
            var newGrid = new T[InternalGrid.GetLength(0), InternalGrid.GetLength(1)];

            for (var x = 0; x < newGrid.GetLength(0); x++)
            {
                for (var y = 0; y < newGrid.GetLength(1); y++)
                {
                    var newX = newGrid.GetLength(0) - 1 - x;
                    newGrid[x, y] = this[newX, y];
                }
            }

            InternalGrid = newGrid;
        }

        public void RotateRight()
        {
            var newGrid = new T[InternalGrid.GetLength(1), InternalGrid.GetLength(0)];

            for (var x = 0; x < newGrid.GetLength(0); x++)
            {
                for (var y = 0; y < newGrid.GetLength(1); y++)
                {
                    var newY = newGrid.GetLength(0) - 1 - y;

                    newGrid[x, y] = this[newY, x];
                }
            }

            InternalGrid = newGrid;
        }

        public void RotateLeft()
        {
            var newGrid = new T[InternalGrid.GetLength(1), InternalGrid.GetLength(0)];

            for (var x = 0; x < newGrid.GetLength(0); x++)
            {
                for (var y = 0; y < newGrid.GetLength(1); y++)
                {
                    var newX = newGrid.GetLength(1) - 1 - x;

                    newGrid[x, y] = this[y, newX];
                }
            }

            InternalGrid = newGrid;
        }

        public void ConsolePrint()
        {
            Console.WriteLine(ToString());
        }

        public void ConsoleClearPrint()
        {
            Console.WriteLine(Helpers.ClearGridString(ToString()));
        }

        public void FilePrint(string filePath)
        {
            File.WriteAllText(filePath, ToString());
        }

        public void FileClearPrint(string filePath)
        {
            File.WriteAllText(filePath, Helpers.ClearGridString(ToString()));
        }

        public void ReplaceValueAlongPath(T OldValue, T NewValue, int x, int y, (int x, int y) Path)
        {
            var aX = x;
            var aY = y;

            while (true)
            {
                if (aX >= InternalGrid.GetLength(0)
                    || aY >= InternalGrid.GetLength(1)
                    || aX < 0
                    || aY < 0)
                {
                    break;
                }

                if (this[aX, aY].Equals(OldValue))
                {
                    this[aX, aY] = NewValue;
                }

                aX += Path.x;
                aY += Path.y;
            }
        }

        public void ReplaceValueAlongPath(T OldValue, T NewValue, int x, int y, (int x, int y) Path, int PathLength)
        {
            var aX = x;
            var aY = y;

            while (PathLength >= 0)
            {
                if (aX >= InternalGrid.GetLength(0)
                    || aY >= InternalGrid.GetLength(1)
                    || aX < 0
                    || aY < 0)
                {
                    break;
                }

                if (this[aX, aY].Equals(OldValue))
                {
                    this[aX, aY] = NewValue;
                }

                aX += Path.x;
                aY += Path.y;
                PathLength--;
            }
        }

        public void ReplaceAllofValue(T OldValue, T NewValue)
        {
            for (var x = 0; x < this.InternalGrid.GetLength(0); x++)
            {
                for (var y = 0; y < this.InternalGrid.GetLength(1); y++)
                {
                    if (InternalGrid[x, y].Equals(OldValue))
                        InternalGrid[x, y] = NewValue;
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (var x = 0; x < this.InternalGrid.GetLength(0); x++)
            {
                for (var y = 0; y < this.InternalGrid.GetLength(1); y++)
                {
                    sb.Append(InternalGrid[x, y]);
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

        public bool Equals(Grid<T> inputGrid)
        {
            for (var x = 0; x < this.InternalGrid.GetLength(0); x++)
            {
                for (var y = 0; y < this.InternalGrid.GetLength(1); y++)
                {
                    if (this[x, y].Equals(inputGrid[x, y]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public T this[int x, int y]
        {
            get { return InternalGrid[x, y]; }

            set { InternalGrid[x, y] = value; }
        }

        public T this[IntVector2 point]
        {
            get { return InternalGrid[point.X, point.Y]; }

            set { InternalGrid[point.X, point.Y] = value; }
        }
    }
}