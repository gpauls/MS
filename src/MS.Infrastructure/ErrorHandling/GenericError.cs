namespace MS.Infrastructure.ErrorHandling
{
    public class GenericError : Error
    {
        public static readonly Error UnknownError = new GenericError("UnknownError");
        public static readonly Error ItemNotFound = new GenericError("ItemNotFound");

        public GenericError(string value) : base(value)
        {
        }
    }
}
