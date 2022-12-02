using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public static class MyMath
    {
        public static double GetSin(double angle)
        {
            var rad = angle / 57.2958;
            double sin = 0;
            int pow = 1;

            for (int i = 0; i < 10; i++)
            {
                int k = 2 * i + 1;
                sin += Math.Pow((-1), i) * (Math.Pow(rad, k) / Factorial(k));
            }

            return Math.Round(sin, 5);
        }

        public static int Factorial(int value)
        {
            int res = 1;

            for (int i = 1; i <= value; i++)
            {
                res *= i;
            }

            return res;
        }
    }
}
