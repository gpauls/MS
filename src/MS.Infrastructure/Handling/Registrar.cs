using MS.Infrastructure.Messaging;
using MS.Infrastructure.Messaging.RabbitMQ;

namespace MS.Infrastructure.Handling
{
    public class Registrar : ICommandRegistrar, IEventRegistrar
    {
        private readonly RabbitMQServer _rabbitMqServer;

        public Registrar(RabbitMQServer rabbitMqServer)
        {
            _rabbitMqServer = rabbitMqServer;
        }

        public void RegisterCommand<T>() where T : class, ICommand
        {
            var client = _rabbitMqServer.GetRabbitMQClient();
            ICommandHandler<T> handler = (ICommandHandler<T>)new object(); // TODO get handler with DI
            client.Consume(QueueNamingFactory.GetCommandQueue<T>(), msg => handler.Handle(msg.GetBody<T>()));
        }

        public void RegisterCommandResponse<TCommand, TResponse>() where TCommand : class, ICommand, ICommandReturns<TResponse> where TResponse : class
        {
            var client = _rabbitMqServer.GetRabbitMQClient();


            ICommandHandlerResponse<TCommand, TResponse> handler = (ICommandHandlerResponse<TCommand, TResponse>)new object(); // TODO get handler with DI
            

            client.Consume(QueueNamingFactory.GetCommandQueue<T>(), msg => handler.Handle(msg.GetBody<T>()));
        }

        public void RegisterEvent<T>() where T : class, IEvent
        {
            throw new System.NotImplementedException();
        }
    }
}
