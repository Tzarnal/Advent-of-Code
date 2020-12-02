using System;
using System.Collections.Generic;
using System.Text;

namespace Advent
{
    static class Extensions
    {
        public static int Count(this string str, string comp)
        {
            int starLength = str.Length;
            var modified = str.Replace(comp, "");
            int finalLength = modified.Length;

            return (starLength - finalLength) / comp.Length;
        }
    }
}