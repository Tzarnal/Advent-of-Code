using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_19
{
    //https://adventofcode.com/2020/day/19
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Monster Messages. Part One."; }

        public void Run()
        {
            //var (rules, messages) = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(rules, messages);

            //var (rules, messages) = ParseInput($"Day {Dayname}/input.txt");
            //Solve(rules, messages);
        }

        public void Solve(Dictionary<int, string> RuleInput, List<string> Messages)
        {
            var Rules = new Dictionary<int, IMessageRule>();

            foreach (var ruleInput in RuleInput)
            {
                if (ruleInput.Value.Contains("|"))
                {
                    Rules[ruleInput.Key] = new MessageRuleOr(ruleInput.Key, ruleInput.Value);
                }
                else if (ruleInput.Value.Contains("\""))
                {
                    Rules[ruleInput.Key] = new MessageRuleString(ruleInput.Key, ruleInput.Value);
                }
                else
                {
                    Rules[ruleInput.Key] = new MessageRuleAnd(ruleInput.Key, ruleInput.Value);
                }
            }

            while (!Rules.All(r => r.Value.Ready))
            {
                foreach (var rule in Rules.Where(r => !r.Value.Ready))
                {
                    rule.Value.Prepare(Rules);
                }
            }

            var correctMessages = 0;
            foreach (var message in Messages)
            {
                if (Rules[0].Matches(message))
                {
                    correctMessages++;
                }
            }

            Log.Information("In  {count} Messages {correctMessages} matched.",
                Messages.Count, correctMessages);
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