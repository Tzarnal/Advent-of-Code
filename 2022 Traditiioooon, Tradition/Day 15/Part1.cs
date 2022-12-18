using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;
using Advent.AoCLib;


namespace Day_15
{
    //https://adventofcode.com/2022/day/15
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Beacon Exclusion Zone. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput, 10);

            var input = ParseInput($"Day {Dayname}/input.txt");
            //Solve(input, 2000000, 1000000);
        }

        public void Solve(List<(IntVector2 Sensor, IntVector2 Beacon)> input, int testRow, int margin = 100)
        {
            var maxX = 1;
            var minX = 1;

            var excluded = 0;

            foreach (var (Sensor, Beacon) in input)
            {
                maxX = Math.Max(Math.Max(Sensor.X, Beacon.X), maxX);
                minX = Math.Min(Math.Min(Sensor.X, Beacon.X), minX);
            }

            maxX += margin;
            minX -= margin;

            for (int x = minX; x <= maxX; x++)
            {
                var isExcluded = false;
                var point = new IntVector2(x, testRow);

                foreach (var pair in input)
                {
                    if (InsideExclusion(pair.Sensor, pair.Beacon, point))
                    {
                        isExcluded = true;
                        break;
                    }
                }

                if (isExcluded)
                {
                    excluded++;
                }
            }

            var knownDevices = new HashSet<IntVector2>();
            foreach (var (Sensor, Beacon) in input)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    var point = new IntVector2(x, testRow);

                    if (Sensor == point)
                    {
                        knownDevices.Add(point);
                    }

                    if (Beacon == point)
                    {
                        knownDevices.Add(point);
                    }
                }
            }

            excluded -= knownDevices.Count;

            Log.Information("Found {excludedspots} positions that cannot contain a beacon.", excluded);
        }

        public static bool InsideExclusion(IntVector2 Sensor, IntVector2 Beacon, IntVector2 Location)
        {
            var exclusionDistance = Math.Abs(Sensor.X - Beacon.X) + Math.Abs(Sensor.Y - Beacon.Y);
            var locationDistance = Math.Abs(Sensor.X - Location.X) + Math.Abs(Sensor.Y - Location.Y);

            return exclusionDistance >= locationDistance;
        }

        public static List<(IntVector2 Sensor, IntVector2 Beacon)> ParseInput(string filePath)
        {
            var output = new List<(IntVector2 Sensor, IntVector2 Beacon)>();

            foreach (var line in File.ReadAllLines(filePath))
            {
                var (sensorX, sensorY, beaconX, beaconY) = line.Extract<(int, int, int, int)>(
                    "Sensor at x=(-?\\d+), y=(-?\\d+): closest beacon is at x=(-?\\d+), y=(-?\\d+)");

                output.Add((new IntVector2(sensorX, sensorY), new IntVector2(beaconX, beaconY)));
            }

            return output;
        }
    }
}