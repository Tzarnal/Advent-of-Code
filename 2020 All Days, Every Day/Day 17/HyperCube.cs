using System;
using System.Collections.Generic;
using Serilog;

namespace Day_17
{
    public class HyperCube
    {
        public CubeState[,,,] CubeSpace;

        private List<(int x, int y, int z, int w)> _modifiers;
        public HyperCube(HyperCube cubeSpace)
        {
            CubeSpace = new CubeState[cubeSpace.CubeSpace.GetLength(0),
            cubeSpace.CubeSpace.GetLength(1), cubeSpace.CubeSpace.GetLength(2), cubeSpace.CubeSpace.GetLength(3)];

            for (var x = 0; x < this.CubeSpace.GetLength(0); x++)
            {
                for (var y = 0; y < this.CubeSpace.GetLength(1); y++)
                {
                    for (var z = 0; z < this.CubeSpace.GetLength(2); z++)
                    {
                        for (var w = 0; w < this.CubeSpace.GetLength(3); w++)
                        {
                            this[x, y, z, w] = cubeSpace[x, y, z, w];
                        }
                    }
                }
            }

            _modifiers = GenerateModifiers;
        }

        public HyperCube(List<string> Data)
        {
            var width = Data[0].Length;
            var cubeSpace = new CubeState[Data.Count, width, 1, 1];

            for (int x = 0; x < Data.Count; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    if (Data[x][y] == '#')
                    {
                        cubeSpace[x, y, 0, 0] = CubeState.Active;
                    }

                    if (Data[x][y] == '.')
                    {
                        cubeSpace[x, y, 0, 0] = CubeState.Inactive;
                    }
                }
            }

            CubeSpace = cubeSpace;
            _modifiers = GenerateModifiers;
        }

        public void Grow(int i = 3)
        {
            var g = i * 2;
            var newCube = new CubeState[CubeSpace.GetLength(0) + g, CubeSpace.GetLength(1) + g, CubeSpace.GetLength(2) + g, CubeSpace.GetLength(3) + g];
            for (var x = 0; x < CubeSpace.GetLength(0); x++)
            {
                for (var y = 0; y < CubeSpace.GetLength(1); y++)
                {
                    for (var z = 0; z < CubeSpace.GetLength(2); z++)
                    {
                        for (var w = 0; w < this.CubeSpace.GetLength(3); w++)
                        {
                            newCube[x + i, y + i, z + i, w + i] = CubeSpace[x, y, z, w];
                        }
                    }
                }
            }

            CubeSpace = newCube;
        }

        public int AdjacentActiveCells(int x, int y, int z, int w)
        {
            var adjacent = 0;
            foreach (var mod in _modifiers)
            {
                var aX = x + mod.x;
                var aY = y + mod.y;
                var aZ = z + mod.z;
                var aW = w + mod.w;

                if (aX >= CubeSpace.GetLength(0)
                    || aY >= CubeSpace.GetLength(1)
                    || aZ >= CubeSpace.GetLength(2)
                    || aW >= CubeSpace.GetLength(3)
                    || aX < 0
                    || aY < 0
                    || aZ < 0
                    || aW < 0)
                {
                    continue;
                }

                if (CubeSpace[aX, aY, aZ, aW] == CubeState.Active)
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
                        for (var w = 0; w < this.CubeSpace.GetLength(3); w++)
                        {
                            if (this[x, y, z, w] == CubeState.Inactive)
                            {
                                line += ".";
                            }

                            if (this[x, y, z, w] == CubeState.Active)
                            {
                                line += "#";
                            }
                        }
                    }
                }
                Log.Verbose("{line}", line);
            }
            Console.WriteLine();
        }

        public bool IsTheSame(HyperCube hypercube)
        {
            for (var x = 0; x < hypercube.CubeSpace.GetLength(0); x++)
            {
                for (var y = 0; y < hypercube.CubeSpace.GetLength(1); y++)
                {
                    for (var z = 0; z < this.CubeSpace.GetLength(2); z++)
                    {
                        for (var w = 0; w < this.CubeSpace.GetLength(3); w++)
                        {
                            if (hypercube[x, y, z, w] != this[x, y, z, w])
                            {
                                return false;
                            }
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
                        for (var w = 0; w < this.CubeSpace.GetLength(3); w++)
                        {
                            if (this[x, y, z, w] == CubeState.Active)
                            {
                                occ++;
                            }
                        }
                    }
                }
            }
            return occ;
        }

        public void Print(int z = 0, int w = 0)
        {
            for (var x = 0; x < this.CubeSpace.GetLength(0); x++)
            {
                var line = "";
                for (var y = 0; y < this.CubeSpace.GetLength(1); y++)
                {
                    if (this[x, y, z, w] == CubeState.Active)
                    {
                        line += "#";
                    }
                    if (this[x, y, z, w] == CubeState.Inactive)
                    {
                        line += ".";
                    }
                }
                Log.Verbose("{line}", line);
            }
            Console.WriteLine();
        }

        private static List<(int, int, int, int)> GenerateModifiers
        {
            get
            {
                var output = new List<(int, int, int, int)>();

                for (var x = -1; x < 2; x++)
                {
                    for (var y = -1; y < 2; y++)
                    {
                        for (var z = -1; z < 2; z++)
                        {
                            for (var w = -1; w < 2; w++)
                            {
                                if (x == 0 && y == 0 && z == 0 && w == 0)
                                {
                                    //Skip the home cell
                                    continue;
                                }
                                output.Add((x, y, z, w));
                            }
                        }
                    }
                }

                return output;
            }
        }

        public CubeState this[int x, int y, int z, int w]
        {
            get { return CubeSpace[x, y, z, w]; }

            set { CubeSpace[x, y, z, w] = value; }
        }
    }
}