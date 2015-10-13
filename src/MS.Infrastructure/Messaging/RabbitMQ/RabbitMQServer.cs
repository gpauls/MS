using System;
using NLog;
using RabbitMQ.Client;

namespace MS.Infrastructure.Messaging.RabbitMQ
{
    // ReSharper disable once InconsistentNaming
    public class RabbitMQServer : IDisposable
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;

        public string HostName { get; set; }

        public RabbitMQServer()
        {
            HostName = "localhost";
        }

        public void Startup()
        {
            _connectionFactory = new ConnectionFactory { HostName = HostName };
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        // ReSharper disable once InconsistentNaming
        public RabbitMQClient GetRabbitMQClient()
        {
            return new RabbitMQClient(_channel);
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
                _connection?.Close();
            }
        }
    }
}
