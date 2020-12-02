using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace Advent
{
    public class Today
    {
        public IAdventProblem ProblemPart1;
        public IAdventProblem ProblemPart2;

        private IAdventBenchmark AdventDay;

        public Today()
        {
            //What problem are we solving today
            AdventDay = new Day_03.AdventDay();

            ProblemPart1 = AdventDay.ProblemPart1;
            ProblemPart2 = AdventDay.ProblemPart2;
        }

        public void RunTodaysBenchmarks(ManualConfig config)
        {
            BenchmarkRunner.Run(AdventDay.GetType(), config);
        }
    }
}