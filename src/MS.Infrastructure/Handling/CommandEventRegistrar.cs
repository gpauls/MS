using MS.Infrastructure.Messaging;
using MS.Infrastructure.Messaging.RabbitMQ;
using SimpleInjector;

namespace MS.Infrastructure.Handling
{
    public class CommandEventRegistrar : ICommandRegistrar, IEventRegistrar
    {
        private readonly RabbitMQServer _rabbitMqServer;
        private readonly Container _container;

        public CommandEventRegistrar(RabbitMQServer rabbitMqServer, Container container)
        {
            _rabbitMqServer = rabbitMqServer;
            _container = container;
        }

        public void RegisterCommand<T>() where T : class, ICommand
        {
            var client = _rabbitMqServer.GetRabbitMQClient();
            var commandHandler = _container.GetInstance<ICommandHandler<T>>();
            client.Consume(QueueNamingFactory.GetCommandQueue<T>(), msg => commandHandler.Handle(msg.GetBody<T>()));
        }

        public void RegisterCommandResponse<TCommand, TResponse>() where TCommand : class, ICommand, ICommandReturns<TResponse> where TResponse : class, ICommandResponse
        {
            var client = _rabbitMqServer.GetRabbitMQClient();
            var commandHandler = _container.GetInstance<ICommandHandlerResponse<TCommand, TResponse>>();
            client.ConsumeAndRespond(QueueNamingFactory.GetCommandQueue<TCommand>(), msg =>
            {
                var response = commandHandler.Handle(msg.GetBody<TCommand>());
                return MessageFactory.CreateResponse(msg, response);
            });
        }

        public void RegisterEvent<T>() where T : class, IEvent
        {
            var client = _rabbitMqServer.GetRabbitMQClient();
            var eventHandler = _container.GetInstance<IEventHandler<T>>();
            client.Consume(QueueNamingFactory.GetEventQueue<T>(), msg => eventHandler.Handle(msg.GetBody<T>()));
        }
    }
}
