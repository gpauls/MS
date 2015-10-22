namespace MS.Infrastructure.Messaging.RabbitMQ
{
    public class QueueNamingFactory
    {
        private const string QueuePrefix = "MS";

        public static string GetCommandQueue<T>() where T : class, ICommand
        {
            return $"{QueuePrefix}.Command.{typeof(T).Name}";
        }

        public static string GetEventQueue<T>() where T : class, IEvent
        {
            return $"{QueuePrefix}.Event.{typeof(T).Name}";
        }

        public static string GetCallBackQueue<TCommand, TResponse>() where TCommand : class, ICommand where TResponse : class
        {
            return $"{QueuePrefix}.Command.{typeof(TCommand).Name}.CallBack.{typeof(TResponse).Name}";
        }
    }
}
