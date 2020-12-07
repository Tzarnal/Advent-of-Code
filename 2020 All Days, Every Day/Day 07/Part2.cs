using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;

namespace Day_07
{
    //https://adventofcode.com/2020/day/7#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Handy Haversacks. Part Two."; }

        private Dictionary<string, Dictionary<string, int>> _bagRules;

        public void Run()
        {
            //_bagRules = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve("shiny gold");

            _bagRules = ParseInput($"Day {Dayname}/input.txt");
            Solve("shiny gold");
        }

        public void Solve(string BagIHave)
        {
            ulong awnser = Dive(BagIHave);
            Log.Information("Total needed bags = {awnser}", awnser);
        }

        public ulong Dive(string BagColour)
        {
            ulong bagCount = 0;

            if (_bagRules[BagColour].Count == 0)
            {
                return bagCount;
            }

            foreach (var bagsRule in _bagRules[BagColour])
            {
                bagCount += (ulong)bagsRule.Value;
                bagCount += Dive(bagsRule.Key) * (ulong)bagsRule.Value;
            }

            return bagCount;
        }

        private Dictionary<string, Dictionary<string, int>> ParseInput(string filePath)
        {
            var input = Helpers.ReadStringsFile(filePath);

            var bagRules = new Dictionary<string, Dictionary<string, int>>();

            foreach (var line in input)
            {
                var bagRule = new Dictionary<string, int>();
                var rules = line.Split(",");

                var firstRuleElements = rules[0].Split("contain");
                var firstRuleColour = StripText(firstRuleElements[0]);

                if (firstRuleElements[1] == " no other bags.")
                {
                    bagRules.Add(firstRuleColour, bagRule);
                    continue;
                }

                var firstBagCount = int.Parse(firstRuleElements[1].Trim().Substring(0, 1));
                var firstBagColour = StripText(firstRuleElements[1].Trim().Substring(1));

                bagRule[firstBagColour] = firstBagCount;

                foreach (var ruleElement in rules.Skip(1))
                {
                    var BagCount = int.Parse(ruleElement.Trim().Substring(0, 1));
                    var BagColour = StripText(ruleElement.Trim().Substring(1));

                    bagRule[BagColour] = BagCount;
                }

                bagRules.Add(firstRuleColour, bagRule);
            }

            return bagRules;
        }

        private string StripText(string input)
        {
            input = input.Trim(new char[] { '.', ',' });
            input = input.Replace("bags", "").Replace("bag", "").Trim();

            return input;
        }
    }
}