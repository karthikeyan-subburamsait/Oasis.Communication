using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCodes
{
    public static class _2DTo1DArray
    {
        public static int[,] twoDArray = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 }, { 10, 11, 12 } };

        public static void Convert2DTo1DArrayRowWise()
        {
            int[] oneDArray = new int[twoDArray.GetLength(0) * twoDArray.GetLength(1)];
            int k = 0;

            for (int i = 0; i < twoDArray.GetLength(0); i++)
            {
                for (int j = 0; j < twoDArray.GetLength(1); j++)
                {
                    oneDArray[k++] = twoDArray[i, j];
                }
            }

            Console.WriteLine("===============================Rowwise 1D Array========================================");
            foreach (int i in oneDArray)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("===============================END=====================================================");
        }

        public static void Convert2DTo1DArrayColumnWise()
        {
            int[] oneDArray = new int[twoDArray.GetLength(0) * twoDArray.GetLength(1)];
            int k = 0;

            for (int i = 0; i < twoDArray.GetLength(1); i++)
            {
                for (int j = 0; j < twoDArray.GetLength(0); j++)
                {
                    oneDArray[k++] = twoDArray[j, i];
                }
            }

            Console.WriteLine("===============================Columnwise 1D Array========================================");
            foreach (int i in oneDArray)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("===============================END=====================================================");
        }
    }
}
