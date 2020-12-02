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
            ProblemPart1 = new Day_02.Part1();
            ProblemPart2 = new Day_02.Part2();
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