using MS.Employee.Commands;
using MS.Infrastructure;
using MS.Infrastructure.Handling;
using SimpleInjector;

namespace MS.Employee.Worker
{
    public class Registrar : IRegistrar
    {
        public void Register(Container container)
        {
            container.Register<ICommandHandlerResponse<CreateEmployeeCommand, CreateEmployeeResponse>, EmployeeHandler>();
        }
    }
}
