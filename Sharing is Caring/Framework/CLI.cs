using Serilog;
using System;
using System.Diagnostics;
using System.Linq;

namespace Advent.Framework
{
    public class CLI
    {
        public static Serilog.Core.Logger DefaultLogger;
        public static Serilog.Core.Logger IndentLogger;

        public CLI()
        {
            string defaultLoggerTemplate = "[{Timestamp:HH:mm:ss ffff} {Level:u3}] {Message}{NewLine}{Exception}";
            DefaultLogger = new LoggerConfiguration()
                                     .MinimumLevel.Information()
                                     .WriteTo.Console(outputTemplate: defaultLoggerTemplate)
                                     .CreateLogger();

            string indentLoggerTemplate = "[{Timestamp:HH:mm:ss ffff} {Level:u3}] \t{Message}{NewLine}{Exception}";
            IndentLogger = new LoggerConfiguration()
                                     .MinimumLevel.Information()
                                     .WriteTo.Console(outputTemplate: indentLoggerTemplate)
                                     .CreateLogger();
            [Conditional("DEBUG")]
            static void DebugLoggers()
            {
                string defaultLoggerTemplate = "[{Timestamp:HH:mm:ss ffff} {Level:u3}] {Message}{NewLine}{Exception}";
                DefaultLogger = new LoggerConfiguration()
                                     .MinimumLevel.Verbose()
                                     .WriteTo.Console(outputTemplate: defaultLoggerTemplate)
                                     .CreateLogger();

                string indentLoggerTemplate = "[{Timestamp:HH:mm:ss ffff} {Level:u3}] \t{Message}{NewLine}{Exception}";
                IndentLogger = new LoggerConfiguration()
                                         .MinimumLevel.Verbose()
                                         .WriteTo.Console(outputTemplate: indentLoggerTemplate)
                                         .CreateLogger();
            }

            DebugLoggers();

            Log.Logger = DefaultLogger;
        }

        public void Process(string[] args)
        {
            if (args.Length == 1
                && string.Equals(args[0], "all", System.StringComparison.OrdinalIgnoreCase))
            {
                RunAll();
            }
            else if (args.Length == 1)
            {
                RunOne(args[0]);
            }
            else if (args.Length > 1)
            {
                RunMany(args);
            }
            else
            {
                NoArgs();
            }
        }

        private void RunAll()
        {
            var dayTypes = FindDays();

            if (dayTypes.Count == 0)
            {
                Log.Error("No days found. Did you write any?");
                return;
            }

            var groupedDays = dayTypes.GroupBy(d => d.FullName.Split(".")[0]);

            foreach (var day in groupedDays)
            {
                foreach (var problem in day)
                {
                    var part = (IAdventProblem)Activator.CreateInstance(problem);
                    Helpers.ProblemRunner(part);
                }
            }
        }

        private void RunMany(string[] args)
        {
            foreach (var arg in args)
            {
                RunOne(arg);
            }
        }

        private void RunOne(string dayName)
        {
            var dayTypes = FindDays().Where(d => d.FullName.ToLower().Contains(dayName.ToLower())).ToList();

            if (dayTypes.Count == 0)
            {
                Log.Error("No days found. Did you write any?");
                return;
            }

            var groupedDays = dayTypes.GroupBy(d => d.FullName.Split(".")[0]);

            foreach (var problem in groupedDays.Last())
            {
                var part = (IAdventProblem)Activator.CreateInstance(problem);
                Helpers.ProblemRunner(part);
            }
        }

        private void NoArgs()
        {
            var dayTypes = FindDays();

            if (dayTypes.Count == 0)
            {
                Log.Error("No days found. Did you write any?");
                return;
            }

            var groupedDays = dayTypes.GroupBy(d => d.FullName.Split(".")[0]);

            foreach (var problem in groupedDays.Last())
            {
                var part = (IAdventProblem)Activator.CreateInstance(problem);
                Helpers.ProblemRunner(part);
            }
        }

        private System.Collections.Generic.List<Type> FindDays()
        {
            var problemInterface = typeof(IAdventProblem);
            var problems = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(a => a.GetTypes())
                            .Where(t => problemInterface.IsAssignableFrom(t) && t.Name != "IAdventProblem")
                            .OrderBy(p => p.FullName);

            return problems.ToList();
        }
    }
}