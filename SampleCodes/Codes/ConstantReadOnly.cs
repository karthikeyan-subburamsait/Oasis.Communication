using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCodes
{
    public class ConstantReadOnly
    {
        public const int Constant = 10;
        public readonly int ReadOnly = 20;
        public static readonly int StaticReadOnly = 30;

        ConstantReadOnly() 
        {
            //Constant = 20;
            ReadOnly = 30;
            //StaticReadOnly = 40;
        }

        static ConstantReadOnly()
        {
            //Constant = 20;
            //ReadOnly = 30;
            StaticReadOnly = 40;
        }
    }
}
