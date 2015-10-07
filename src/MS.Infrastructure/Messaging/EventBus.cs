using MS.Infrastructure.Messaging.RabbitMQ;

namespace MS.Infrastructure.Messaging
{
    public class EventBus : IEventBus
    {
        private readonly RabbitMQServer _rabbitMqServer;

        public EventBus(RabbitMQServer rabbitMqServer)
        {
            _rabbitMqServer = rabbitMqServer;
        }

        public void Publish<T>(T _event) where T : class, IEvent
        {
            var client = _rabbitMqServer.GetRabbitMQClient();
            client.Publish(MessageFactory.CreateFromEvent(_event));
        }
    }
}
