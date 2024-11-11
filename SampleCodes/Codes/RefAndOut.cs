using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCodes
{
    public class RefAndOut
    {
        public void CallRefAndOut()
        {
            int num1 = 30;
            int num2 = 40;
            int refData = 0;//Need to set some value    
            RefParams(num1, num2, ref refData);

            int num3 = 10;
            int num4 = 20;
            int outData;//No need to set any value         
            OutParams(num3, num4, out outData);
            Console.WriteLine("Out result Now : {0}", outData);
        }

        public void RefParams(int num1, int num2, ref int refData)
        {
            refData = num1 + num2;
        }

        public void OutParams(int num3, int num4, out int outData)
        {
            outData = num3 + num4;//Need to assign/update value to out paramter before leaving the method
        }
    }
}
