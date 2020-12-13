using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_13
{
    //https://adventofcode.com/2020/day/13
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Shuttle Search. Part One."; }

        public void Run()
        {
            var (DepartureTime, BusTimes) = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(DepartureTime, BusTimes);

            (DepartureTime, BusTimes) = ParseInput($"Day {Dayname}/input.txt");
            Solve(DepartureTime, BusTimes);
        }

        public void Solve(int DepartureTime, List<int> BusTimes)
        {
            for (int departureTime = DepartureTime; departureTime < int.MaxValue; departureTime++)
            {
                foreach (var busTime in BusTimes)
                {
                    if (departureTime % busTime == 0)
                    {
                        var awnser = (departureTime - DepartureTime) * busTime;

                        Log.Information("Can depart at {departureTime} on bus {busTime}. Awnser: {awnser}",
                            departureTime, busTime, awnser);
                        return;
                    }
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
            }

            return (departureTime, busTimes);
        }
    }
}