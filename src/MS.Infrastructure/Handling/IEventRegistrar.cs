using MS.Infrastructure.Messaging;

namespace MS.Infrastructure.Handling
{
    public interface IEventRegistrar
    {
        void RegisterEvent<T>() where T : class, IEvent;
    }
}