using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SampleCodes.Codes
{
    public static class ArrayPrograms
    {
        public static void LeftCircularRotation(int[] array)
        {
            int[] updatedArray = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                if (i == 0)
                {
                    updatedArray[array.Length - 1] = array[i];
                }
                else
                {
                    updatedArray[i - 1] = array[i];
                }
            }
            foreach (int i in updatedArray)
            {
                Console.WriteLine(i.ToString());
            }
        }

        public static void RightCircularRotation(int[] array)
        {
            int[] updatedArray = new int[array.Length];//1 2 3 4 5            
            for (int i = 0; i < array.Length; i++)
            {
                if (i == array.Length - 1)
                {
                    updatedArray[0] = array[i];
                }
                else
                {
                    updatedArray[i + 1] = array[i];
                }
            }
            foreach (int i in updatedArray)
            {
                Console.WriteLine(i.ToString());
            }
        }
    }
}
