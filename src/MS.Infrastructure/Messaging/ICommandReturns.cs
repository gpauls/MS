namespace MS.Infrastructure.Messaging
{
    public interface ICommandReturns<T> : ICommand where T : class, ICommandResponse
    {

    }
}
