using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Serilog;

namespace Advent.Framework
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
                var AdventDay = (IAdventDay)Activator.CreateInstance(day);
                AdventDay.SolveProblems();
            }
        }

        private List<Type> FindDays()
        {
            var nameSpaces = from type in Assembly.GetExecutingAssembly().GetTypes()
                             select type;
            nameSpaces = nameSpaces.Distinct().Where(t => t.FullName.Contains("AdventDay")
            && !t.FullName.Contains("00")
            && !t.FullName.Contains("IAdventDay")).OrderBy(t => t.FullName); ;

            return nameSpaces.ToList();
        }
    }
}