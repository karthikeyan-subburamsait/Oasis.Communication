using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCodes
{
    public class DesignPatterns
    {
        public class AbstractFactoryPattern
        {
            //Abstract Product
            public interface IInsurance
            {
                string Name { get; }
            }

            //Concrete Product
            public class CarInsurance : IInsurance
            {
                public string Name => "Car Insurance";
            }

            public class BikeInsurance : IInsurance
            {
                public string Name => "Bike Insurance";
            }

            //Abstract Factory
            public interface IInsuranceFactory
            {
                IInsurance CreateInsurance();
            }

            //Concrete Factory
            public class CarInsuranceFactory : IInsuranceFactory
            {
                public IInsurance CreateInsurance()
                {
                    return new CarInsurance();
                }
            }

            public class BikeInsuranceFactory : IInsuranceFactory
            {
                public IInsurance CreateInsurance()
                {
                    return new BikeInsurance();
                }
            }

            //Client
            public class AbstractFactoryPatternClient
            {
                public void GetInsurance()
                {
                    IInsuranceFactory carInsuranceFactory = new CarInsuranceFactory();
                    IInsurance carInsurance = carInsuranceFactory.CreateInsurance();
                    Console.WriteLine("Insurance Type : " + carInsurance.Name);

                    IInsuranceFactory bikeInsuranceFactory = new BikeInsuranceFactory();
                    IInsurance bikeInsurance = bikeInsuranceFactory.CreateInsurance();
                    Console.WriteLine("Insurance Type : " + bikeInsurance.Name);
                }
            }
        }

        public class SingletonPattern
        {
            //Without thread safety
            public sealed class SingletonClass
            {
                private SingletonClass() { }
                private static SingletonClass _instance = null;
                public static SingletonClass Instance
                {
                    get
                    {
                        if (_instance == null)
                            _instance = new SingletonClass();
                        return _instance;
                    }
                }
            }

            //with thread safe
            public sealed class SingletonClass1
            {
                private SingletonClass1() { }
                private static SingletonClass1 _instance = null;
                private static object singletonLock = new object();
                public static SingletonClass1 Instance
                {
                    get
                    {
                        lock (singletonLock)
                        {
                            if (_instance == null)
                                _instance = new SingletonClass1();
                            return _instance;
                        }
                    }
                }
            }

            //with thread safe and double null check
            public sealed class SingletonClass2
            {
                private SingletonClass2() { }
                private static SingletonClass2 _instance = null;
                private static readonly object singletonLock = new object();
                public static SingletonClass2 Instance
                {
                    get
                    {
                        if (_instance == null)
                        {
                            lock (singletonLock)
                            {
                                if (_instance == null)
                                    _instance = new SingletonClass2();
                            }
                        }
                        return _instance;
                    }
                }
            }

            //with lazy loading
            public sealed class SingletonClass3
            {
                private SingletonClass3() { }
                private static readonly Lazy<SingletonClass3> lazyObject = new Lazy<SingletonClass3>(
                    () => new SingletonClass3());
                public static SingletonClass3 Instance
                {
                    get
                    {
                        return lazyObject.Value;
                    }
                }
            }
        }
    }
}
