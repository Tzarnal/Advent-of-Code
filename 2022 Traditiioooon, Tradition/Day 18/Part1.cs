using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_18
{
    //https://adventofcode.com/2022/day/18
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Boiling Boulders. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }
        public void Solve(List<(int x, int y, int z)> input)
        {
            var cubes = new Dictionary<(int x, int y, int z), int>();
            foreach (var line in input)
            {
                cubes.Add(line, 0);
            }

            foreach (var cube in cubes.Keys)
            {
                var cubeSides = 6;

                for (int xOffset = -1; xOffset <= 1; xOffset += 2)
                {
                    if (cubes.ContainsKey((cube.x + xOffset, cube.y, cube.z)))
                    {
                        cubeSides--;
                    }
                }

                for (int yOffset = -1; yOffset <= 1; yOffset += 2)
                {
                    if (cubes.ContainsKey((cube.x, cube.y + yOffset, cube.z)))
                    {
                        cubeSides--;
                    }
                }

                for (int zOffset = -1; zOffset <= 1; zOffset += 2)
                {
                    if (cubes.ContainsKey((cube.x, cube.y, cube.z + zOffset)))
                    {
                        cubeSides--;
                    }
                }

                cubes[cube] = cubeSides;
            }

            var surfaceArea = cubes.Sum(c => c.Value);
            Log.Information("The surface area of the scanned lava droptlet is {surfaceArea}.", surfaceArea);
        }

        public static List<(int x, int y, int z)> ParseInput(string filePath)
        {
            var cubes = new List<(int x, int y, int z)>();

            foreach (var line in File.ReadLines(filePath))
            {
                var values = Helpers.ReadAllIntsStrings(line);
                cubes.Add((values[0], values[1], values[2]));
            }

            return cubes;
        }
    }
}