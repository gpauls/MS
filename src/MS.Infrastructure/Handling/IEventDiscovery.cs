namespace MS.Infrastructure.Handling
{
    public interface IEventDiscovery
    {
        void Register(IEventRegistrar registrar);
    }
}
