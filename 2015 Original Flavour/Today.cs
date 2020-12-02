using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Advent
{
    public class Today
    {
        public IAdventProblem ProblemPart1;
        public IAdventProblem ProblemPart2;

        private IAdventBenchmark AdventDay;

        public Today()
        {
            var lastDay = FindLastDay();

            IAdventBenchmark AdventDay = (IAdventBenchmark)Activator.CreateInstance(lastDay);

            ProblemPart1 = AdventDay.ProblemPart1;
            ProblemPart2 = AdventDay.ProblemPart2;
        }

        public void RunTodaysBenchmarks(ManualConfig config)
        {
            BenchmarkRunner.Run(AdventDay.GetType(), config);
        }

        private Type FindLastDay()
        {
            var nameSpaces = from type in Assembly.GetExecutingAssembly().GetTypes()
                             select type;
            nameSpaces = nameSpaces.Distinct().Where(t => t.FullName.Contains("AdventDay")).OrderBy(t => t.FullName); ;

            return nameSpaces.ToList().Last();
        }
    }
}