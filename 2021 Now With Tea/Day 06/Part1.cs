using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_06
{
    //https://adventofcode.com/2021/day/06
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Lanternfish. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public static void Solve(List<LanternFish> input, int Days = 80)
        {
            for (var d = 0; d < Days; d++)
            {
                var fishCount = input.Count();
                for (var i = 0; i < fishCount; i++)
                {
                    var fish = input[i];

                    if (fish.Age == 0)
                    {
                        fish.Age = 6;
                        input.Add(new LanternFish { Age = 8 });
                    }
                    else
                    {
                        fish.Age--;
                    }
                }
            }

            Log.Information("After {Days} days there are {TotalFish} Fish.",
                Days, input.Count);
        }

        public static List<LanternFish> ParseInput(string filePath)
        {
            var Fish = new List<LanternFish>();

            var numbers = Helpers.ReadAllIntsFile(filePath);

            Fish = numbers.Select(n => new LanternFish { Age = n }).ToList();

            return Fish;
        }
    }

    public class LanternFish
    {
        public int Age;
        public long Count;
    }
}