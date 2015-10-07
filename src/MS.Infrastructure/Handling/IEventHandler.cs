using MS.Infrastructure.Messaging;

namespace MS.Infrastructure.Handling
{
    public interface IEventHandler { }

    public interface IEventHandler<in T> : IEventHandler
        where T : IEvent
    {
        void Handle(T @event);
    }
}
