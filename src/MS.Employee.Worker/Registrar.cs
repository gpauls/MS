using MS.Employees.Commands;
using MS.Infrastructure;
using MS.Infrastructure.Handling;
using SimpleInjector;

namespace MS.Employees.Worker
{
    public class Registrar : IRegistrar
    {
        public void Register(Container container)
        {
            container.Register<ICommandHandlerResponse<CreateEmployeeCommand, CreateEmployeeResponse>, EmployeeHandler>();
        }
    }
}
