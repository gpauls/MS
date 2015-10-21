using System.Collections.Generic;
using System.Linq;
using MS.Employees.Model;

namespace MS.Employees.Services
{
    public class EmployeeQuery : IEmployeeQuery
    {
        private readonly EmployeeContext _employeeContext;

        public EmployeeQuery()
        {
            _employeeContext = EmployeeContextLocator.EmployeeContext;
        }

        public List<Employee> List()
        {
            return _employeeContext.Employees.ToList();
        }

        public Employee Get(long id)
        {
            return _employeeContext.Employees.FirstOrDefault(e => e.Id == id);
        }
    }
}
