using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Advent
{
    public class Today
    {
        public IAdventProblem ProblemPart1;
        public IAdventProblem ProblemPart2;

        private IAdventDay AdventDay;

        public Today()
        {
            var lastDay = FindLastDay();

            IAdventDay AdventDay = (IAdventDay)Activator.CreateInstance(lastDay);

            ProblemPart1 = AdventDay.ProblemPart1;
            ProblemPart2 = AdventDay.ProblemPart2;
        }

        private Type FindLastDay()
        {
            var nameSpaces = from type in Assembly.GetExecutingAssembly().GetTypes()
                             select type;
            nameSpaces = nameSpaces.Distinct().Where(t => t.FullName.Contains("AdventDay")).OrderBy(t => t.FullName); ;

            return nameSpaces.ToList().Last();
        }
    }
}