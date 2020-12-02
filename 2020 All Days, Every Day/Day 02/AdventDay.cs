using BenchmarkDotNet.Attributes;
using Advent;

namespace Day_02
{
    [MemoryDiagnoser]
    public class AdventDay : IAdventBenchmark
    {
        public IAdventProblem ProblemPart1 { get; set; }
        public IAdventProblem ProblemPart2 { get; set; }

        public AdventDay()
        {
            ProblemPart1 = new Part1();
            ProblemPart2 = new Part2();
        }

        [Benchmark]
        public void PartOne()
        {
            ProblemPart1.Run();
        }

        [Benchmark]
        public void PartTwo()
        {
            ProblemPart2.Run();
        }
    }
}