using MS.Infrastructure.ErrorHandling;

namespace MS.Employee.ErrorHandling
{
    public class EmployeeError : Error
    {
        public static readonly Error EmailAlreadyExists = new EmployeeError("EmailAlreadyExists");

        public EmployeeError(string value) : base(value)
        {
        }
    }
}
