using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_18
{
    //https://adventofcode.com/2022/day/18#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Boiling Boulders. Part Two."; }

        public void Run()
        {
            var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<(int x, int y, int z)> input)
        {
            var theVoid = new HashSet<(int x, int y, int z)>();
            var theShape = new HashSet<(int x, int y, int z)>();

            var cubesToCheck = new Queue<(int x, int y, int z)>();
            cubesToCheck.Enqueue((-2, -2, -2)); //Starting cube the check, placed in the negatives to make sure its outide of the shape

            var maxX = input.Max(c => c.x) + 1;
            var maxY = input.Max(c => c.y) + 1;
            var maxZ = input.Max(c => c.z) + 1;

            var shapeSides = 0;
            while (cubesToCheck.Count > 0)
            {
                var cube = cubesToCheck.Dequeue();

                //Already checked this cube
                if (theShape.Contains(cube) || theVoid.Contains(cube))
                {
                    continue;
                }

                //Its in the shape, if its in the shape that means its adjacent edges cannot inform us being on the outside edge
                if (input.Contains(cube))
                {
                    theShape.Add(cube);
                    continue;
                }

                //Its in the empty void
                {
                    theVoid.Add(cube);
                }

                var adjacentCubes = new List<(int x, int y, int z)>();

                //Find all the 6 adjacent spots to check
                for (int xOffset = -1; xOffset <= 1; xOffset += 2)
                {
                    adjacentCubes.Add((cube.x + xOffset, cube.y, cube.z));
                }

                for (int yOffset = -1; yOffset <= 1; yOffset += 2)
                {
                    adjacentCubes.Add((cube.x, cube.y + yOffset, cube.z));
                }

                for (int zOffset = -1; zOffset <= 1; zOffset += 2)
                {
                    adjacentCubes.Add((cube.x, cube.y, cube.z + zOffset));
                }

                foreach (var adjacentCube in adjacentCubes)
                {
                    //Bounds check
                    if (cube.x < -2
                        || cube.x > maxX

                        || cube.y < -2
                        || cube.y > maxY

                        || cube.z < -2
                        || cube.z > maxZ)
                    {
                        continue;
                    }

                    //Adjacent spot is a cube, that means its a surface connected to the outside void
                    if (input.Contains(adjacentCube))
                    {
                        shapeSides++;
                    }

                    //Dont add places that have already been checked
                    if (theShape.Contains(adjacentCube) || theVoid.Contains(adjacentCube))
                    {
                        continue;
                    }

                    //Add remaining items to queue to check
                    cubesToCheck.Enqueue(adjacentCube);
                }
            }

            Log.Information("The exterior surface area of the scanned lava droptlet is {surfaceArea}.", shapeSides);
        }
    }
}