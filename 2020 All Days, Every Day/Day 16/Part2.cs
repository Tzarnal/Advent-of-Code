using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_16
{
    //https://adventofcode.com/2020/day/16#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Ticket Translation. Part Two."; }

        public void Run()
        {
            //var testData = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testData.Rules, testData.MyTicket, testData.NearbyTickets);

            var inputData = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputData.Rules, inputData.MyTicket, inputData.NearbyTickets);
        }

        public void Solve(List<TicketRule> Rules, List<int> MyTicket, List<List<int>> NearbyTickets)
        {
            var validTickets = ValidTickets(Rules, NearbyTickets);
            var rulesValidForFieldNumber = RulesValidForFieldNumber(Rules, validTickets);

            var ruleToField = new Dictionary<int, int>();

            do
            {
                //Find all rules that are valid for only 1 field, add them to a list
                var determinedRulesToField = new Dictionary<int, int>();

                foreach (var ruleValidForFieldNumber in rulesValidForFieldNumber)
                {
                    if (ruleValidForFieldNumber.Value.Count == 1)
                    {
                        var fieldNr = ruleValidForFieldNumber.Value.First();
                        determinedRulesToField[ruleValidForFieldNumber.Key] = fieldNr;
                    }
                }

                //For all the found rules, take their fields out of the remaining rules
                foreach (var determinedRule in determinedRulesToField)
                {
                    //Removed the recently matched fields form the possible fields to consider
                    foreach (var ruleValidForFieldNumber in rulesValidForFieldNumber)
                    {
                        if (ruleValidForFieldNumber.Value.Contains(determinedRule.Value))
                        {
                            ruleValidForFieldNumber.Value.Remove(determinedRule.Value);
                        }
                    }
                }

                //Merged found rules in this iteration with final set
                determinedRulesToField.ToList().ForEach(d => ruleToField.Add(d.Key, d.Value));
            } while (ruleToField.Count < Rules.Count); //Repeat until every rule is matched up with a field

            //My Ticket Data
            long awnser = 1;
            foreach (var rule in ruleToField.OrderBy(r => Rules[r.Key].Name))
            {
                if (Rules[rule.Key].Name.Contains("departure"))
                {
                    awnser *= MyTicket[rule.Value];
                }
                Log.Verbose("{name}: {value}", Rules[rule.Key].Name, MyTicket[rule.Value]);
            }

            Log.Information("The awnser is {awnser}.", awnser
                );
        }

        public Dictionary<int, HashSet<int>> RulesValidForFieldNumber(List<TicketRule> Rules, List<List<int>> Tickets)
        {
            var validForField = new Dictionary<int, HashSet<int>>();

            //loop over all the rules, then match every rule with every "column" of fields
            for (int i = 0; i < Rules.Count; i++)
            {
                validForField[i] = new HashSet<int>();
                var rule = Rules[i];
                for (int f = 0; f < Tickets[0].Count; f++)//column to match against
                {
                    bool matchesAllTickets = true;

                    foreach (var ticket in Tickets) //tickets that we are gettign the column from
                    {
                        var field = ticket[f];

                        if (!(rule.FirstRule.Low <= field && field <= rule.FirstRule.High) &&
                            !(rule.SecondRule.Low <= field && field <= rule.SecondRule.High))
                        {
                            matchesAllTickets = false;
                            break;
                        }
                    }

                    if (matchesAllTickets)
                    {
                        validForField[i].Add(f);
                    }
                }
            }

            return validForField;
        }

        public List<List<int>> ValidTickets(List<TicketRule> Rules, List<List<int>> NearbyTickets)
        {
            var validTickets = new List<List<int>>();

            //Find valid tickets, this is basically part 1
            foreach (var nearbyTicket in NearbyTickets)
            {
                var validTicket = true;
                foreach (var field in nearbyTicket)
                {
                    var invalidForRules = 0;
                    foreach (var rule in Rules)
                    {
                        if (!(rule.FirstRule.Low <= field && field <= rule.FirstRule.High) &&
                            !(rule.SecondRule.Low <= field && field <= rule.SecondRule.High))
                        {
                            invalidForRules++;
                        }
                    }

                    if (invalidForRules >= Rules.Count)
                    {
                        validTicket = false;
                    }
                }
                if (validTicket)
                {
                    validTickets.Add(nearbyTicket);
                }
            }

            return validTickets;
        }

        private (List<TicketRule> Rules, List<int> MyTicket, List<List<int>> NearbyTickets) ParseInput(string filePath)
        {
            //Split the input into sections
            var input = File.ReadAllText(filePath);
            var firstSplit = input.Split("your ticket:", StringSplitOptions.RemoveEmptyEntries);
            var secondSplit = firstSplit[1].Split("nearby tickets:", StringSplitOptions.RemoveEmptyEntries);

            //Process the sections into useful structures
            var ticketDataLines = firstSplit[0].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var nearbyTicketsLines = secondSplit[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var yourticket = secondSplit[0];

            //Process ticket rules
            var ticketRules = new List<TicketRule>();
            foreach (var ticketDataLine in ticketDataLines)
            {
                if (string.IsNullOrWhiteSpace(ticketDataLine))
                {
                    continue;
                }

                var (rulename, firstLow, firstHigh, secondLow, secondHigh) =
                    ticketDataLine.Extract<(string, int, int, int, int)>(
                        @"(.+): (\d+)-(\d+) or (\d+)-(\d+)");

                var ticketRule = new TicketRule
                {
                    Name = rulename,
                    FirstRule = (firstLow, firstHigh),
                    SecondRule = (secondLow, secondHigh)
                };

                ticketRules.Add(ticketRule);
            }

            //Process other tickets
            var nearbyTickets = new List<List<int>>();
            foreach (var nearbyTicketsLine in nearbyTicketsLines)
            {
                var nearbyTicketNumbers = nearbyTicketsLine.Split(",", StringSplitOptions.RemoveEmptyEntries)
               .Select(n => int.Parse(n)).ToList();

                nearbyTickets.Add(nearbyTicketNumbers);
            }

            //process your own ticket
            var yourTicketNumbers = yourticket.Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(n => int.Parse(n)).ToList();

            return (ticketRules, yourTicketNumbers, nearbyTickets);
        }
    }
}