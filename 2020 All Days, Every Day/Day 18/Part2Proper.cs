using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_18
{
    //https://adventofcode.com/2020/day/18#part2
    public class Part2Proper : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Operation Order. Part Two. ( Properly this time )"; }

        public void Run()
        {
            var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<string> input)
        {
            double sum = 0;

            foreach (var line in input)
            {
                var result = AdvancedMath.DoMath(line);
                sum += result;
            }

            Log.Information("After {count} bits of math homework the total sum is {sum}",
                input.Count, sum);
        }

        private long DoMath(string input)
        {
            var addSubSymbols = new string[] { "+", "-" };
            var ultDivSymbols = new string[] { "*", "/" };

            while (addSubSymbols.Any(s => input.Contains(s)))
            {
                input = DoMath(input, addSubSymbols);
            }

            while (ultDivSymbols.Any(s => input.Contains(s)))
            {
                input = DoMath(input, ultDivSymbols);
            }

            return long.Parse(input);
        }

        private string DoMath(string input, string[] operators)
        {
            var chunks = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            for (var i = 1; i < chunks.Length; i++)
            {
                if (operators.Contains(chunks[i]))
                {
                    var firstValue = long.Parse(chunks[i - 1]);
                    var secondValue = long.Parse(chunks[i + 1]);

                    var op = chunks[i];

                    var result = DoMath(firstValue, secondValue, op);
                    chunks[i + 1] = result.ToString();

                    var expressionToReplace = $"{firstValue} {op} {secondValue}";

                    input = input.ReplaceFirst(expressionToReplace, result.ToString());

                    //skip an element so we don't process the same operation twice
                    i++;
                }
            }

            return input;
        }

        private long DoMath(long valA, long valB, string op)
        {
            switch (op)
            {
                case "+":
                    valA += valB;
                    break;

                case "-":
                    valA -= valB;
                    break;

                case "*":
                    valA *= valB;
                    break;

                case "/":
                    valA /= valB;
                    break;
            }

            return valA;
        }

        //resturns the deepest nested statement
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