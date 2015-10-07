namespace MS.Infrastructure.Messaging
{
    public interface ICommandBus
    {
        void Execute<T>(T command) where T : class, ICommand;

        TResponse Execute<TRequest, TResponse>(TRequest command)
            where TRequest : class, ICommandReturns<TResponse>
            where TResponse : class;
    }
}
