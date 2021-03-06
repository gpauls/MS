﻿namespace MS.Infrastructure.Messaging.RabbitMQ
{
    public class MessageFactory
    {
        public static Message<T> CreateFromCommand<T>(T command) where T : class, ICommand
        {
            var msg = Message<T>.Create(command);
            msg.Queue = QueueNamingFactory.GetCommandQueue<T>();
            return msg;
        }

        public static Message<TCommand> CreateFromCommandReturns<TCommand, TResponse>(TCommand command) 
            where TCommand : class, ICommandReturns<TResponse> 
            where TResponse : class, ICommandResponse
        {
            var msg = Message<TCommand>.Create(command);
            msg.Queue = QueueNamingFactory.GetCommandQueue<TCommand>();
            msg.CallBackQueue = QueueNamingFactory.GetCallBackQueue<TCommand, TResponse>();
            return msg;
        }

        public static Message<TResponse> CreateResponse<TResponse>(Message commandMessage, TResponse response)
        {
            var msg = Message<TResponse>.Create(response);
            msg.Queue = commandMessage.CallBackQueue;
            msg.IsResponse = true;
            return msg;
        }

        public static Message<T> CreateFromEvent<T>(T _event) where T : class, IEvent
        {
            var msg = Message<T>.Create(_event);
            msg.Queue = QueueNamingFactory.GetEventQueue<T>();
            return msg;
        }
    }
}
