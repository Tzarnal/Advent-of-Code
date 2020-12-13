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
            var tests = ParseTestInput($"Day {Dayname}/inputTestPart2.txt");
            foreach (var test in tests)
            {
                Solve(test.Item2, test.Item1);
            }

            var (DepartureTime, BusTimes) = ParseInput($"Day {Dayname}/input.txt");
            Solve(BusTimes, 0);
        }

        //This is ultimatly a Chinese Remainder Theory problem with some little wrinkles in the input
        //https://en.wikipedia.org/wiki/Chinese_remainder_theorem
        //https://rosettacode.org/wiki/Chinese_remainder_theorem#C.23
        public void Solve(List<int> BusTimes, long testTime)
        {
            var constraints = new List<(int i, int b)>();
            long N = 1; //Product of all bus id's ( not 0 )

            //Go over all the given busTimes, calculate the constraint for it, calculate the product
            for (var offset = 0; offset < BusTimes.Count; offset++)
            {
                var busTime = BusTimes[offset];

                //Skip the 0's, since they are "permissive" we need their index offsets but not the actual number
                if (busTime == 0)
                {
                    continue;
                }

                var i = offset % busTime;
                var constraint = (busTime - i) % busTime;

                //add to the total N product
                N *= busTime;

                constraints.Add((constraint, busTime)); //Packing constraints now minus the x's
            }

            long awnser = 0;

            foreach (var c in constraints)
            {
                var (constraint, busTime) = c; //just unpacking again;

                long nBustime = N / busTime; //N for a given I or in this case bustime.

                var mInverse = CRMHelper.ModInverse(nBustime, busTime);

                awnser += constraint * mInverse * nBustime;
            }

            //N is the product of all the bustimes so % N is essentially the overlap between all the bustimes we are looking for
            awnser %= N;

            if (testTime == 0)
            {
                Log.Information("The awnser {awnser}.", awnser);
            }
            else
            {
                var passed = awnser == testTime;
                Log.Information("Test {awnser}, looking for {testTime}. Passed [{passed}].", awnser, testTime, passed);
            }
        }

        public void BruteSolve(List<int> BusTimes, long earliest, long testTime)
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
                    Log.Information("Found earliest departure time {departureTime}. Correct time {testTime}.",
                        departureTime, testTime, successes);
                    return;
                }
            }

            Log.Information("A Solution Can Be Found.");
        }

        private List<(long, List<int>)> ParseTestInput(string filePath)
        {
            var output = new List<(long, List<int>)>();
            var input = Helpers.ReadStringsFile(filePath);

            foreach (var line in input)
            {
                var chunks = line.Split(" : ");

                var testTime = long.Parse(chunks[0]);
                var busTimes = new List<int>();

                var numbers = chunks[1].Split(",");
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

                output.Add((testTime, busTimes));
            }

            return output;
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