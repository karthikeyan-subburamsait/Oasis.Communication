using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCodes
{
    public class SolidPrinciples
    {
        //Before
        //public class LiskovPrinciple
        //{
        //    public class Apple
        //    {
        //        public virtual string GetColor()
        //        {
        //            return "Red"; 
        //        }
        //    }

        //    public class Orange:Apple
        //    {
        //        public override string GetColor()
        //        {
        //            return "Orange";
        //        }
        //    }
        //}

        public interface IColor
        {
            string GetColor();
        }

        public class LiskovPrinciple
        {
            public class Apple:IColor
            {
                public string GetColor()
                {
                    return "Red";
                }
            }

            public class Orange : IColor
            {
                public string GetColor()
                {
                    return "Orange";
                }
            }
        }
    }
}
