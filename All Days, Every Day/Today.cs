using BenchmarkDotNet.Attributes;

namespace Advent
{
    [MemoryDiagnoser]
    public class Today
    {
        public IAdventProblem ProblemPart1;
        public IAdventProblem ProblemPart2;

        private IAdventProblem BenchmarkProblem;

        public Today()
        {
            //What problem are we solving today
            ProblemPart1 = new Day_01.Part1();
            ProblemPart2 = new Day_01.Part2();

            BenchmarkProblem = new Day_01.Part2EarlyExit();
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