using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;
using System.Numerics;

namespace Day_11
{
    //https://adventofcode.com/2022/day/11#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Monkey in the Middle. Part Two."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<Monkey> monkeys, int rounds = 10000)
        {
            var commonMod = monkeys.Aggregate(1, (mod, monkey) => mod * monkey.Divisor);

            for (int i = 0; i < rounds; i++)
            {
                foreach (var monkey in monkeys)
                {
                    while (monkey.Items.Count > 0)
                    {
                        //Monkey looks at item
                        var item = monkey.Items.Dequeue();
                        monkey.Inspections++;

                        //Keep worry level managable
                        item %= commonMod;

                        long operationvalue = monkey.OperationValue;

                        //Monkey does its operation
                        if (monkey.Operation == "*")
                        {
                            item *= operationvalue;
                        }
                        else if (monkey.Operation == "+")
                        {
                            item += operationvalue;
                        }
                        else if (monkey.Operation == "old * old")
                        {
                            item *= item;
                        }

                        //Test worry level and throw
                        if (item % monkey.Divisor == 0)
                        {
                            monkeys[monkey.TrueTarget].Items.Enqueue(item);
                        }
                        else
                        {
                            monkeys[monkey.FalseTarget].Items.Enqueue(item);
                        }
                    }
                }
            }

            var monkeyInspections = monkeys.Select(monkey => monkey.Inspections).OrderByDescending(i => i).ToList();
            var monkeyBusiness = monkeyInspections[0] * monkeyInspections[1];
            Log.Information("The level of monkey business after {rounds} rounds of stuff - slinging simian shenanigans is {monkeyBusiness}",
                rounds, monkeyBusiness);
        }
    }

    public class BigMonkey
    {
        public int Id;
        public Queue<long> Items;

        public string Operation;
        public int OperationValue;

        public int Divisor;

        public int TrueTarget;
        public int FalseTarget;

        public long Inspections;
    }
}