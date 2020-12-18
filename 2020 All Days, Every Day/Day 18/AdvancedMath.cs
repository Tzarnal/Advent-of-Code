using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day_18
{
    //Solves Advanced Math according ot AoC 2020 Day 18 part2.
    //Which is not actually how to do math. adjust operator priority to sensible values if resuing
    public static class AdvancedMath
    {
        public static double DoMath(string input)
        {
            var problem = StringToProblem(input);

            //Resolve the Parenthesis deepest first one at a time.
            while (DeepestNestedProblem(problem).Success)
            {
                var (_, start, length) = DeepestNestedProblem(problem);
                var result = ResolveProblem(problem.GetRange(start + 1, length - 1));

                problem[start] = new MathElement(result); // replace the ( of the nested statement with the result of the problem
                problem.RemoveRange(start + 1, length);//remove everything past the opening ( from the list
            }

            //Resolve the final unparenthesized problem
            return ResolveProblem(problem);
        }

        //Resolve a problem in two steps first + and - then * and /
        //These are Advanced Math rules which does not obey real life math rules
        //Adjust these if re-using this code for other math problems
        public static double ResolveProblem(List<MathElement> problem)
        {
            problem = ResolveOperators(problem, new string[] { "+", "-" });
            problem = ResolveOperators(problem, new string[] { "*", "/" });

            if (problem.Count == 1)
                return problem[0].Value;

            throw new Exception("Symbols remaining after resolution.");
        }

        //Resolves only the provided operators
        private static List<MathElement> ResolveOperators(List<MathElement> problem, string[] operators)
        {
            for (var i = 0; i < problem.Count; i++)
            {
                var element = problem[i];

                if (element.IsOperator && operators.Contains(element.Operator))
                {
                    var result = OperateOperators(problem[i - 1], element, problem[i + 1]);
                    problem[i - 1] = new MathElement(result);
                    problem.RemoveRange(i, 2);
                    i--;
                }
            }

            return problem;
        }

        private static double OperateOperators(MathElement valA, MathElement op, MathElement valB)
        {
            return OperateOperators(valA.Value, op.Operator, valB.Value);
        }

        //Actually Do Math To Numbers
        private static double OperateOperators(double valA, string op, double valB)
        {
            switch (op)
            {
                case "+":
                    return valA + valB;

                case "-":
                    return valA - valB;

                case "*":
                    return valA * valB;

                case "/":
                    return valA / valB;

                default:
                    throw new Exception($"Unknown operator {op}");
            }
        }

        private static List<MathElement> StringToProblem(string input)
        {
            var problem = new List<MathElement>();
            var number = "";

            for (var i = 0; i < input.Length; i++)
            {
                //is it a digit or a ., remember it
                var symbol = input[i];
                if (IsDigit(symbol) || symbol == '.')
                {
                    number += symbol;
                }

                //is not a digit or a . then add any remember digits and. to the problem as a number and clear the remembered ones
                if (!(IsDigit(symbol) || symbol == '.') && !string.IsNullOrWhiteSpace(number))
                {
                    var element = new MathElement(number);
                    problem.Add(element);
                    number = "";
                }

                //its not a digit or a . so it must be a symbol to add to the problem
                if (!(IsDigit(symbol) || symbol == '.') && !string.IsNullOrWhiteSpace(symbol.ToString()))
                {
                    var element = new MathElement(symbol);
                    problem.Add(element);
                }
            }

            //Trailing number
            if (!string.IsNullOrWhiteSpace(number))
            {
                var element = new MathElement(number);
                problem.Add(element);
            }

            return problem;
        }

        public static string ProblemToString(List<MathElement> Problem)
        {
            var sb = new StringBuilder();

            foreach (var element in Problem)
            {
                sb.Append(element);
            }

            return sb.ToString();
        }

        private static (bool Success, int Start, int Length) DeepestNestedProblem(List<MathElement> Problem)
        {
            //empty problems don't have nesting
            if (Problem.Count == 0)
            {
                return (false, 0, 0);
            }

            var lastOpen = 0;

            for (int i = 0; i < Problem.Count; ++i)
            {
                var c = Problem[i];
                if (c == '(')
                {
                    lastOpen = i;
                }
                else if (c == ')')
                {
                    return (true, lastOpen, i - lastOpen);
                }
            }

            //didn't find any, must not have any
            return (false, 0, 0);
        }

        private static bool IsDigit(char c)
        {
            return double.TryParse(c.ToString(), out _);
        }

        private static bool IsDigit(string s)
        {
            return double.TryParse(s, out _);
        }
    }
}