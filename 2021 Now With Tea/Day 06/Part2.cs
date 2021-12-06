using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_06
{
    //https://adventofcode.com/2021/day/06#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Lanternfish. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input, 256);
        }

        public static void Solve(List<LanternFish> input, int Days = 80)
        {
            var AllFish = new LanternFish[10];
            for (int i = 0; i < AllFish.Length; i++)
            {
                AllFish[i] = new LanternFish();
            }

            foreach (var Fish in input)
            {
                AllFish[Fish.Age].Count++;
            }

            for (var d = 0; d < Days; d++)
            {
                for (var i = 0; i < AllFish.Length; i++)
                {
                    if (i == 0)
                    {
                        AllFish[9].Count += AllFish[0].Count; //spawned fish
                        AllFish[7].Count += AllFish[0].Count; //parent fish
                        AllFish[0].Count = 0;
                    }
                    else
                    {
                        AllFish[i - 1].Count += AllFish[i].Count;
                        AllFish[i].Count = 0;
                    }
                }
            }
            var fishCount = AllFish.Sum(f => f.Count);
            Log.Information("After {Days} days there are {TotalFish} Fish.",
                    Days, fishCount);
        }
    }
}