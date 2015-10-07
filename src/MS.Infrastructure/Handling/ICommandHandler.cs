using MS.Infrastructure.Messaging;

namespace MS.Infrastructure.Handling
{
    public interface ICommandHandler { }

    public interface ICommandHandler<in TRequest> : ICommandHandler
        where TRequest : ICommand
    {
        void Handle(TRequest command);
    }

    public interface ICommandHandlerResponse<in TRequest, out TResponse> : ICommandHandler
        where TRequest : ICommandReturns<TResponse>
    {
        TResponse Handle(TRequest command);
    }
}
