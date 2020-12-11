using System;
using System.Collections.Generic;
using Serilog;

namespace Day_11
{
    public enum SeatState
    {
        Ground, Open, Occupied
    };

    public class Ferry
    {
        public SeatState[,] WaitingArea;

        public Ferry(Ferry ferry)
        {
            WaitingArea = new SeatState[ferry.WaitingArea.GetLength(0),
                ferry.WaitingArea.GetLength(1)];

            for (var x = 0; x < this.WaitingArea.GetLength(0); x++)
            {
                for (var y = 0; y < this.WaitingArea.GetLength(1); y++)
                {
                    this[x, y] = ferry[x, y];
                }
            }
        }

        public Ferry(List<string> Data)
        {
            var width = Data[0].Length;
            var waitingArea = new SeatState[Data.Count, width];

            for (int x = 0; x < Data.Count; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    if (Data[x][y] == '#')
                    {
                        waitingArea[x, y] = SeatState.Occupied;
                    }

                    if (Data[x][y] == 'L')
                    {
                        waitingArea[x, y] = SeatState.Open;
                    }

                    if (Data[x][y] == '.')
                    {
                        waitingArea[x, y] = SeatState.Ground;
                    }
                }
            }

            WaitingArea = waitingArea;
        }

        public int AdjacentOccupiedSeatsVector(int x, int y)
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

                    if (aX >= WaitingArea.GetLength(0)
                        || aY >= WaitingArea.GetLength(1)
                        || aX < 0
                        || aY < 0)
                    {
                        break;
                    }

                    if (WaitingArea[aX, aY] == SeatState.Open)
                    {
                        break;
                    }

                    if (WaitingArea[aX, aY] == SeatState.Occupied)
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

        public int AdjacentOccupiedSeats(int x, int y)
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

                if (aX >= WaitingArea.GetLength(0)
                    || aY >= WaitingArea.GetLength(1)
                    || aX < 0
                    || aY < 0)
                {
                    continue;
                }

                if (WaitingArea[aX, aY] == SeatState.Occupied)
                {
                    adjacent++;
                }
            }

            return adjacent;
        }

        public void Print()
        {
            for (var x = 0; x < this.WaitingArea.GetLength(0); x++)
            {
                var line = "";
                for (var y = 0; y < this.WaitingArea.GetLength(1); y++)
                {
                    if (this[x, y] == SeatState.Ground)
                    {
                        line += ".";
                    }

                    if (this[x, y] == SeatState.Open)
                    {
                        line += "L";
                    }

                    if (this[x, y] == SeatState.Occupied)
                    {
                        line += "#";
                    }
                }
                Log.Verbose("{line}", line);
            }
            Console.WriteLine();
        }

        public bool IsTheSame(Ferry ferry)
        {
            for (var x = 0; x < ferry.WaitingArea.GetLength(0); x++)
            {
                for (var y = 0; y < ferry.WaitingArea.GetLength(1); y++)
                {
                    if (ferry[x, y] != this[x, y])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public int OccypiedSeats()
        {
            int occ = 0;
            for (var x = 0; x < this.WaitingArea.GetLength(0); x++)
            {
                for (var y = 0; y < this.WaitingArea.GetLength(1); y++)
                {
                    if (this[x, y] == SeatState.Occupied)
                    {
                        occ++;
                    }
                }
            }
            return occ;
        }

        public SeatState this[int x, int y]
        {
            get { return WaitingArea[x, y]; }

            set { WaitingArea[x, y] = value; }
        }
    }
}