using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCodes.Codes
{
    public static class MathPrograms
    {
        public static void PrimeNumber(int number)
        {
            int flag = 0;
            for (int i = 2; i <= number; i++)
            {
                if (number % i == 0)
                {
                    flag++;
                }
                else
                {

                }
            }

            Console.WriteLine("Number {0} is {1} ", number, flag >= 2 ? "Not Prime" : "Prime");
        }

        public static void FibonacciSeries(int len)
        {
            int a = 0, b = 1, c = 0;
            Console.Write("{0} {1}", a, b);
            for (int i = 2; i < len; i++)
            {
                c = a + b;
                Console.Write(" {0}", c);
                a = b;
                b = c;
            }
        }

    }
}
