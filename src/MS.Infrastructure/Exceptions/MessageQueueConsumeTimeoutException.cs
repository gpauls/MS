using System;

namespace MS.Infrastructure.Exceptions
{
    // ReSharper disable once InconsistentNaming
    public class MessageQueueConsumeTimeoutException : Exception
    {
        public MessageQueueConsumeTimeoutException(string message) : base(message)
        {
        }
    }
}
