using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SampleCodes
{
    public class NormalClass
    {
        public void Add() { }
        public void Subtract() { }
    }

    public static class ExtensionClass
    {
        public static void ExtensionMethod(this NormalClass normalClass)
        {
            
        }

        public static int StringExtensionMethod(this string sampleString)
        {
            return sampleString.Count();
        }
    }
}
