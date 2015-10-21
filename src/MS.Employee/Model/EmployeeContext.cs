using System.Collections.Generic;

namespace MS.Employees.Model
{
    public class EmployeeContext
    {
        public List<Employee> Employees { get; set; }
    }

    public static class EmployeeContextLocator
    {
        private static EmployeeContext _employeeContext;

        public static EmployeeContext EmployeeContext => _employeeContext ?? (_employeeContext = CreateEmployeeContext());

        private static EmployeeContext CreateEmployeeContext()
        {
            var context = new EmployeeContext
            {
                Employees = new List<Employee>
                {
                    new Employee {Id = 1, FirstName = "David", LastName = "De Gea", Email = "david.degea@mu.com"},
                    new Employee {Id = 8, FirstName = "Juan", LastName = "Mata", Email = "jmata8@mu.com"},
                    new Employee {Id = 9, FirstName = "Anthony", LastName = "Martial", Email = "martial9@mu.com"},
                }
            };

            return context;
        }
    }
}
