using MS.Employee.Services;
using MS.Infrastructure;
using SimpleInjector;

namespace MS.Employee
{
    public class Registrar : IRegistrar
    {
        public void Register(Container container)
        {
            container.Register<IEmployeeService, EmployeeService>();
        }
    }
}
