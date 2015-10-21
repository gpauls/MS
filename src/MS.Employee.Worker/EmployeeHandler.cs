using System.Linq;
using MS.Employees.Commands;
using MS.Employees.ErrorHandling;
using MS.Employees.Model;
using MS.Infrastructure.Handling;
using MS.Infrastructure.Messaging;

namespace MS.Employees.Worker
{
    public class EmployeeHandler :
        ICommandHandlerResponse<EmployeeCreateCommand, EmployeeCreateResponse>,
        ICommandHandlerResponse<EmployeeUpdateCommand, EmployeeUpdateResponse>,
        ICommandHandlerResponse<EmployeeDeleteCommand, EmployeeDeleteResponse>

    {
        private readonly EmployeeContext _employeeContext;

        public EmployeeHandler()
        {
            _employeeContext = EmployeeContextLocator.EmployeeContext;
        }

        public EmployeeCreateResponse Handle(EmployeeCreateCommand command)
        {
            // TODO validation
            if (!ValidateEmailDoesntExist(command.Email))
            {
                return ResponseFactory.Failure<EmployeeCreateResponse>(EmployeeError.EmailAlreadyExists);
            }
            
            var employee = new Employee // TODO mapper
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email
            };

            _employeeContext.Employees.Add(employee);

            return ResponseFactory.Success<EmployeeCreateResponse>();
        }

        public EmployeeUpdateResponse Handle(EmployeeUpdateCommand command)
        {
            // TODO validation

            var employee = _employeeContext.Employees.FirstOrDefault(e => e.Id == command.Id);
            if (employee == null)
            {
                return ResponseFactory.Failure<EmployeeUpdateResponse>(EmployeeError.EmployeeDoesntExist);
            }

            if (!ValidateEmailDoesntExist(command.Email))
            {
                return ResponseFactory.Failure<EmployeeUpdateResponse>(EmployeeError.EmailAlreadyExists);
            }

            employee.FirstName = command.FirstName;
            employee.LastName = command.LastName;
            employee.Email = command.Email;

            return ResponseFactory.Success<EmployeeUpdateResponse>();
        }

        public EmployeeDeleteResponse Handle(EmployeeDeleteCommand command)
        {
            var employee = _employeeContext.Employees.FirstOrDefault(e => e.Id == command.Id);
            if (employee == null)
            {
                return ResponseFactory.Failure<EmployeeDeleteResponse>(EmployeeError.EmployeeDoesntExist);
            }

            _employeeContext.Employees.Remove(employee);

            return ResponseFactory.Success<EmployeeDeleteResponse>();
        }

        private bool ValidateEmailDoesntExist(string email)
        {
            return _employeeContext.Employees.All(e => e.Email != email);
        }
    }
}
