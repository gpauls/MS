using MS.Infrastructure.Messaging.RabbitMQ;
using SimpleInjector;

namespace MS.Infrastructure.Messaging
{
    public class Registrar : IRegistrar
    {
        public void Register(Container container)
        {
            container.RegisterSingleton(() =>
            {
                var rabbitMqServer = new RabbitMQServer();
                // TODO set hostname
                rabbitMqServer.Startup();
                return rabbitMqServer;
            });

            container.RegisterSingleton<ICommandBus, CommandBus>();
            container.RegisterSingleton<IEventBus, EventBus>();
            container.RegisterSingleton<IMessageChannel, MessageChannel>();
        }
    }
}
