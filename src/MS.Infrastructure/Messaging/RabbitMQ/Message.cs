using System;
using MS.Infrastructure.Exceptions;

namespace MS.Infrastructure.Messaging.RabbitMQ
{
    public class Message
    {
        public Guid Id { get; set; }
        public DateTime TimestampCreated { get; set; }
        public object Body { get; set; }
        public string BodyType { get; set; }
        
        public string Queue { get; set; }
        public string CallBackQueue { get; set; }
        public bool IsResponse { get; set; }
        
        public Message()
        {
            Id = Guid.NewGuid();
            TimestampCreated = DateTime.UtcNow;
        }

        public T GetBody<T>() where T : class
        {
            var cBody = Body as T;
            if (cBody == null)
            {
                throw new CouldntExtractMessageBodyException();
            }
            return cBody;
        }
    }

    public class Message<T> : Message
    {
        public Message(T body)
        {
            Body = body;
        }

        public static Message<T> Create(T body)
        {
            return new Message<T>(body);
        }

        public T GetBody()
        {
            return (T) Body;
        }
    }
}
