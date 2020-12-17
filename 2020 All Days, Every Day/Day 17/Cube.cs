using System;
using System.Collections.Generic;
using Serilog;

namespace Day_17
{
    public enum CubeState
    {
        Inactive, Active
    };

    public class Cube
    {
        public CubeState[,,] CubeSpace;

        public Cube(Cube cubeSpace)
        {
            CubeSpace = new CubeState[cubeSpace.CubeSpace.GetLength(0),
            cubeSpace.CubeSpace.GetLength(1), cubeSpace.CubeSpace.GetLength(2)];

            for (var x = 0; x < this.CubeSpace.GetLength(0); x++)
            {
                for (var y = 0; y < this.CubeSpace.GetLength(1); y++)
                {
                    for (var z = 0; z < this.CubeSpace.GetLength(2); z++)
                    {
                        this[x, y, z] = cubeSpace[x, y, z];
                    }
                }
            }
        }

        public Cube(List<string> Data)
        {
            var width = Data[0].Length;
            var cubeSpace = new CubeState[Data.Count, width, 1];

            for (int x = 0; x < Data.Count; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    if (Data[x][y] == '#')
                    {
                        cubeSpace[x, y, 0] = CubeState.Active;
                    }

                    if (Data[x][y] == '.')
                    {
                        cubeSpace[x, y, 0] = CubeState.Inactive;
                    }
                }
            }

            CubeSpace = cubeSpace;
        }

        public void Grow(int i = 3)
        {
            var g = i * 2;
            var newCube = new CubeState[CubeSpace.GetLength(0) + g, CubeSpace.GetLength(1) + g, CubeSpace.GetLength(2) + g];
            for (var x = 0; x < CubeSpace.GetLength(0); x++)
            {
                for (var y = 0; y < CubeSpace.GetLength(1); y++)
                {
                    for (var z = 0; z < CubeSpace.GetLength(2); z++)
                    {
                        newCube[x + i, y + i, z + i] = CubeSpace[x, y, z];
                    }
                }
            }

            CubeSpace = newCube;
        }

        public int AdjacentActiveCells(int x, int y, int z)
        {
            var modifiers = new List<(int x, int y, int z)> { (-1, -1, -1), (-1, -1, 0), (-1, -1, 1), (-1, 0, -1), (-1, 0, 0), (-1, 0, 1), (-1, 1, -1), (-1, 1, 0), (-1, 1, 1), (0, -1, -1), (0, -1, 0), (0, -1, 1), (0, 0, -1), (0, 0, 1), (0, 1, -1), (0, 1, 0), (0, 1, 1), (1, -1, -1), (1, -1, 0), (1, -1, 1), (1, 0, -1), (1, 0, 0), (1, 0, 1), (1, 1, -1), (1, 1, 0), (1, 1, 1) };

            var adjacent = 0;
            foreach (var mod in modifiers)
            {
                var aX = x + mod.x;
                var aY = y + mod.y;
                var aZ = z + mod.z;

                if (aX >= CubeSpace.GetLength(0)
                    || aY >= CubeSpace.GetLength(1)
                    || aZ >= CubeSpace.GetLength(2)
                    || aX < 0
                    || aY < 0
                    || aZ < 0)
                {
                    continue;
                }

                if (CubeSpace[aX, aY, aZ] == CubeState.Active)
                {
                    adjacent++;
                }
            }

            return adjacent;
        }

        public void Print()
        {
            for (var x = 0; x < this.CubeSpace.GetLength(0); x++)
            {
                var line = "";
                for (var y = 0; y < this.CubeSpace.GetLength(1); y++)
                {
                    for (var z = 0; z < this.CubeSpace.GetLength(2); z++)
                    {
                        if (this[x, y, z] == CubeState.Inactive)
                        {
                            line += ".";
                        }

                        if (this[x, y, z] == CubeState.Active)
                        {
                            line += "#";
                        }
                    }
                }
                Log.Verbose("{line}", line);
            }
            Console.WriteLine();
        }

        public bool IsTheSame(Cube hypercube)
        {
            for (var x = 0; x < hypercube.CubeSpace.GetLength(0); x++)
            {
                for (var y = 0; y < hypercube.CubeSpace.GetLength(1); y++)
                {
                    for (var z = 0; z < this.CubeSpace.GetLength(2); z++)
                    {
                        if (hypercube[x, y, z] != this[x, y, z])
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public int ActiveCubes()
        {
            int occ = 0;
            for (var x = 0; x < this.CubeSpace.GetLength(0); x++)
            {
                for (var y = 0; y < this.CubeSpace.GetLength(1); y++)
                {
                    for (var z = 0; z < this.CubeSpace.GetLength(2); z++)
                    {
                        if (this[x, y, z] == CubeState.Active)
                        {
                            occ++;
                        }
                    }
                }
            }
            return occ;
        }

        public void Print(int z = 0)
        {
            for (var x = 0; x < this.CubeSpace.GetLength(0); x++)
            {
                var line = "";
                for (var y = 0; y < this.CubeSpace.GetLength(1); y++)
                {
                    if (this[x, y, z] == CubeState.Active)
                    {
                        line += "#";
                    }
                    if (this[x, y, z] == CubeState.Inactive)
                    {
                        line += ".";
                    }
                }
                Log.Verbose("{line}", line);
            }
            Console.WriteLine();
        }

        public CubeState this[int x, int y, int z]
        {
            get { return CubeSpace[x, y, z]; }

            set { CubeSpace[x, y, z] = value; }
        }
    }
}