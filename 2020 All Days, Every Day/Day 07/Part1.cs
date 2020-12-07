using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;

namespace Day_07
{
    //https://adventofcode.com/2020/day/7
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Handy Haversacks. Part One."; }

        private Dictionary<string, Dictionary<string, int>> _bagRules;
        private Dictionary<string, List<string>> _reverseList;

        public void Run()
        {
            //_bagRules = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve("shiny gold");

            _bagRules = ParseInput($"Day {Dayname}/input.txt");
            Solve("shiny gold");
        }

        public void Solve(string BagIHave)
        {
            _reverseList = new Dictionary<string, List<string>>();
            var eventuallyContainTarget = new HashSet<string>();

            foreach (var bagRule in _bagRules)
            {
                if (bagRule.Value.Count != 0)
                {
                    foreach (var bagColour in bagRule.Value)
                    {
                        if (_reverseList.ContainsKey(bagColour.Key))
                        {
                            if (!_reverseList[bagColour.Key].Contains(bagRule.Key))
                            {
                                _reverseList[bagColour.Key].Add(bagRule.Key);
                            }
                        }
                        else
                        {
                            _reverseList.Add(bagColour.Key, new List<string> { bagRule.Key });
                        }
                    }
                }
            }

            var awnser = Dive(eventuallyContainTarget, BagIHave);
            Log.Information("Found {count} bags that can eventually contain {BagIHave}", awnser.Count(), BagIHave);
        }

        public HashSet<string> Dive(HashSet<string> FoundBags, string BagColour)
        {
            if (_reverseList.ContainsKey(BagColour))
            {
                var newOptions = _reverseList[BagColour];
                foreach (var option in newOptions)
                {
                    FoundBags.Add(option);
                    FoundBags.UnionWith(Dive(FoundBags, option));
                }
            }

            return FoundBags;
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