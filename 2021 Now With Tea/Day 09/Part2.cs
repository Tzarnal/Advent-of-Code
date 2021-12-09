using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_09
{
    //https://adventofcode.com/2021/day/09#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Smoke Basin. Part Two."; }

        public void Run()
        {
            var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
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

            var basinOriginPoints = new List<(int x, int y)>();

            foreach (var cell in input.AllCells())
            {
                var originalValue = int.Parse(cell.value);
                var comparisons = input.AdjacentCellsList(cell.x, cell.y)
                    .Select(int.Parse)
                    .Select(c => c > originalValue);

                if (comparisons.All(b => b))
                {
                    basinOriginPoints.Add((cell.x, cell.y));
                }
            }

            var allBasinCells = new List<(int x, int y)>();
            var basinSizes = new List<int>();
            foreach (var basinOrigin in basinOriginPoints)
            {
                var currentBasinValues = new List<(int x, int y)>();
                var basinCandidates = new List<(int x, int y)> { (basinOrigin.x, basinOrigin.y) };

                while (basinCandidates.Count > 0)
                {
                    var currentCell = basinCandidates.First();
                    basinCandidates.Remove(currentCell);

                    allBasinCells.Add(currentCell);
                    currentBasinValues.Add(currentCell);

                    foreach (var candidateCell in input.AdjacentCellsEnumerate(currentCell.x, currentCell.y))
                    {
                        if (!allBasinCells.Contains((candidateCell.x, candidateCell.y)) &&
                            candidateCell.value != "9")
                        {
                            basinCandidates.Add((candidateCell.x, candidateCell.y));
                        }
                    }
                }

                basinSizes.Add(currentBasinValues.Distinct().Count());
            }

            var topThreeBasins = basinSizes.OrderByDescending(b => b).Take(3).Aggregate((total, next) => total * next); ;

            Log.Information("Product of three largest basins: {topThreeBasins}", topThreeBasins);
        }
    }
}