using System;

namespace MS.Infrastructure.Exceptions
{
    public class CouldntExtractMessageBodyException : Exception
    {
        public CouldntExtractMessageBodyException()
        {
        }

        public CouldntExtractMessageBodyException(string message) : base(message)
        {
        }
    }
}
