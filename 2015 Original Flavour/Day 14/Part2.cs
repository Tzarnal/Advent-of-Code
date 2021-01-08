using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_14
{
    //https://adventofcode.com/2015/day/14#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Reindeer Olympics. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input, 2503);
        }

        public static void Solve(List<Racer> Racers, int RaceDuration = 1000)
        {
            for (int i = 0; i < RaceDuration; i++)
            {
                foreach (var racer in Racers)
                {
                    if (racer.IsResting)
                    {
                        racer.CurrentRestingTime--;
                        if (racer.CurrentRestingTime <= 0)
                        {
                            racer.IsResting = false;
                            racer.CurrentRestingTime = racer.RestingTime;
                        }
                    }
                    else
                    {
                        racer.DistanceTraveled += racer.Speed;
                        racer.CurrentRaceTime--;
                        if (racer.CurrentRaceTime <= 0)
                        {
                            racer.IsResting = true;
                            racer.CurrentRaceTime = racer.RaceTime;
                        }
                    }
                }

                var topDistance = Racers.Max(r => r.DistanceTraveled);

                foreach (var racer in Racers)
                {
                    if (racer.DistanceTraveled == topDistance)
                    {
                        racer.Points++;
                    }
                }
            }

            var bestRacer = Racers.OrderByDescending(r => r.Points).First();
            Log.Information("The best scoring Reindeer was {n} who scored {p}",
                bestRacer.Name, bestRacer.Points);
        }

        public static List<Racer> ParseInput(string filePath)
        {
            var lines = Helpers.ReadStringsFile(filePath);

            var racers = new List<Racer>();

            foreach (var line in lines)
            {
                var (name, speed, raceTime, restTime) = line.Extract<(string name, int speed, int raceTime, int restTime)>(@"(.+) can fly (\d+) km\/s for (\d+) seconds, but then must rest for (\d+) seconds\.");
                var racer = new Racer
                {
                    Name = name,
                    Speed = speed,
                    RaceTime = raceTime,
                    RestingTime = restTime
                };
                racer.Init();
                racers.Add(racer);
            }

            return racers;
        }
    }

    public class Racer
    {
        public string Name;

        public bool IsResting;

        public int DistanceTraveled;
        public int Points;

        public int CurrentRaceTime;
        public int CurrentRestingTime;
        public int RaceTime;
        public int RestingTime;

        public int Speed;

        public void Init()
        {
            CurrentRaceTime = RaceTime;
            CurrentRestingTime = RestingTime;
        }
    }
}