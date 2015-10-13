using System.Collections.Generic;
using System.Linq;
using MS.Employees.Model;

namespace MS.Employees.Services
{
    public class EmployeeService : IEmployeeService
    {
        private static readonly List<Employee> Employees = new List<Employee>();

        public void Create(Employee employee)
        {
            employee.Id = Employees.Select(e => e.Id).OrderBy(i => i).Last() + 1;
            Employees.Add(employee);
        }
    }
}
