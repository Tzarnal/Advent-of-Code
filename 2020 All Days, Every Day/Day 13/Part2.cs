using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;

namespace Day_13
{
    //https://adventofcode.com/2020/day/13#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Shuttle Search. Part Two."; }

        public void Run()
        {
            var (DepartureTime, BusTimes) = ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(BusTimes, 0);

            (DepartureTime, BusTimes) = ParseInput($"Day {Dayname}/input.txt");
            Solve(BusTimes, 100000000000000);
        }

        public void Solve(List<int> BusTimes, long earliest)
        {
            var step = BusTimes[0];
            for (long departureTime = earliest; departureTime < long.MaxValue; departureTime += step)
            {
                var successes = new List<long>();

                for (var offset = 0; offset < BusTimes.Count; offset++)
                {
                    var busTime = BusTimes[offset];

                    if (busTime == 0)
                    {
                        successes.Add(departureTime + offset);
                        continue;
                    }

                    if ((departureTime + offset) % busTime == 0)
                    {
                        successes.Add(departureTime + offset);
                    }
                    else
                    {
                        break;
                    }
                }

                if (successes.Count == BusTimes.Count)
                {
                    Log.Information("Found earliest departure time {departureTime}. {@successes}", departureTime, successes);
                    return;
                }
            }

            Log.Information("A Solution Can Be Found.");
        }

        private (int, List<int>) ParseInput(string filePath)
        {
            var input = Helpers.ReadStringsFile(filePath);

            var departureTime = int.Parse(input[0]);
            var busTimes = new List<int>();

            var numbers = input[1].Split(",");
            foreach (var n in numbers)
            {
                var i = 0;
                if (int.TryParse(n, out i))
                {
                    busTimes.Add(i);
                }
                else
                {
                    busTimes.Add(0);
                }
            }

            return (departureTime, busTimes);
        }
    }
}