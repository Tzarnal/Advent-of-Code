using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_12
{
    //https://adventofcode.com/2021/day/12
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Passage Pathing. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(Dictionary<string, List<string>> input)
        {
            var pathCount = Traverse(new List<string> { "start" }, input);

            Log.Information("There are {pathCount} paths.", pathCount);
        }

        public int Traverse(List<string> path, Dictionary<string, List<string>> graph)
        {
            var lastNode = path.Last();

            if (lastNode == "end")
            {
                return 1;
            }
            else
            {
                var count = 0;

                foreach (var neighbour in graph[lastNode].
                    Where(n =>
                        n.IsUpper() ||
                        !path.Contains(n)
                    ))
                {
                    var newPath = new List<string>(path)
                    {
                        neighbour
                    };
                    count += Traverse(newPath, graph);
                }
                return count;
            }
        }

        public static Dictionary<string, List<string>> ParseInput(string filePath)
        {
            var input = File.ReadAllLines(filePath).Select(l => l.Split('-'));

            var connections = new Dictionary<string, List<string>>();

            foreach (var line in input)
            {
                if (!connections.ContainsKey(line[0]))
                {
                    connections[line[0]] = new List<string>();
                }

                if (!connections.ContainsKey(line[1]))
                {
                    connections[line[1]] = new List<string>();
                }
                connections[line[0]].Add(line[1]);
                connections[line[1]].Add(line[0]);
            }

            return connections;
        }
    }
}