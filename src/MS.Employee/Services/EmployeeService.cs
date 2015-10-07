using System.Collections.Generic;
using System.Linq;

namespace MS.Employee.Services
{
    public class EmployeeService : IEmployeeService
    {
        private static readonly List<Model.Employee> Employees = new List<Model.Employee>();

        public void Create(Model.Employee employee)
        {
            employee.Id = Employees.Select(e => e.Id).OrderBy(i => i).Last() + 1;
            Employees.Add(employee);
        }
    }
}
