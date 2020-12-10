using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_07
{
    //https://adventofcode.com/2015/day/7#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Some Assembly Required. Part Two."; }
        private Dictionary<string, ushort> _wireSignals;

        private Dictionary<string, string> _instructions;

        public void Run()
        {
            //var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<(string command, string wire)> input)
        {
            _wireSignals = new Dictionary<string, ushort>();
            _instructions = new Dictionary<string, string>();

            input.ForEach(i => _instructions.Add(i.wire, i.command));

            var a = Run("a");

            _wireSignals = new Dictionary<string, ushort>();
            _instructions["b"] = a.ToString();

            Log.Information("The signal on wire is {signal}.", Run("a"));
        }

        private ushort Getargument(string wire)
        {
            //First check if the command is a simple integer
            ushort n = 0;
            if (ushort.TryParse(wire, out n))
            {
                return n;
            }

            return Run(wire);
        }

        private ushort Run(string wire)
        {
            //First check if the command is a simple integer
            ushort n = 0;
            if (ushort.TryParse(wire, out n))
            {
                return n;
            }

            var command = _instructions[wire];

            if (ushort.TryParse(command, out n))
            {
                _wireSignals[wire] = n;
            }
            else if (command.Contains("NOT"))
            {
                var tWire = command.Extract<string>(@"NOT (\w+)");
                if (_wireSignals.ContainsKey(tWire))
                {
                    _wireSignals[wire] = (ushort)~_wireSignals[tWire];
                }
                else
                {
                    Run(tWire);
                    _wireSignals[wire] = (ushort)~Getargument(tWire);
                }
            }
            else if (command.Contains("AND"))
            {
                var (firstWire, secondWire) = command.Extract<(string, string)>(@"(\w+) AND (\w+)");

                if (_wireSignals.ContainsKey(firstWire) && _wireSignals.ContainsKey(secondWire))
                {
                    _wireSignals[wire] = (ushort)(_wireSignals[firstWire] & _wireSignals[secondWire]);
                }
                else
                {
                    _wireSignals[wire] = (ushort)(Getargument(firstWire) & Getargument(secondWire));
                }
            }
            else if (command.Contains("OR"))
            {
                var (firstWire, secondWire) = command.Extract<(string, string)>(@"(\w+) OR (\w+)");

                if (_wireSignals.ContainsKey(firstWire) && _wireSignals.ContainsKey(secondWire))
                {
                    _wireSignals[wire] = (ushort)(_wireSignals[firstWire] | _wireSignals[secondWire]);
                }
                else
                {
                    _wireSignals[wire] = (ushort)(Getargument(firstWire) | Getargument(secondWire));
                }
            }
            else if (command.Contains("LSHIFT"))
            {
                var (tWire, value) = command.Extract<(string, ushort)>(@"(\w+) LSHIFT (\d+)");

                if (_wireSignals.ContainsKey(tWire))
                {
                    _wireSignals[wire] = (ushort)(_wireSignals[tWire] << value);
                }
                else
                {
                    _wireSignals[wire] = (ushort)(Getargument(tWire) << value);
                }
            }
            else if (command.Contains("RSHIFT"))
            {
                var (tWire, value) = command.Extract<(string, ushort)>(@"(\w+) RSHIFT (\d+)");

                if (_wireSignals.ContainsKey(tWire))
                {
                    _wireSignals[wire] = (ushort)(_wireSignals[tWire] >> value);
                }
                else
                {
                    Run(tWire);
                    _wireSignals[wire] = (ushort)(Getargument(tWire) >> value);
                }
            }
            else
            {
                if (_instructions.ContainsKey(command))
                {
                    _wireSignals[wire] = Run(command);
                }
                else
                {
                    Log.Warning(command);
                }
            }

            return _wireSignals[wire];
        }

        private List<(string command, string wire)> ParseInput(string filePath)
        {
            var output = new List<(string command, string wire)>();

            foreach (var line in Helpers.ReadStringsFile(filePath))
            {
                var (command, wire) = line.Extract<(string, string)>("(.+) -> (.+)");
                output.Add((command, wire));
            }

            return output;
        }
    }
}