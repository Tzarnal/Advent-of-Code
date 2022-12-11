using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;

namespace Day_11
{
    //https://adventofcode.com/2022/day/11
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Monkey in the Middle. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<Monkey> monkeys, int rounds = 20)
        {
            for(int i = 0; i< 20; i++)
            {
                foreach(var monkey in monkeys)
                {
                    while(monkey.Items.Count > 0)
                    {
                        //Monkey looks at item
                        var item = monkey.Items.Dequeue();
                        monkey.Inspections++;
                                                
                        //Monkey does its operation
                        if(monkey.Operation == "*")
                        {
                            item *= monkey.OperationValue;
                        }else if(monkey.Operation == "+")
                        {
                            item += monkey.OperationValue;
                        }
                        else if (monkey.Operation == "old * old")
                        {
                            item *= item;
                        }

                        //Worry level drops
                        item /= 3;

                        //Test worry level and throw
                        if(item % monkey.Divisor == 0)
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

        public static List<Monkey> ParseInput(string filePath)
        {
            var monkeys = new List<Monkey>();
            
            foreach(var record in Helpers.ReadAllRecords(filePath))
            {
                var monkey = new Monkey();
                
                monkey.Id = Helpers.ReadAllIntsStrings(record[0])[0];
                monkey.Items = new Queue<long>(Helpers.ReadAllIntsStrings(record[1]).Select(r => (long)r));
                
                (string operation, string value) op = record[2].Extract<(string, string)>
                    ("Operation: new = old (.) (.*)");

                monkey.Operation = op.operation;
                if (op.value == "old")
                {
                    monkey.Operation = "old * old";
                }
                else
                {
                    monkey.OperationValue = int.Parse(op.value);
                }

                monkey.Divisor = Helpers.ReadAllIntsStrings(record[3])[0];
                monkey.TrueTarget = Helpers.ReadAllIntsStrings(record[4])[0];
                monkey.FalseTarget = Helpers.ReadAllIntsStrings(record[5])[0];

                monkeys.Add(monkey);
            }

            return monkeys;

        }
    }

    public class Monkey
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