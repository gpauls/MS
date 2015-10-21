namespace MS.Infrastructure.ErrorHandling
{
    public class GenericError : Error
    {
        public static Error UnknownError => new GenericError("UnknownError");
        public static Error ItemNotFound => new GenericError("ItemNotFound");

        public GenericError(string value) : base(value)
        {
        }
    }
}
