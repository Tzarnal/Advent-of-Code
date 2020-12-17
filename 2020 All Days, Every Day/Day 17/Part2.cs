using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_17
{
    //https://adventofcode.com/2020/day/17#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Conway Cubes. Part Two."; }

        public void Run()
        {
            //var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<string> input)
        {
            var cCube = new HyperCube(input);

            for (var i = 0; i < 6; i++)
            {
                cCube.Grow(1);
                RunRules(cCube);
            }

            Log.Information("Active Cells {active}", cCube.ActiveCubes());
        }

        public void RunRules(HyperCube cCube)
        {
            var referenceCube = new HyperCube(cCube);
            for (var x = 0; x < cCube.CubeSpace.GetLength(0); x++)
            {
                for (var y = 0; y < cCube.CubeSpace.GetLength(1); y++)
                {
                    for (var z = 0; z < cCube.CubeSpace.GetLength(2); z++)
                    {
                        for (var w = 0; w < cCube.CubeSpace.GetLength(3); w++)
                        {
                            var adj = referenceCube.AdjacentActiveCells(x, y, z, w);
                            if (referenceCube[x, y, z, w] == CubeState.Active && !(adj == 2 || adj == 3))
                            {
                                cCube[x, y, z, w] = CubeState.Inactive;
                            }

                            if (referenceCube[x, y, z, w] == CubeState.Inactive && adj == 3)
                            {
                                cCube[x, y, z, w] = CubeState.Active;
                            }
                        }
                    }
                }
            }
        }

        private List<string> ParseInput(string filePath)
        {
            return Helpers.ReadStringsFile(filePath);
        }
    }
}