using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_03
{
    //https://adventofcode.com/2023/day/03#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Gear Ratios. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(TextGrid Grid)
        {
            var gears = Grid.FindString("*");

            var gearRatios = new List<int>();
            foreach (var gear in gears)
            {
                var numbers = FindNumbersInLine(Grid[gear.x - 1]);
                numbers.AddRange(FindNumbersInLine(Grid[gear.x]));
                numbers.AddRange(FindNumbersInLine(Grid[gear.x + 1]));

                var adjacentNumbers = new HashSet<(int number, List<int> place)>();

                foreach (var number in numbers)
                {
                    foreach (var p in number.place)
                    {
                        var diff = Math.Abs(p - gear.y);
                        if (diff <= 1)
                        {
                            adjacentNumbers.Add(number);
                        }
                    }
                }

                if (adjacentNumbers.Count == 2)
                {
                    var gearRatio = adjacentNumbers.First().number * adjacentNumbers.Last().number;
                    gearRatios.Add(gearRatio);
                }
            }

            var gearRatioSum = gearRatios.Sum();

            Log.Information("The sum of all gear ratios ({c}) is {sum}", gearRatios.Count, gearRatioSum);
        }

        public List<(int number, List<int> place)> FindNumbersInLine(string line)
        {
            var numbers = new List<(int number, List<int> place)>();

            var number = "";
            var locations = new List<int>();

            for (int i = 0; i < line.Length; i++)
            {
                var c = line[i];

                if (char.IsNumber(c))
                {
                    number += c;
                    locations.Add(i);
                }
                else
                {
                    if (!string.IsNullOrEmpty(number))
                    {
                        numbers.Add((int.Parse(number), locations));
                    }

                    number = "";
                    locations = [];
                }
            }

            if (!string.IsNullOrEmpty(number))
            {
                numbers.Add((int.Parse(number), locations));
            }

            number = "";
            locations = [];

            return numbers;
        }
    }
}