using System.Collections.Generic;
using MS.Employees.Model;

namespace MS.Employees.Services
{
    public interface IEmployeeQuery
    {
        Employee Get(long id);
        List<Employee> List();
    }
}