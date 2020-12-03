using Advent;
using Serilog;

namespace Day_01
{
    //https://adventofcode.com/2020/day/1#part2
    public class Part2 : IAdventProblem
    {
        public string ProblemName { get => "Day 1: Report Repair. Part Two."; }

        public void Run()
        {
            var inputList = Helpers.ReadNumbersFile("Day 01/input.txt");

            foreach (var number1 in inputList)
            {
                foreach (var number2 in inputList)
                {
                    foreach (var number3 in inputList)
                    {
                        if (number1 + number2 + number3 == 2020)
                        {
                            var product = number1 * number2 * number3;
                            Log.Information("Found it: {number1}+{number2}+{number3} = 2020. Product: {product}",
                                number1, number2, number3, product);
                            return;
                        }
                    }
                }
            }
        }
    }
}