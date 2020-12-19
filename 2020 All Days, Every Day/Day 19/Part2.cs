using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;

using Advent;
using RegExtract;
using System.Text.RegularExpressions;

namespace Day_19
{
    //https://adventofcode.com/2020/day/19#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Monster Messages. Part Two."; }

        public void Run()
        {
            //var (rules, messages) = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(rules, messages);

            var (rules, messages) = ParseInput($"Day {Dayname}/input2.txt");
            Solve(rules, messages);
        }

        public void Solve(Dictionary<int, string> RuleInput, List<string> Messages)
        {
            //Before we start fix the a and b to not include "
            for (var i = 0; i < RuleInput.Count; i++)
            {
                if (RuleInput[i].Contains("\""))
                {
                    RuleInput[i] = RuleInput[i].Replace("\"", "");
                }
            }

            //Go over the rules, grab the first one, make sure its not "special" and fold it into the parent rules
            while (RuleInput.Count > 5) //Magic nubmer but fix the problem you have right.
            {
                //Ignore the special recursion dependant rules
                //42 and 31 are the rules that the recursion rules of 8 and 11, so we treat them as special
                //Because 42 and 31 do not get folded, 8 and 11 do not get folded
                //Because 8 and 11 do not get folden 0 does not get folded.
                //Which gives us the magic number of 5 for aborting the loop
                var nextRule = RuleInput.FirstOrDefault(r =>
                    !Regex.IsMatch(r.Value, @"\d+")
                    && r.Key != 42
                    && r.Key != 31);

                //Find all the remaining parent rules and fold this rule into it
                foreach (var r in RuleInput)
                {
                    RuleInput[r.Key] = Regex.Replace(
                        RuleInput[r.Key], @"\b" + nextRule.Key + @"\b",
                        "(" + nextRule.Value + ")");
                }

                //get rid of the folded in rule
                RuleInput.Remove(nextRule.Key);
            }

            //Spaces kept things nicely seperated during merging but are meaningful in regex
            //so now they need to go because our input strings have no spaces
            RuleInput[31] = RuleInput[31].Replace(" ", "");
            RuleInput[42] = RuleInput[42].Replace(" ", "");

            //Assemble the monster
            var RegexCrimes = new Regex("^" + RuleInput[0].Replace("8", "(" + RuleInput[42] + ")+").Replace("11", "(?<A>" + RuleInput[42] + ")+(?<-A> " + RuleInput[31] + ")+").Replace(" ", "") + "$");

            //Count it all up
            var count = Messages.Count(m => RegexCrimes.IsMatch(m));
            Log.Information("After a lot of REGEX crimes found {count} correct messages.", count);
        }

        private (Dictionary<int, string> Rules, List<string> Messages) ParseInput(string filePath)
        {
            var input = File.ReadAllText(filePath);

            var splitInput = input.Split("\r\n\r\n");

            var rules = new Dictionary<int, string>();
            var messages = splitInput[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in splitInput[0].Split("\r\n"))
            {
                var (index, rule) = line.Extract<(int, string)>(@"(\d+): (.+)");

                rules.Add(index, rule);
            }

            return (rules, messages.ToList());
        }
    }
}