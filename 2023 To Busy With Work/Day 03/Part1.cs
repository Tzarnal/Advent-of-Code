using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_03
{
    //https://adventofcode.com/2023/day/03
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Gear Ratios. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(TextGrid Grid)
        {
            var validParts = new List<int>();

            var currentNr = "";
            var isValidPart = false;

            for (var x = 0; x < Grid.Width; x++)
            {
                for (var y = 0; y < Grid.Heigth; y++)
                {
                    var cell = Grid[x, y];

                    if (Helpers.IsSymbols(cell))
                    {
                        if (isValidPart)
                        {
                            validParts.Add(int.Parse(currentNr));
                        }

                        currentNr = "";
                        isValidPart = false;
                    }

                    if (Helpers.IsDigits(cell))
                    {
                        currentNr += cell;
                        var adjacent = Grid.AdjacentCellsList(x, y);
                        var adjacentString = Helpers.StringEnumerableToString(adjacent).Replace(".", "");

                        if (Helpers.HasSymbols(adjacentString))
                        {
                            isValidPart = true;
                        }
                    }
                }

                if (isValidPart)
                {
                    validParts.Add(int.Parse(currentNr));
                }

                currentNr = "";
                isValidPart = false;
            }

            var partSum = validParts.Sum();

            Log.Information("There are {count} parts with a sum of {sum}.", validParts.Count, partSum);
        }

        public static TextGrid ParseInput(string filePath)
        {
            var grid = new TextGrid(File.ReadAllLines(filePath).ToList());
            return grid;
        }
    }
}