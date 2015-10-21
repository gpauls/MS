using System;
using MS.Infrastructure.Handling;
using MS.Infrastructure.Messaging.RabbitMQ;
using SimpleInjector;

namespace MS.Infrastructure.Messaging
{
    public class MessageChannel : IMessageChannel
    {
        private readonly Container _container;
        private readonly RabbitMQServer _server;
        private readonly ICommandDiscovery _commandDiscovery;
        private readonly IEventDiscovery _eventDiscovery;
        
        public MessageChannel(Container container, RabbitMQServer server, ICommandDiscovery commandDiscovery, IEventDiscovery eventDiscovery)
        {
            _container = container;
            _server = server;
            _commandDiscovery = commandDiscovery;
            _eventDiscovery = eventDiscovery;
        }

        public void Startup()
        {
            var registrar = new Handling.CommandEventRegistrar(_server, _container);
            _commandDiscovery.Register(registrar);
            _eventDiscovery.Register(registrar);

            _server.Startup();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _server?.Dispose();
            }
        }
    }
}