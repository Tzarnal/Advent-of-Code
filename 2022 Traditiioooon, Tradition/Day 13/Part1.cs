using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;
using System.Text;

namespace Day_13
{
    //https://adventofcode.com/2022/day/13
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Distress Signal. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<(Signal Left, Signal Right)> input)
        {
            var recordsInOrder = new List<int>();

            for (int i = 0; i < input.Count; i++)
            {
                var record = input[i];

                var (_, correct) = CompareRecords(record.Left, record.Right);

                if (correct)
                {
                    recordsInOrder.Add(i + 1); //Records are 1 indexed according to AoC
                }
            }

            Log.Information("There are {c} records in the right order with the sum of {sum}.",
                recordsInOrder.Count,
                recordsInOrder.Sum());
        }

        public static (bool Equal, bool Correct) CompareRecords(Signal left, Signal right)
        {
            //If both values are integers, the lower integer should come first ( left ).
            if (left.IsValue && right.IsValue)
            {
                if (left.Value == right.Value)
                {
                    return (true, false);
                }

                return (false, right.Value > left.Value);
            }

            //If exactly one value is an integer, convert the integer to a list which contains that integer as its only value
            if (left.IsValue != right.IsValue)
            {
                left.BecomeList();
                right.BecomeList();
            }

            //Compare values in lists, Left side should still be lower
            var minLength = Math.Min(left.signals.Count, right.signals.Count);
            for (int i = 0; i < minLength; i++)
            {
                var rleft = left.signals[i];
                var rright = right.signals[i];

                var (Equal, Correct) = CompareRecords(rleft, rright);

                if (!Equal)
                {
                    return (false, Correct);
                }
            }

            //All the left side signals were smaller so is the right side list larger
            return (right.signals.Count == left.signals.Count, right.signals.Count > left.signals.Count);
        }

        public static List<(Signal Left, Signal Right)> ParseInput(string filePath)
        {
            var signals = new List<(Signal Left, Signal Right)>();

            foreach (var record in Helpers.ReadAllRecords(filePath))
            {
                var left = ToSignal(record[0]);
                var right = ToSignal(record[1]);

                signals.Add((left, right));
            }

            return signals;
        }

        public static Signal ToSignal(string input)
        {
            const string numbers = "1234567890";

            var rootSignal = new Signal();
            var stack = new Stack<Signal>();

            string number = "";
            Signal currentSignal = rootSignal;

            foreach (char c in input)
            {
                switch (c)
                {
                    case '[':
                        stack.Push(currentSignal);
                        var newSignal = new Signal(true);
                        currentSignal.Add(newSignal);
                        currentSignal = newSignal;

                        break;

                    case ']':
                        if (number.Length > 0)
                        {
                            currentSignal.Add(number);
                            number = "";
                        }

                        currentSignal = stack.Pop();

                        break;

                    case ',':
                        if (number.Length > 0)
                        {
                            currentSignal.Add(number);
                            number = "";
                        }

                        break;

                    default:
                        if (numbers.Contains(c))
                        {
                            number += c;
                        }
                        else
                        {
                            Log.Warning("Unhandled character {c}", c);
                        }

                        break;
                }
            }

            return currentSignal.signals[0];
        }
    }

    public class Signal
    {
        public List<Signal> signals;
        public bool IsValue => !listOverride && signals.Count == 0;
        public int Value;

        private bool listOverride;

        public Signal()
        {
            signals = new List<Signal>();
        }

        public Signal(bool isList)
        {
            signals = new List<Signal>();
            listOverride = isList;
        }

        public Signal(int value)
        {
            signals = new List<Signal>();
            Value = value;
        }

        public Signal(string value)
        {
            signals = new List<Signal>();
            Value = int.Parse(value);
        }

        public Signal(Signal value)
        {
            signals = new List<Signal>();
            listOverride = true;
            signals.Add(value);
        }

        public void BecomeList()
        {
            if (!IsValue)
                return;

            listOverride = true;

            if (Value == 0)
                return;

            var valueSignal = new Signal(Value);
            signals.Add(valueSignal);

            Value = 0;
        }

        public void Add(int value)
        {
            BecomeList();
            signals.Add(new Signal(value));
        }

        public void Add(string value)
        {
            BecomeList();
            signals.Add(new Signal(value));
        }

        public void Add(Signal value)
        {
            BecomeList();
            signals.Add(value);
        }

        public override string ToString()
        {
            if (IsValue)
            {
                return Value.ToString();
            }

            var sb = new StringBuilder();
            sb.Append('[');

            var entries = new List<string>();
            foreach (var signal in signals)
            {
                entries.Add(signal.ToString());
            }

            sb.AppendJoin(", ", entries);

            sb.Append(']');

            return sb.ToString();
        }
    }
}