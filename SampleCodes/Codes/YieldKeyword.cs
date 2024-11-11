using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCodes
{
    public class YieldKeyword
    {
        List<int> ints = new List<int>();
        private void LoadList()
        {
            ints.Add(0);
            ints.Add(1);
            ints.Add(2);
            ints.Add(3);
            ints.Add(4);
        }

        private IEnumerable<int> Filter()
        {           
            foreach (int i in ints)
            {
                if (i > 2)
                {
                    yield return i;
                }
            }
        }

        public void ProcessYield()
        {
            LoadList();
            foreach (int i in Filter())
            {
                Console.WriteLine(i);
            }
        }
    }
}
