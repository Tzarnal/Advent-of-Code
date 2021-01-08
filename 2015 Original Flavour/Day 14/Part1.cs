using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_14
{
    //https://adventofcode.com/2015/day/14
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Reindeer Olympics. Part One."; }

        public void Run()
        {
            var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input, 2503);
        }

        public void Solve(List<(string Name, int speed, int duration, int pause)> input, int RaceDuration = 1000)
        {
            var results = new Dictionary<string, int>();

            foreach (var racer in input)
            {
                var raceTime = racer.duration;
                var restingTime = racer.pause;
                var resting = false;
                var distance = 0;

                for (int i = 0; i < RaceDuration; i++)
                {
                    if (resting)
                    {
                        restingTime--;
                        if (restingTime <= 0)
                        {
                            resting = false;
                            restingTime = racer.pause;
                        }
                    }
                    else
                    {
                        distance += racer.speed;
                        raceTime--;
                        if (raceTime <= 0)
                        {
                            resting = true;
                            raceTime = racer.duration;
                        }
                    }
                }

                results.Add(racer.Name, distance);
            }

            var furthest = results.OrderByDescending(r => r.Value).First();

            Log.Information("The fastest Reindeer was {n} who scored {d}", furthest.Key, furthest.Value);
        }

        public static List<(string Name, int speed, int duration, int pause)> ParseInput(string filePath)
        {
            var lines = Helpers.ReadStringsFile(filePath);

            var racers = new List<(string Name, int speed, int duration, int pause)>();

            foreach (var line in lines)
            {
                var racer = line.Extract<(string, int, int, int)>(@"(.+) can fly (\d+) km\/s for (\d+) seconds, but then must rest for (\d+) seconds\.");
                racers.Add(racer);
            }

            return racers;
        }
    }
}