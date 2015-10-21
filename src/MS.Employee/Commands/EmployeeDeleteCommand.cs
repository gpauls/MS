using MS.Infrastructure.Messaging;

namespace MS.Employees.Commands
{
    public class EmployeeDeleteCommand : ICommandReturns<EmployeeDeleteResponse>
    {
        public long Id { get; set; }
    }

    public class EmployeeDeleteResponse : ResponseBase
    {
    }
}
