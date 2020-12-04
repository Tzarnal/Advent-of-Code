using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Advent.Framework
{
    public class LastDay
    {
        public IAdventDay AdventDay;

        public LastDay()
        {
            var lastDay = FindLastDay();

            AdventDay = (IAdventDay)Activator.CreateInstance(lastDay);
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