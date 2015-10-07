using MS.Infrastructure.Messaging.RabbitMQ;

namespace MS.Infrastructure.Messaging
{
    public class CommandBus : ICommandBus
    {
        private readonly RabbitMQServer _rabbitMqServer;

        public CommandBus(RabbitMQServer rabbitMqServer)
        {
            _rabbitMqServer = rabbitMqServer;
        }

        public void Execute<T>(T command) where T : class, ICommand
        {
            var client = _rabbitMqServer.GetRabbitMQClient();
            client.Publish(MessageFactory.CreateFromCommand(command));
        }

        public TResponse Execute<TCommand, TResponse>(TCommand command) where TCommand : class, ICommandReturns<TResponse> where TResponse : class
        {
            var client = _rabbitMqServer.GetRabbitMQClient();
            var message = client.Call(MessageFactory.CreateFromCommandReturns<TCommand, TResponse>(command));
            return message.GetBody<TResponse>();
        }
    }
}
