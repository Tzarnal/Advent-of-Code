using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_16
{
    //https://adventofcode.com/2020/day/16
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Ticket Translation. Part One."; }

        public void Run()
        {
            //var testData = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testData.Rules, testData.MyTicket, testData.NearbyTickets);

            var inputData = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputData.Rules, inputData.NearbyTickets);
        }

        public void Solve(List<TicketRule> Rules, List<List<int>> NearbyTickets)
        {
            var invalidFields = new List<int>();

            foreach (var nearbyTicket in NearbyTickets)
            {
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
                        invalidFields.Add(field);
                    }
                }
            }

            Log.Information("Found {invalid} invalid fields. Sum of invalid fields is {sum}.",
                invalidFields.Count, invalidFields.Sum(f => f));
        }

        private (List<TicketRule> Rules, List<List<int>> NearbyTickets) ParseInput(string filePath)
        {
            //Split the input into sections
            var input = File.ReadAllText(filePath);
            var firstSplit = input.Split("your ticket:", StringSplitOptions.RemoveEmptyEntries);
            var secondSplit = firstSplit[1].Split("nearby tickets:", StringSplitOptions.RemoveEmptyEntries);

            //Process the sections into useful structures
            var ticketDataLines = firstSplit[0].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var nearbyTicketsLines = secondSplit[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

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

            return (ticketRules, nearbyTickets);
        }
    }
}

public record TicketRule
{
    public string Name;
    public (int Low, int High) FirstRule;
    public (int Low, int High) SecondRule;
}