using SampleCodes.Models;

namespace SampleCodes
{
    public class LinqQueries
    {
        Employee _employee;
        Department _department;
        List<Employee> employees;
        List<Employee> newEmployees;
        List<Department> departments;

        public LinqQueries()
        {
            _employee = new Employee();
            _department = new Department();
            employees = _employee.Employees;
            newEmployees = _employee.NewEmployees;
            departments = _department.Departments;
        }

        //Based on Id's
        public List<Employee> GetEmployeesByIDs(List<int> ids)
        {
            var employees = _employee.Employees;

            var empyQuery = (from emp in employees
                             where ids.Contains(emp.Id)
                             select emp).ToList();
            return empyQuery;
        }

        //Inner Join
        public void InnerJoin()
        {
            var empyQuery = (from emp in employees
                             join newemp in newEmployees on emp.Id equals newemp.Id
                             select new
                             {
                                 empId = emp.Id,
                                 empName = emp.Name,
                                 newEmpName = newemp.Name
                             }).ToList();

            var empyQuery1 = employees.Join(
                                        newEmployees,
                                        x => x.Id,
                                        y => y.Id,
                                        (x, y) =>
                                        new
                                        {
                                            empId = x.Id,
                                            empName = x.Name,
                                            newEmpName = y.Name
                                        });

            Console.WriteLine("====================================Inner Join===================================");
            foreach (var emp in empyQuery1)
            {
                Console.WriteLine("{0} {1} {2}", emp.empId, emp.empName, emp.newEmpName);
            }
            Console.WriteLine("====================================END==========================================");
        }

        public void GroupJoin()
        {
            var groupByQuery = (from dept in departments
                                join emp in employees on dept.Id equals emp.Department into g
                                select new
                                {
                                    dep = dept,
                                    emp = g
                                }).ToList();


            var groupByQuery1 = departments.GroupJoin(
                                employees,
                                dep => dep.Id,
                                emp => emp.Department,
                                (dep, emp) =>
                                new
                                {
                                    dep,
                                    emp
                                });

            Console.WriteLine("====================================Group Join===================================");
            foreach (var department in groupByQuery)
            {
                Console.WriteLine("Department : {0}", department.dep.Name);
                foreach (var employee in department.emp)
                {
                    Console.WriteLine("      {0} {1}", employee.Id, employee.Name);
                }
            }
            Console.WriteLine("====================================END==========================================");
        }

        public void LeftOuterJoin()
        {
            var leftJoinQuery = (from emp in employees
                                 join dep in departments on emp.Department equals dep.Id into temp
                                 from c in temp.DefaultIfEmpty()
                                 select new
                                 {
                                     EmpId = emp.Id,
                                     EmployeeName = emp.Name,
                                     Department = (c != null && !string.IsNullOrEmpty(c.Name)) ? c.Name : "Department Not Mapped"
                                 }).ToList();

            Console.WriteLine("====================================Left Outer Join===================================");
            foreach (var emp in leftJoinQuery)
            {
                Console.WriteLine("EmployeeID : {0}, EmployeeName : {1}, Department : {2}", emp.EmpId, emp.EmployeeName, emp.Department);
            }
            Console.WriteLine("====================================END==========================================");
        }

        public void CrossJoin()
        {
            var crossJoinQuery = (from emp in employees
                                  from dept in departments
                                  select new
                                  {
                                      empData = emp,
                                      deptData = dept
                                  }).ToList();

            Console.WriteLine("====================================Cross Join===================================");
            foreach (var dept in crossJoinQuery)
            {
                Console.WriteLine("Department : {0} {1} {2}", dept.empData.Id, dept.empData.Name, dept.deptData.Name);
            }
            Console.WriteLine("====================================END==========================================");
        }

        public void CallAllLinqMethods()
        {
            var first = employees.First(x => x.Name.Contains("ka"));

            var firstOrDefault = employees.FirstOrDefault(x => x.Name.Contains("ka"));

            var single = employees.Single(x => x.Name.Contains("ka"));

            var singleOrDefault = employees.SingleOrDefault(x => x.Name.Contains("ka"));

            var find = employees.Find(x => x.Name.Contains("ka"));

            var where = employees.Where(x => x.Name.Contains("ka"));

            var orderBy = employees.OrderBy(x => x.Name);

            var orderByDescending = employees.OrderByDescending(x => x.Salary);

            var except = employees.Except(newEmployees);

            var intersect=employees.Intersect(newEmployees);
        }
    }
}
