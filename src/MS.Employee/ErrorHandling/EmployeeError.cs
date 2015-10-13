using MS.Infrastructure.ErrorHandling;

namespace MS.Employees.ErrorHandling
{
    public class EmployeeError : Error
    {
        public static readonly Error EmailAlreadyExists = new EmployeeError("EmailAlreadyExists");

        public EmployeeError(string value) : base(value)
        {
        }
    }
}
