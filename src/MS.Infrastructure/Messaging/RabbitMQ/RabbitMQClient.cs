using System;
using System.Diagnostics;
using System.Text;
using MS.Infrastructure.Exceptions;
using Newtonsoft.Json;
using NLog;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MS.Infrastructure.Messaging.RabbitMQ
{
    // ReSharper disable once InconsistentNaming
    public class RabbitMQClient
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private const string ContentType = "application/json";
        private const int TimeoutMilliseconds = 5000;

        private readonly IModel _channel;

        public RabbitMQClient(IModel channel)
        {
            _channel = channel;
        }

        public void Publish(Message message)
        {
            // TODO validate msg

            EnsureQueueIsCreated(message.Queue);

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;
            properties.ContentType = ContentType;

            var body = SerializeMessage(message);

            _channel.BasicPublish(exchange: "",
                routingKey: message.Queue,
                basicProperties: properties,
                body: body);

            Logger.Debug($"Published message [{message.Id}] to queue: {message.Queue}");
        }

        public Message Call(Message message)
        {
            // TODO validate msg

            EnsureQueueIsCreated(message.Queue);
            EnsureQueueIsCreated(message.CallBackQueue);

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;
            properties.ContentType = ContentType;
            properties.ReplyTo = message.CallBackQueue;
            properties.CorrelationId = message.Id.ToString();

            var body = SerializeMessage(message);

            _channel.BasicPublish(exchange: "",
                routingKey: message.Queue,
                basicProperties: properties,
                body: body);

            var consumer = new QueueingBasicConsumer(_channel);
            _channel.BasicConsume(queue: message.CallBackQueue,
                                  noAck: true,
                                  consumer: consumer);

            var sw = new Stopwatch();
            sw.Start();

            while (true)
            {
                BasicDeliverEventArgs args;
                if (sw.ElapsedMilliseconds > TimeoutMilliseconds || !consumer.Queue.Dequeue(TimeoutMilliseconds, out args))
                {
                    Logger.Debug($"No message received on queue: {message.CallBackQueue}");
                    throw new MessageQueueConsumeTimeoutException($"Timeout while waiting for return message on queue: {message.CallBackQueue}");
                }

                if (args.BasicProperties.CorrelationId == message.Id.ToString())
                {
                    var recMsg = DeSerializeMessage(args.Body);
                    Logger.Debug($"Received message [{recMsg.Id}]");
                    return recMsg;
                }
            }
        }

        public void ConsumeAndRespond(string queue, Func<Message, Message> function)
        {
            var consumer = SetupConsumer(queue);
            consumer.Received += (model, args) =>
            {
                Logger.Debug($"Consumed message from channel: {queue}");
                var message = DeSerializeMessage(args.Body);
                // TODO validate message
                Logger.Debug($"Deserialized message [{message.Id}]");

                var responseMsg = function.Invoke(message);

                // TODO validate responseMsg

                var replyProps = _channel.CreateBasicProperties();
                replyProps.CorrelationId = args.BasicProperties.CorrelationId;
                replyProps.ContentType = ContentType;

                var body = SerializeMessage(responseMsg);

                var replyQueue = args.BasicProperties.ReplyTo;

                _channel.BasicPublish(exchange: "",
                    routingKey: replyQueue,
                    basicProperties: replyProps,
                    body: body);

                Logger.Debug($"Sent response message [{responseMsg.Id}] to queue: {replyQueue}");

                _channel.BasicAck(deliveryTag: args.DeliveryTag, multiple: false);
                Logger.Debug($"Done. Acknowledgement sent. [{message.Id}]");
            };
        }

        public void Consume(string queue, Action<Message> function)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, args) =>
            {
                Logger.Debug($"Consumed message from channel: {queue}");
                var message = DeSerializeMessage(args.Body);
                Logger.Debug($"Deserialized message [{message.Id}]");

                function.Invoke(message);

                _channel.BasicAck(deliveryTag: args.DeliveryTag, multiple: false);
                Logger.Debug($"Done. Acknowledgement sent. [{message.Id}]");
            };
        }

        private EventingBasicConsumer SetupConsumer(string queue)
        {
            EnsureQueueIsCreated(queue);

            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(_channel);
            
            _channel.BasicConsume(queue: queue, noAck: false, consumer: consumer);

            return consumer;
        }

        private void EnsureQueueIsCreated(string queue)
        {
            _channel.QueueDeclare(queue: queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        private byte[] SerializeMessage(Message message)
        {
            var json = JsonConvert.SerializeObject(message);
            return Encoding.UTF8.GetBytes(json);
        }

        private Message DeSerializeMessage(byte[] bytes)
        {
            var json = Encoding.UTF8.GetString(bytes);
            return JsonConvert.DeserializeObject<Message>(json);
        }
    }
}
