using Advent;
using Serilog;

namespace Day_06
{
    public class AdventDay : IAdventDay
    {
        private IAdventProblem ProblemPart1;
        private IAdventProblem ProblemPart2;
        private IAdventProblem Reddit1;

        public AdventDay()
        {
            ProblemPart1 = new Part1();
            ProblemPart2 = new Part2();
            Reddit1 = new RedditorBasukun();
        }

        public void SolveProblems()
        {
            Log.Information("Running {ProblemName}", ProblemPart1.ProblemName);
            ProblemPart1.Run();

            Log.Information("Running {ProblemName}", ProblemPart2.ProblemName);
            ProblemPart2.Run();

            Log.Information("Running {ProblemName}", Reddit1.ProblemName);
            Reddit1.Run();
        }
    }
}