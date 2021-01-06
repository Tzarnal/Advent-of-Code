using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_11
{
    //https://adventofcode.com/2015/day/11
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Corporate Policy. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public static void Solve(List<string> input)
        {
            foreach (var line in input)
            {
                var password = line;

                while (!IsValid(password))
                {
                    password = Increment(password);
                }

                Log.Information("{line} became {password}", line, password);
            }
        }

        public static string Increment(string input)
        {
            var characters = input.ToCharArray();

            for (int i = characters.Length - 1; i >= 0; i--)
            {
                var cursor = characters[i];
                cursor++;

                //next after a+1 in ascii is {
                //if cursor isn't { we're not carrying and can finish.
                if (cursor != '{')
                {
                    characters[i] = cursor;
                    break;
                }

                //z loops back to a
                characters[i] = 'a';
            }

            return string.Join("", characters);
        }

        public static bool IsValid(string Password)
        {
            //Does not contain i, o or l
            string[] invalidCharacters = new string[] { "i", "o", "l" };
            if (invalidCharacters.Any(c => Password.Contains(c)))
            {
                return false;
            }

            //Contains a straight
            var straights = ThreeCharStraights();
            if (straights.All(s => !Password.Contains(s)))
            {
                return false;
            }

            //At least two non overlapping pairs
            var runs = CountRuns(Password).Where(run => run.count > 1).Distinct().ToList();
            return runs.Count >= 2;
        }

        public static IEnumerable<string> ThreeCharStraights()
        {
            const string alphabet = "abcdefghijklmnopqrstuvwxyz";

            for (int i = 0; i < alphabet.Length - 2; i++)
            {
                yield return $"{alphabet[i]}{alphabet[i + 1]}{alphabet[i + 2]}";
            }

            yield return "abc";
        }

        public static List<(char character, int count)> CountRuns(string input)
        {
            var output = new List<(char character, int count)>();

            var currentChar = input[0];
            var runLength = 1;

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == currentChar)
                {
                    runLength++;
                }
                else
                {
                    output.Add((currentChar, runLength));
                    currentChar = input[i];
                    runLength = 1;
                }
            }

            output.Add((currentChar, runLength));

            return output;
        }

        public static List<string> ParseInput(string filePath)
        {
            return Helpers.ReadStringsFile(filePath);
        }
    }
}