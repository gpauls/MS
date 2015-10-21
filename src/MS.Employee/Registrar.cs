using MS.Employees.Services;
using MS.Infrastructure;
using SimpleInjector;

namespace MS.Employees
{
    public class Registrar : IRegistrar
    {
        public void Register(Container container)
        {
            container.Register<IEmployeeQuery, EmployeeQuery>();
        }
    }
}
