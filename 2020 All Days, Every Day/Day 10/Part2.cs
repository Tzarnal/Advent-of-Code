using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;

namespace Day_10
{
    //https://adventofcode.com/2020/day/10#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}:Adapter Array. Part Two."; }

        private Dictionary<int, long> _memo;
        private List<int> _adapters;

        public void Run()
        {
            _adapters = ParseInput($"Day {Dayname}/inputTest.txt");
            Solve();

            _adapters = ParseInput($"Day {Dayname}/inputTest2.txt");
            Solve();

            _adapters = ParseInput($"Day {Dayname}/input.txt");
            Solve();
        }

        public void Solve()
        {
            _adapters.Add(0);//Initial zero
            _adapters = _adapters.OrderBy(i => i).ToList();
            _adapters.Add(_adapters.Max() + 3);//Final device

            _memo = new Dictionary<int, long>();

            var arrangements = FindArrangements(0);

            Log.Information("Total arrangements {arrangements}.", arrangements);
        }

        private long FindArrangements(int i)
        {
            long arrangements = 0;

            if (_memo.ContainsKey(i))
                return _memo[i];

            if (i == _adapters.Count - 1)
                return 1;

            for (int j = i + 1; j < _adapters.Count; j++)
            {
                if (_adapters[j] - _adapters[i] <= 3)
                {
                    arrangements += FindArrangements(j);
                }
            }

            _memo[i] = arrangements;

            return arrangements;
        }

        private List<int> ParseInput(string filePath)
        {
            return Helpers.ReadStringsFile(filePath).ConvertAll(i => int.Parse(i));
        }
    }
}