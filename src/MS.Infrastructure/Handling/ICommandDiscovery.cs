namespace MS.Infrastructure.Handling
{
    public interface ICommandDiscovery
    {
        void Register(ICommandRegistrar registrar);
    }
}
