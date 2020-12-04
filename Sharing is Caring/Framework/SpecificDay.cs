using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Advent.Framework
{
    class SpecificDay
    {
        public IAdventDay AdventDay;

        public SpecificDay(string dayName)
        {
            var day = FindDay(dayName);

            if (day == null)
            {
                throw new ArgumentException("Day not found");
            }

            AdventDay = (IAdventDay)Activator.CreateInstance(day);
        }

        private Type FindDay(string dayName)
        {
            var nameSpaces = from type in Assembly.GetExecutingAssembly().GetTypes()
                             select type;
            nameSpaces = nameSpaces.Distinct().Where(t => t.FullName.Contains("AdventDay")
            && !t.FullName.Contains("00")
            && !t.FullName.Contains("IAdventDay")).OrderBy(t => t.FullName);

            return nameSpaces.Where(t => t.FullName.Contains(dayName)).FirstOrDefault();
        }
    }
}