using System;
using System.Collections.Generic;
using System.Text;

namespace Advent
{
    internal static class Extensions
    {
        private static Random _genie = new Random();

        public static int Count(this string str, string comp)
        {
            int starLength = str.Length;
            var modified = str.Replace(comp, "");
            int finalLength = modified.Length;

            return (starLength - finalLength) / comp.Length;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _genie.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}