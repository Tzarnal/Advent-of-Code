using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;

namespace Day_11
{
    //https://adventofcode.com/2020/day/11#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}:  Seating System. Part Two."; }

        public void Run()
        {
            //var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<string> input)
        {
            var ferry = new Ferry(input);
            Ferry referenceFerry;

            var runCount = 0;

            do
            {
                referenceFerry = new Ferry(ferry);

                RunRules(ferry);
                runCount++;
            } while (!ferry.IsTheSame(referenceFerry));

            Log.Information("Stable after {runCount}. With {occ} seats occupied", runCount, ferry.OccypiedSeats());
        }

        public void RunRules(Ferry ferry)
        {
            var referenceFerry = new Ferry(ferry);
            for (var x = 0; x < ferry.WaitingArea.GetLength(0); x++)
            {
                for (var y = 0; y < ferry.WaitingArea.GetLength(1); y++)
                {
                    if (referenceFerry[x, y] == SeatState.Ground)
                    {
                        continue;
                    }

                    var adjacent = referenceFerry.AdjacentOccupiedSeatsVector(x, y);
                    if (referenceFerry[x, y] == SeatState.Open && adjacent == 0)
                    {
                        ferry[x, y] = SeatState.Occupied;
                    }

                    if (referenceFerry[x, y] == SeatState.Occupied && adjacent >= 5)
                    {
                        ferry[x, y] = SeatState.Open;
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