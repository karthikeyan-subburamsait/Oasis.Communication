using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCodes
{
    public class Generics<T>
    {
        public void Concat(T t1)
        {
            Console.WriteLine(t1);
        }
    }
}
