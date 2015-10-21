using System.Collections.Generic;
using MS.Infrastructure.ErrorHandling;
using MS.Infrastructure.Messaging;

namespace MS.Employees.Commands
{
    public class EmployeeCreateCommand : ICommandReturns<EmployeeCreateResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class EmployeeCreateResponse : ResponseBase
    {
    }
}
