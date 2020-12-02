using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Serilog;

namespace Advent
{
    public class AllDays
    {
        public List<Type> AdventDays;

        public AllDays()
        {
            AdventDays = FindDays();
        }

        public void RunSolutions()
        {
            foreach (var day in AdventDays)
            {
                IAdventBenchmark AdventDay = (IAdventBenchmark)Activator.CreateInstance(day);

                Log.Information("Running '{ProblemName}'", AdventDay.ProblemPart1.ProblemName);
                AdventDay.ProblemPart1.Run();

                Log.Information("Running '{ProblemName}'", AdventDay.ProblemPart2.ProblemName);
                AdventDay.ProblemPart2.Run();
            }
        }

        private List<Type> FindDays()
        {
            var nameSpaces = from type in Assembly.GetExecutingAssembly().GetTypes()
                             select type;
            nameSpaces = nameSpaces.Distinct().Where(t => t.FullName.Contains("AdventDay") && !t.FullName.Contains("00")).OrderBy(t => t.FullName); ;

            return nameSpaces.ToList();
        }
    }
}