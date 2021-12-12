using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_12
{
    //https://adventofcode.com/2021/day/12#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Passage Pathing. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(Dictionary<string, List<string>> input)
        {
            var pathCount = Traverse(new List<string> { "start" }, input, false);

            Log.Information("There are {pathCount} paths.", pathCount);
        }

        public int Traverse(List<string> path, Dictionary<string, List<string>> graph, bool doubleVisit)
        {
            var lastNode = path.Last();

            if (lastNode == "end")
            {
                //Log.Debug("{p}, {double}", path, doubleVisit);
                return 1;
            }
            else
            {
                var count = 0;

                foreach (var neighbour in graph[lastNode].OrderBy(c => c))
                {
                    var cDoubleVisit = doubleVisit;

                    //Never visit start twice
                    if (neighbour == "start")
                        continue;

                    if (neighbour.IsLower() && path.Contains(neighbour))
                    {
                        if (doubleVisit)
                        {
                            continue;
                        }
                        else
                        {
                            cDoubleVisit = true;
                        }
                    }

                    var newPath = new List<string>(path)
                    {
                        neighbour
                    };

                    count += Traverse(newPath, graph, cDoubleVisit);
                }

                return count;
            }
        }
    }
}