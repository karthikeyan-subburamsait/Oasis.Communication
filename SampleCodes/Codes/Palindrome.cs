using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCodes
{
    public static class Palindrome
    {
        private static string sampleString = "MALAYALAM";
        private static string reverseString = string.Empty;

        public static void IsPalindrome()
        {
            char[] chars = sampleString.ToCharArray();
            Array.Reverse(chars);
            reverseString = new string(chars);
            Console.Write(reverseString);
            Console.WriteLine("IsPalindrome : ", sampleString.Equals(reverseString, StringComparison.OrdinalIgnoreCase) ? "True" : "False");
        }
    }
}
