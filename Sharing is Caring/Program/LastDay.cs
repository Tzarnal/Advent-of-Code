using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Advent
{
    public class LastDay
    {
        public IAdventProblem ProblemPart1;
        public IAdventProblem ProblemPart2;

        private IAdventDay AdventDay;

        public LastDay()
        {
            var lastDay = FindLastDay();

            AdventDay = (IAdventDay)Activator.CreateInstance(lastDay);

            ProblemPart1 = AdventDay.ProblemPart1;
            ProblemPart2 = AdventDay.ProblemPart2;
        }

        private Type FindLastDay()
        {
            var nameSpaces = from type in Assembly.GetExecutingAssembly().GetTypes()
                             select type;
            nameSpaces = nameSpaces.Distinct().Where(t => t.FullName.Contains("AdventDay")
            && !t.FullName.Contains("00")
            && !t.FullName.Contains("IAdventDay")).OrderBy(t => t.FullName); ;

            return nameSpaces.ToList().Last();
        }
    }
}