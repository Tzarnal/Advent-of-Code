using Advent;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_11
{
    //https://adventofcode.com/2020/day/11#part2
    public class Part2TextGrid : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}:  Seating System (TextGrid). Part Two."; }

        public void Run()
        {
            //var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");

            Solve(inputList);
        }

        public void Solve(List<string> input)
        {
            var ferry = new TextGridFerry(input);
            TextGridFerry referenceFerry;

            var runCount = 0;
            do
            {
                referenceFerry = new TextGridFerry(ferry);

                RunRules(ferry);
                runCount++;
            } while (!ferry.Equals(referenceFerry));

            Log.Information("Stable after {runCount}. With {occ} seats occupied", runCount, ferry.OccupiedSeats());
        }

        public void RunRules(TextGridFerry ferry)
        {
            var referenceFerry = new TextGridFerry(ferry);
            for (var x = 0; x < ferry.Grid.GetLength(0); x++)
            {
                for (var y = 0; y < ferry.Grid.GetLength(1); y++)
                {
                    if (referenceFerry[x, y] == ".")
                    {
                        continue;
                    }

                    var adjacent = referenceFerry.AdjacentOccupiedSeatsVector(x, y);
                    if (referenceFerry[x, y] == "L" && adjacent == 0)
                    {
                        ferry[x, y] = "#";
                    }

                    if (referenceFerry[x, y] == "#" && adjacent >= 5)
                    {
                        ferry[x, y] = "L";
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