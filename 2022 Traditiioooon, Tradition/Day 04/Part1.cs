using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_04
{
    //https://adventofcode.com/2022/day/4
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Camp Cleanup. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<(IEnumerable<int> Left, IEnumerable<int> Right)> input)
        {
            var overlaps = 0;
            
            foreach(var pair in input)
            {
                if(Contains(pair.Left, pair.Right))
                {
                   overlaps++;                    
                }
                else if(Contains(pair.Right, pair.Left))
                {
                    overlaps++;
                }
            }
            
            Log.Information("Found {overlaps} assingment pairs where one range fully contains the other.", overlaps);
        }


        public static bool Contains(IEnumerable<int> needle, IEnumerable<int> haystack)
        {
            var overlap = haystack.Intersect(needle);

            if(needle.Count() == overlap.Count())
            {
                return true;
            }
            
            return false;
        }

        public static List<(IEnumerable<int> Left,IEnumerable<int> Right)> ParseInput(string filePath)
        {
            var input = Helpers.ReadStringsFile(filePath);

            var elfPairs = new List<(IEnumerable<int> Left, IEnumerable<int> Right)>();

            foreach (var line in input)
            {
                var (L1, L2, R1, R2) = line
                    .Extract<(int, int, int, int)>("(\\d+)-(\\d+),(\\d+)-(\\d+)");

                var left = Enumerable.Range(L1, L2-L1+1);
                var right = Enumerable.Range(R1, R2-R1+1);

                elfPairs.Add((left, right));
            }

            return elfPairs;

        }
    }
}