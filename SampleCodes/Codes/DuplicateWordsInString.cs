using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCodes
{
    public static class DuplicateWordsInAString
    {
        public static void GetDuplicateWordsInAString()
        {
            string testString = "Hi Karthikeyan Welcome to c# online editor hi to c#";
            string[] testStringArray = testString.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var duplicateWords = testStringArray.GroupBy(g => g).Select(s => new
            {
                word = s.Key,
                wordCount = s.Count()
            }).Where(x => x.wordCount > 1).ToList();

            foreach (var duplicateWord in duplicateWords)
            {
                Console.WriteLine("Word " + duplicateWord.word + " is replicated " + duplicateWord.wordCount + " times..");
            }
        }
    }

    public static class DuplicateCharactersInAString
    {
        public static void GetDuplicateCharactersInAString()
        {
            string testString = "Hi Karthikeyan, Welcome to c# online editor hi to c#";

            var duplicateCharacters = testString.Replace(" ", "").ToCharArray().GroupBy(g => g).Select(s => new
            {
                character = s.Key,
                characterCount = s.Count()
            }).Where(x => x.characterCount > 1).ToList();

            foreach (var duplicateCharacter in duplicateCharacters)
            {
                Console.WriteLine("Character " + duplicateCharacter.character + " is replicated " + duplicateCharacter.characterCount + " times..");
            }
        }
    }

    public class ArrayMethods
    {
        public static void GetArrayMethods()
        {
            int[] intArray = new int[] { 1, 2, 9, 3, 4, 5, 6, 8, 7 };

            //Sorting Arrays
            Array.Sort(intArray);
            Console.WriteLine("================Sorted Array====================");
            foreach (int i in intArray)
            {
                Console.WriteLine(i.ToString());
            }

            //Reverse Arrays
            Array.Reverse(intArray);
            Console.WriteLine("================Reversed Array====================");
            foreach (int i in intArray)
            {
                Console.WriteLine(i.ToString());
            }

            //Index Of
            int index = Array.IndexOf(intArray, 4);
            Console.WriteLine("================Index Of Array====================");
            Console.WriteLine("Index of item 4 is {0}", index);

            //Clear
            int[] intArrayToClear = new int[] { 1, 2, 9 };
            Console.WriteLine("================Clear Array====================");
            Array.Clear(intArrayToClear, 0, 2);
            foreach (int i in intArrayToClear)
            {
                Console.WriteLine(i.ToString());
            }

            //Resize
            int[] arrayToResize = { 1, 2, 3 };
            Console.WriteLine("================Resized Array====================");
            Array.Resize(ref arrayToResize, 6);
            foreach (int i in arrayToResize)
            {
                Console.WriteLine(i.ToString());
            }

            //Copy
            int[] copiedArray = new int[10];
            Array.Copy(intArray, 4, copiedArray, 2, 5);
            Console.WriteLine("================Copied Array====================");
            foreach (int i in copiedArray)
            {
                Console.WriteLine(i.ToString());
            }

            //Clone
            int[] clonedArray = (int[])intArray.Clone();
            Console.WriteLine("================Cloned Array====================");
            foreach (int i in clonedArray)
            {
                Console.WriteLine(i.ToString());
            }

            //Swap
            int[] arrayToSwap = { 1, 2, 3, 4, 5 };
            (arrayToSwap[3], arrayToSwap[4]) = (arrayToSwap[4], arrayToSwap[3]);
            Console.WriteLine("================Swapped Array====================");
            foreach (int i in arrayToSwap)
            {
                Console.WriteLine(i.ToString());
            }
        }
    }

    public class DuplicateItemsInAList
    {
        public static void GetDuplicateItemsInAList()
        {
            List<int> lst = new List<int>
                {
                    1,2,3,2,3,5,6,7,5,1,1,2,2,2,2,3,7,6
                };

            foreach (int i in lst)
            {
                Console.WriteLine(i);
            }

            var duplicates = lst.GroupBy(x => x).Where(g => g.Count() > 1).Select(g => g.Key);
            Console.WriteLine(string.Join(", ", duplicates));

            var duplicates1 = lst.GroupBy(x => x).Select(g => new
            {
                item = g.Key,
                count = g.Count()
            });
            foreach (var d in duplicates1)
            {
                Console.WriteLine("Value : " + d.item + " Count : " + d.count);
            }
        }
    }

    public class GroupJoinInLinq
    {
        public class Employee
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int DepartmentId { get; set; }
            public static List<Employee> GetAllEmployees()
            {
                return new List<Employee>()
            {
                new Employee { ID = 1, Name = "Preety", DepartmentId = 10},
                new Employee { ID = 2, Name = "Priyanka", DepartmentId =20},
                new Employee { ID = 3, Name = "Anurag", DepartmentId = 30},
                new Employee { ID = 4, Name = "Pranaya", DepartmentId = 30},
                new Employee { ID = 5, Name = "Hina", DepartmentId = 20},
                new Employee { ID = 6, Name = "Sambit", DepartmentId = 10},
                new Employee { ID = 7, Name = "Happy", DepartmentId = 10},
                new Employee { ID = 8, Name = "Tarun", DepartmentId = 0},
                new Employee { ID = 9, Name = "Santosh", DepartmentId = 10},
                new Employee { ID = 10, Name = "Raja", DepartmentId = 20},
                new Employee { ID = 11, Name = "Ramesh", DepartmentId = 30}
            };
            }
        }

        public class Department
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public static List<Department> GetAllDepartments()
            {
                return new List<Department>()
            {
                new Department { ID = 10, Name = "IT"},
                new Department { ID = 20, Name = "HR"},
                new Department { ID = 30, Name = "Sales"  },
            };
            }
        }

        public static void Main(string[] args)
        {
            var GroupJoin = Department.GetAllDepartments()
            .GroupJoin(
                Employee.GetAllEmployees(),
                dept => dept.ID,
                emp => emp.DepartmentId,
                (dept, emp) => new { dept, emp }
                );

            foreach (var item in GroupJoin)
            {
                Console.WriteLine("Department :" + item.dept.Name);
                //Inner Foreach loop for each employee of a Particular department
                foreach (var employee in item.emp)
                {
                    Console.WriteLine("  EmployeeID : " + employee.ID + " , Name : " + employee.Name);
                }
            }
        }
    }

    public class ShiftArrayItems
    {
        public static void GetShiftArrayItems()
        {
            int[] o = new int[] { 1, 2, 3, 4, 5 };
            int[] temp = new int[o.Length];
            int currentItemIndex = 0;

            foreach (int i in o)
            {
                currentItemIndex = Array.IndexOf(o, i, 0);
                if (currentItemIndex == o.Length - 1)
                {
                    temp[0] = i;
                }
                else
                {
                    temp[currentItemIndex + 1] = i;
                }
            }

            foreach (int i in temp)
            {
                Console.WriteLine(i);
            }
        }
    }
}


