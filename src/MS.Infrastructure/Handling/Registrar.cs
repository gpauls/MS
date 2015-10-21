using SimpleInjector;

namespace MS.Infrastructure.Handling
{
    public class Registrar : IRegistrar
    {
        public void Register(Container container)
        {
            container.RegisterSingleton<IEventDiscovery, EventDiscovery>();
            container.RegisterSingleton<ICommandDiscovery, CommandDiscovery>();
        }
    }
}
