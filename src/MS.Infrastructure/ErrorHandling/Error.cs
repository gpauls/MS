namespace MS.Infrastructure.ErrorHandling
{
    public class Error
    {
        public Error(string value)
        {
            Value = this.GetType() + value;
        }

        public string Value { get; }

        public override string ToString()
        {
            return Value;
        }
    }
}
