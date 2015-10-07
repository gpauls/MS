using MS.Infrastructure.Messaging;

namespace MS.Employee.Commands
{
    public class CreateEmployeeCommand : ICommandReturns<CreateEmployeeResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class CreateEmployeeResponse // TODO come up with neat solution for result handling
    {
        public bool Succeeded { get; set; }
        public string Errors { get; set; }
    }
}
