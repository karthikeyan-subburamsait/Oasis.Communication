namespace SampleCodes.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Department { get; set; }
        public decimal Salary { get; set; }

        public List<Employee> Employees
        {
            get
            {
                return new List<Employee>()
                {
                    new Employee{Id=1,Name="karthik", Department=2, Salary=5000},
                    new Employee{Id=2,Name="keyan", Department=3, Salary=8000},
                    new Employee{Id=3,Name="sachin", Department=1, Salary=5800},
                    new Employee{Id=4,Name="sehwag", Department=3, Salary=4000},
                    new Employee{Id=5,Name="ganguly", Department=2, Salary=7000},
                    new Employee{Id=6,Name="zaheer", Department=1, Salary=5090},
                    new Employee{Id=7,Name="rohit", Department=3, Salary=7800},
                    new Employee{Id=8,Name="virat", Department=1, Salary=5090},
                    new Employee{Id=9,Name="kohli", Department=2, Salary=17000},
                    new Employee{Id=10,Name="bumrah", Department=1, Salary=9090},
                    new Employee{Id=11,Name="Pandya", Department=5, Salary=17000},
                    new Employee{Id=12,Name="Ashwin", Department=4, Salary=9090}
                };
            }
        }

        public List<Employee> NewEmployees
        {
            get
            {
                return new List<Employee>()
                {
                    new Employee{Id=1,Name="Karthikeyan SubburamSait", Department=2, Salary=5000},
                    new Employee{Id=2,Name="keyan s", Department=3, Salary=8000},
                    new Employee{Id=3,Name="sachin Tendulakar", Department=1, Salary=5800},
                    new Employee{Id=4,Name="virendar sehwag", Department=2, Salary=4000},
                    new Employee{Id=5,Name="sourav ganguly", Department=1, Salary=7000}
                };
            }
        }
    }

    public class Department
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public List<Department> Departments
        {
            get
            {
                return new List<Department>()
                {
                    new Department{Id=1,Name="IT"},
                    new Department{Id=2,Name="Manufacturing"},
                    new Department{Id=3,Name="Bank"}
                };
            }
        }
    }
}

