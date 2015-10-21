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
            container.Register<ICommandHandlerResponse<EmployeeCreateCommand, EmployeeCreateResponse>, EmployeeHandler>();
            container.Register<ICommandHandlerResponse<EmployeeUpdateCommand, EmployeeUpdateResponse>, EmployeeHandler>();
            container.Register<ICommandHandlerResponse<EmployeeDeleteCommand, EmployeeDeleteResponse>, EmployeeHandler>();
        }
    }
}
