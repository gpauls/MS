namespace MS.Infrastructure.Messaging
{
    public interface IEventBus
    {
        void Publish<T>(T _event) where T : class, IEvent;
    }
}
