using System;
using System.Collections.Generic;
using System.Linq;
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

        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
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

        public static IEnumerable<string> SplitBySize(this string text, int chunkSize)
        {
            for (int i = 0; i < text.Length; i += chunkSize)
                yield return text.Substring(i, chunkSize);
        }

        public static string Reverse(this string input)
        {
            char[] array = input.ToCharArray();
            Array.Reverse(array);
            return new String(array);
        }

        public static bool IsUpper(this string input)
        {
            foreach (var c in input)
            {
                if (char.IsLetter(c) && char.IsLower(c))
                {
                    return false;
                }
            }

            return true;
        }

        public static (string left, string right) CutInHalf(this string input)
        {
            var half = input.Length / 2;
            var l = input[..half];
            var r = input[half..];
            
            return (l,r);
        }

        public static void Increment(this Dictionary<string,int> input, string key)
        {
            if(input.ContainsKey(key))
            {
                input[key]++;
            }
            else
            {
                input[key] = 1;
            }
        }

        public static void Increment(this Dictionary<char, int> input, char key)
        {
            if (input.ContainsKey(key))
            {
                input[key]++;
            }
            else
            {
                input[key] = 1;
            }
        }


        public static bool IsLower(this string input)
        {
            foreach (var c in input)
            {
                if (char.IsLetter(c) && char.IsUpper(c))
                {
                    return false;
                }
            }

            return true;
        }

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> list, int length)
        {
            if (length == 1)
            {
                return list.Select(t => new T[] { t });
            }

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(o => !t.Contains(o)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}