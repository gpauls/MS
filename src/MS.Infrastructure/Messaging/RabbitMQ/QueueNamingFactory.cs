namespace MS.Infrastructure.Messaging.RabbitMQ
{
    public class QueueNamingFactory
    {
        private const string QueuePrefix = "MS.";
        private const string CommandPrefix = "Command.";
        private const string EventPrefix = "Event.";
        private const string CallBackPrefix = "CallBack.";

        public static string GetCommandQueue<T>() where T : class, ICommand
        {
            return QueuePrefix + CommandPrefix + typeof(T);
        }

        public static string GetEventQueue<T>() where T : class, IEvent
        {
            return QueuePrefix + EventPrefix + typeof(T);
        }

        public static string GetCallBackQueue<TCommand, TResponse>() where TCommand : class, ICommand where TResponse : class
        {
            return QueuePrefix + CommandPrefix + CallBackPrefix + $"{typeof(TCommand)}.{typeof (TResponse)}";
        }
    }
}
