using Serilog;
using System.IO;

namespace Advent
{
    public class CLI
    {
        public CLI()
        {
            var debugLogger = new LoggerConfiguration()
                                     .MinimumLevel.Verbose()
                                     .WriteTo.Console()
                                     .CreateLogger();
            Log.Logger = debugLogger;
        }

        public void Process(string[] args)
        {
            if (args.Length == 1
                && string.Equals(args[0], "all", System.StringComparison.OrdinalIgnoreCase))
            {
                var days = new AllDays();
                days.RunSolutions();
            }
            else
            {
                NoArgs();
            }
        }

        private void NoArgs()
        {
            var today = new Today();

            //run solutions
            Log.Information("Running '{ProblemName}'", today.ProblemPart1.ProblemName);
            today.ProblemPart1.Run();

            Log.Information("Running '{ProblemName}'", today.ProblemPart2.ProblemName);
            today.ProblemPart2.Run();
        }
    }
}