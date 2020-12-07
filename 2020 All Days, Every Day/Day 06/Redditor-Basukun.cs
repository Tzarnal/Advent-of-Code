using System;
using System.IO;
using System.Collections.Generic;
using Serilog;
using Advent;
using System.Linq;

namespace Day_06
{
    //https://adventofcode.com/2020/day/6#part2
    public class RedditorBasukun : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: [Redditor: Basukun] Custom Customs. Part Two."; }

        public void Run()
        {
            var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            var total = PartTwo(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            total = PartTwo(inputList);
        }

        public static int PartTwo(List<List<string>> list)
        {
            int totalYesses = 0;
            int groupcount = 0;
            int formcount = 0;

            foreach (var group in list)
            {
                groupcount++;
                List<char> groupYesses = new List<char>();
                List<char> currentYesses = new List<char>();

                foreach (var person in group)
                {
                    formcount++;
                    var personYesses = person.ToCharArray().ToList();
                    if (group.First() == person && groupYesses.Count == 0)
                    {
                        foreach (var yes in personYesses)
                        {
                            currentYesses.Add(yes);
                            groupYesses = new List<char>(currentYesses);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < groupYesses.Count; i++)
                        {
                            if (!personYesses.Any(t => t.Equals(groupYesses[i])))
                            {
                                currentYesses.Remove(groupYesses[i]);
                            }
                        }
                        groupYesses = new List<char>(currentYesses);
                    }
                }
                totalYesses += groupYesses.Count();
                //Log.Information("After {groupcount} groups and {formcount} forms the total is: {total}.", groupcount, formcount, totalYesses);
            }

            Log.Information("After {groupcount} groups and {formcount} forms the total is: {total}.", groupcount, formcount, totalYesses);

            return totalYesses;
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