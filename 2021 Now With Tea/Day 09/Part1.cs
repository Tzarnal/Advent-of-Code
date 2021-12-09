using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_09
{
    //https://adventofcode.com/2021/day/09
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Smoke Basin. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(TextGrid input)
        {
            input.AdjacentDirections = new List<(int x, int y)>
            {
                TextGrid.Up,
                TextGrid.Down,
                TextGrid.Left,
                TextGrid.Right
            };

            int totalRiskLevel = 0;

            foreach (var cell in input.AllCells())
            {
                var originalValue = int.Parse(cell.value);
                var comparisons = input.AdjacentCellsList(cell.x, cell.y)
                    .Select(int.Parse)
                    .Select(c => c > originalValue);

                //low point
                if (comparisons.All(b => b))
                {
                    totalRiskLevel += originalValue + 1;
                }
            }

            Log.Information("Sum of all Risk Values: {totalRiskLevel}", totalRiskLevel);
        }

        public static TextGrid ParseInput(string filePath)
        {
            return new TextGrid(Helpers.ReadStringsFile(filePath));
        }
    }
}