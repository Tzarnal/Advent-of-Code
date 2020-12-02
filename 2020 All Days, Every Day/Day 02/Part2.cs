using Advent;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Day_02
{
    //Page Link
    internal class Part2 : IAdventProblem
    {
        public string ProblemName { get => "Day 02: Password Philosophy Part Two."; }

        public void Run()
        {
            var inputFile = File.ReadAllLines("Day 02/input.txt");
            var passwordData = ParseInput(inputFile);

            var correctPasswords = 0;

            foreach (var passwordDatum in passwordData)
            {
                if (TestPassword(passwordDatum.Key, passwordDatum.Value))
                {
                    correctPasswords++;
                }
            }

            Log.Information("Found {correctPasswords} correct passwords.", correctPasswords);
        }

        private bool TestPassword(PasswordPolicyRequirement Requirement, string Password)
        {
            var occuranceCount = 0;

            if (Password.Substring(Requirement.MinimumAppearances, 1) == Requirement.RequiredLetter)
            {
                occuranceCount++;
            }

            if (Password.Substring(Requirement.MaximumApperances, 1) == Requirement.RequiredLetter)
            {
                occuranceCount++;
            }

            if (occuranceCount == 1)
            {
                return true;
            }
            return false;
        }

        private Dictionary<PasswordPolicyRequirement, string> ParseInput(string[] input)
        {
            var passwordData = new Dictionary<PasswordPolicyRequirement, string>();

            var passwordRegex = @"(\d+)-(\d+) (\w): (\w+)";

            foreach (var line in input)
            {
                var regexMatch = Regex.Match(line, passwordRegex);
                var requirements = new PasswordPolicyRequirement();
                var password = "";

                if (regexMatch.Success)
                {
                    requirements.MinimumAppearances = int.Parse(regexMatch.Groups[1].Value) - 1;
                    requirements.MaximumApperances = int.Parse(regexMatch.Groups[2].Value) - 1;
                    requirements.RequiredLetter = regexMatch.Groups[3].Value;

                    password = regexMatch.Groups[4].Value;
                    passwordData.Add(requirements, password);
                }
            }

            return passwordData;
        }
    }
}