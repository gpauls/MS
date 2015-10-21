using MS.Infrastructure.ErrorHandling;

namespace MS.Employees.ErrorHandling
{
    public class EmployeeError : Error
    {
        public static Error EmailAlreadyExists => new EmployeeError("EmailAlreadyExists");
        public static Error EmployeeDoesntExist => new EmployeeError("EmployeeDoesntExist");

        public EmployeeError(string value) : base(value)
        {
        }
    }
}
