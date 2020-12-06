using System;
using System.IO;
using System.Collections.Generic;
using Serilog;
using Advent;
using System.Linq;

namespace Day_06
{
    //https://adventofcode.com/2020/day/6#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Custom Customs. Part Two."; }

        public void Run()
        {
            //var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<List<string>> input)
        {
            var commonAwnsersCollection = new List<string>();
            var formCount = 0;

            foreach (var awnserCollection in input)
            {
                string commonAwnsers = "";

                foreach (var line in awnserCollection)
                {
                    formCount++;
                    foreach (var c in line)
                    {
                        if (commonAwnsers.Contains(c))
                        {
                            continue;
                        }

                        var commonCount = awnserCollection.Where(a => a.Contains(c)).Count();
                        if (commonCount == awnserCollection.Count)
                        {
                            commonAwnsers += c;
                        }
                    }
                }
                commonAwnsersCollection.Add(commonAwnsers);
            }

            long total = 0;
            foreach (var awnserCollection in commonAwnsersCollection)
            {
                total += awnserCollection.Length;
            }

            Log.Information("After {input} groups and {formCount} forms the total is: {total}.", input.Count, formCount, total);
        }

        private List<List<string>> ParseInput(string filePath)
        {
            var data = File.ReadAllText(filePath);
            var awnserCollections = new List<List<string>>();

            var entries = data.Trim().Split("\r\n\r\n");

            foreach (var entry in entries)
            {
                var awnserCollection = entry.Split(Environment.NewLine);

                awnserCollections.Add(new List<string>(awnserCollection));
            }

            return awnserCollections;
        }
    }
}