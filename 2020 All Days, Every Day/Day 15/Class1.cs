using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_15
{
    public class Memory
    {
        public int MostRecently = 0;
        public int LessRecently = 0;

        private int valuesRemembered=0;

        public void Remember(int n)
        {
            LessRecently = MostRecently;
            MostRecently = n;
            valuesRemembered++;

    }

    public bool MentionedTwice =>
             valuesRemembered >= 2;

        public int Difference =>
             MostRecently - LessRecently;
    }
}