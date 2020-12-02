using Serilog;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using System.Linq;
using BenchmarkDotNet.Loggers;
using System.IO;
using System.Text.RegularExpressions;

namespace Advent
{
    public static class Program
    {
        public static void Main()
        {
            var today = new Today();

            var debugLogger = new LoggerConfiguration()
                     .MinimumLevel.Verbose()
                     .WriteTo.Console()
                     .CreateLogger();

            Log.Logger = debugLogger;

            //run solutions
            Log.Information("Running '{ProblemName}'", today.ProblemPart1.ProblemName);
            today.ProblemPart1.Run();

            Log.Information("Running '{ProblemName}'", today.ProblemPart2.ProblemName);
            today.ProblemPart2.Run();

            //Exit program now if this is a Debug build. If not continue and do a benchmark
#if RELEASE
            Benchmark();
            Log.Logger = debugLogger;
            Log.Verbose("Benchmarks finished");
            BenchMarkReport();
#endif
        }

        public static void Benchmark()
        {
            Log.Verbose("Running in Release build. Starting benchmarks.");

            //Don't want to be super verbose when we are running it hundreds of times
            var releaseLogger = new LoggerConfiguration()
                     .MinimumLevel.Warning()
                     .WriteTo.Console()
                     .CreateLogger();

            Log.Logger = releaseLogger;

            var config = new ManualConfig();
            //config.Options = ConfigOptions.DisableLogFile;
            config.AddColumnProvider(DefaultConfig.Instance.GetColumnProviders().ToArray());
            config.AddExporter(DefaultConfig.Instance.GetExporters().ToArray());
            config.AddDiagnoser(DefaultConfig.Instance.GetDiagnosers().ToArray());
            config.AddAnalyser(DefaultConfig.Instance.GetAnalysers().ToArray());
            config.AddJob(DefaultConfig.Instance.GetJobs().ToArray());
            config.AddValidator(DefaultConfig.Instance.GetValidators().ToArray());
            config.AddLogger(NullLogger.Instance);

            config.UnionRule = ConfigUnionRule.AlwaysUseGlobal; // Overriding the default

            BenchmarkRunner.Run<Today>(config);
        }

        public static void BenchMarkReport()
        {
            var reportText = File.ReadAllText(@"BenchmarkDotNet.Artifacts\results\Advent.Today-report-github.md");

            reportText = Regex.Replace(reportText, "``` ini.*```", "", RegexOptions.Singleline).Trim();

            var reportTextLines = reportText.Split("\n");

            foreach (var line in reportTextLines)
            {
                Log.Information("{line}", line);
            }
        }
    }
}