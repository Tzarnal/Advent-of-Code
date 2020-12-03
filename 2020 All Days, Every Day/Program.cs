using Serilog;
using System.IO;

namespace Advent
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var debugLogger = new LoggerConfiguration()
                                     .MinimumLevel.Verbose()
                                     .WriteTo.Console()
                                     .CreateLogger();
            Log.Logger = debugLogger;

            if (args.Length == 1
                && string.Equals(args[0], "all", System.StringComparison.OrdinalIgnoreCase))
            {
                var days = new AllDays();
                days.RunSolutions();
            }
            else
            {
                var today = new Today();

                //run solutions
                Log.Information("Running '{ProblemName}'", today.ProblemPart1.ProblemName);
                today.ProblemPart1.Run();

                Log.Information("Running '{ProblemName}'", today.ProblemPart2.ProblemName);
                today.ProblemPart2.Run();

                //Exit program now if this is a Debug build. If not continue and do a benchmark
            }
        }
    }
}