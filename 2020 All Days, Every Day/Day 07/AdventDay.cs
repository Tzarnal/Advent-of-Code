using Advent;
using Serilog;

namespace Day_07
{
    public class AdventDay : IAdventDay
    {
        private IAdventProblem ProblemPart1;
        private IAdventProblem ProblemPart2;

        public AdventDay()
        {
            ProblemPart1 = new Part1();
            ProblemPart2 = new Part2();
        }

        public void SolveProblems()
        {
            Log.Information("Running {ProblemName}", ProblemPart1.ProblemName);
            ProblemPart1.Run();

            Log.Information("Running {ProblemName}", ProblemPart2.ProblemName);
            ProblemPart2.Run();
        }
    }
}