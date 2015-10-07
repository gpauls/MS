using MS.Employee.Commands;
using MS.Employee.Services;
using MS.Infrastructure.Handling;

namespace MS.Employee.Worker
{
    public class EmployeeHandler : ICommandHandlerResponse<CreateEmployeeCommand, CreateEmployeeResponse>
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public CreateEmployeeResponse Handle(CreateEmployeeCommand command)
        {
            // TODO validation

            var employee = new Model.Employee // TODO mapper
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email
            };

            _employeeService.Create(employee);

            return new CreateEmployeeResponse {Succeeded = true};
        }
    }
}
