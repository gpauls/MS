﻿using MS.Infrastructure.Messaging;

namespace MS.Infrastructure.Handling
{
    public interface ICommandRegistrar
    {
        void RegisterCommand<T>() where T : class, ICommand;

        void RegisterCommandResponse<TRequest, TResponse>()
            where TRequest : class, ICommand, ICommandReturns<TResponse>
            where TResponse : class;
    }
}
