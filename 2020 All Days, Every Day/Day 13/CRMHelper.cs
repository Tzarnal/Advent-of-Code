using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Day_13
{
    public static class CRMHelper
    {
        public static long ModInverse(long a, long n)
        {
            return (long)BigInteger.ModPow(a, n - 2, n);
        }

        public static long ModInverse(int a, int n)
        {
            return (int)ModInverse((long)a,
                              (long)n);
        }
    }
}