using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;
using Advent.AoCLib;
using System.Drawing;

namespace Day_15
{
    //https://adventofcode.com/2022/day/15#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Beacon Exclusion Zone. Part Two."; }

        public void Run()
        {
            var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinput, 0, 20);

            //Things I've tried
            // 1: Naive aproach, too slow, thought it would be, but I often know its going to be too slow but then i try it anyway
            // 2: Store all known scanned squares, didn't even get to checking the squares, these scan ranges are too big. would take gigabytes to store
            // 3: Iterate over the Sensors and check in a square around them. too slow
            // 4: Iterate over the Sensors and check in a square around them but exclude things inside the manhatten distance. too slow
            
            //What Worked
            //5: Generate only the perimeter squares (manhatten distance of the beacon paired with the sensor + 1) and check those
            
            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input, 0, 4000000);
        }

        public void Solve(List<(IntVector2 Sensor, IntVector2 Beacon)> input, int minDistance, int maxDistance)
        {
            var distressBeacons = new HashSet<IntVector2>();

            var knownDevices = new HashSet<IntVector2>();
            foreach (var (Sensor, Beacon) in input)
            {
                //Exclude the actual Devices themselves
                knownDevices.Add(Sensor);
                knownDevices.Add(Beacon);
            }

            foreach (var (Sensor, Beacon) in input)
            {
                var distance = Math.Abs(Sensor.X - Beacon.X) + Math.Abs(Sensor.Y - Beacon.Y) +1;
                var perimeter = new List<IntVector2>();
                for (int i=0; i<distance; i++)
                {
                    perimeter.Add(new IntVector2(Sensor.X + i, Sensor.Y - distance+i));
                    perimeter.Add(new IntVector2(Sensor.X + distance - i, Sensor.Y + i));
                    perimeter.Add(new IntVector2(Sensor.X - i, Sensor.Y + distance - i));
                    perimeter.Add(new IntVector2(Sensor.X - distance + i, Sensor.Y - i));
                }

                
                foreach(var point in perimeter)
                {
                    var pointExcluded = false;

                    if (point.X >= minDistance && point.X <= maxDistance && point.Y >= minDistance && point.Y <= maxDistance)
                    {
                        //Don't bother if its a device location
                        if (knownDevices.Contains(point))
                        {
                            pointExcluded = true;
                        }

                        foreach (var pair in input)
                        {
                            if (Part1.InsideExclusion(pair.Sensor, pair.Beacon, point))
                            {
                                pointExcluded = true;
                                break;
                            }
                        }

                        if (!pointExcluded)
                        {
                            distressBeacons.Add(point);
                            break;
                        }
                    }

                    if(distressBeacons.Count > 0)
                    {
                        break;
                    }
                }
            }

            double tFrequency = distressBeacons.First().X;
            tFrequency = (tFrequency * 4000000) + distressBeacons.First().Y;
            Log.Information("Found the distress beacon, its tuning frequency is {t}.", tFrequency);

        }
    }
}