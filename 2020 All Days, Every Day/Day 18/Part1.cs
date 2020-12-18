using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_18
{
    //https://adventofcode.com/2020/day/18
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Operation Order. Part One."; }

        public void Run()
        {
            //var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<string> input)
        {
            long sum = 0;

            foreach (var line in input)
            {
                var lineInProgress = line;
                var firstNested = FirstNestedStatement(lineInProgress);

                while (!string.IsNullOrWhiteSpace(firstNested))
                {
                    var result = DoMath(firstNested);
                    lineInProgress = lineInProgress.Replace($"({firstNested})", result.ToString());

                    firstNested = FirstNestedStatement(lineInProgress);
                }

                var endResult = DoMath(lineInProgress);
                sum += endResult;
            }

            Log.Information("After {count} bits of math homework the total sum is {sum}",
                input.Count, sum);
        }

        private long DoMath(string input)
        {
            long total = 0;

            var chunks = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            total = long.Parse(chunks[0]);
            for (var i = 1; i < chunks.Length; i++)
            {
                if (chunks[i] is "*" or "+" or "-")
                {
                    var secondValue = long.Parse(chunks[i + 1]);

                    switch (chunks[i])
                    {
                        case "+":
                            total += secondValue;
                            break;

                        case "-":
                            total -= secondValue;
                            break;

                        case "*":
                            total *= secondValue;
                            break;

                        case "/":
                            total /= secondValue;
                            break;
                    }

                    i++;
                }
            }

            return total;
        }

        private string FirstNestedStatement(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }

            Stack<int> brackets = new Stack<int>();

            for (int i = 0; i < value.Length; ++i)
            {
                var c = value[i];
                if (c == '(')
                {
                    brackets.Push(i);
                }
                else if (c == ')')
                {
                    var openBracket = brackets.Pop();
                    return value.Substring(openBracket + 1, i - openBracket - 1);
                }
            }

            return "";
        }

        private List<string> ParseInput(string filePath)
        {
            return Helpers.ReadStringsFile(filePath);
        }
    }
}