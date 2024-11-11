using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCodes.Codes
{
    public static class StringPrograms
    {
        public static void StringReverse(string str)
        {
            if (str == null)
            {
                Console.WriteLine("String is null");
            }

            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            Console.WriteLine(new string(charArray));
        }

        public static void ReverseOrderOfWordsInAString(string sentence)
        {
            string[] stringArray = sentence.Split(" ");
            Array.Reverse(stringArray);
            Console.WriteLine(string.Join(" ", stringArray));
        }

        public static void ReverseEachWordsInAString(string sentence)
        {
            string[] stringArray = sentence.Split(" ");
            string sb = string.Empty;
            foreach (string str in stringArray)
            {
                char[] chars = str.ToCharArray();
                Array.Reverse((char[])chars);
                sb += new string(chars) + " ";
            }
            Console.WriteLine(sb);
        }

        public static void OccuranceOfEachCharacterInAString(string sentence)
        {
            var charArray = sentence.ToCharArray().GroupBy(x => x).Select(k => new
            {
                character = k.Key,
                characterCount = k.Count()
            });

            foreach (var c in charArray)
            {
                Console.WriteLine("Character {0} , Count {1}", c.character, c.characterCount);
            }
        }

        public static void RemoveDuplicateCharactersFromAString(string sentence)
        {
            char[] charArray = sentence.ToCharArray();
            char[] updatedCharArray = new char[sentence.Length];
            int i = 0;
            foreach (char c in charArray)
            {
                if (!updatedCharArray.Contains(c) || c.ToString().Equals(" "))
                {
                    updatedCharArray[i] = c;
                    i = i + 1;
                }
            }
            Console.WriteLine(new string(updatedCharArray));
        }

        public static void PossibleSubStringOfAnGivenString(string sentence)
        {
            
        }
    }
}
