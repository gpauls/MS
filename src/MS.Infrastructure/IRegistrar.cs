using SimpleInjector;

namespace MS.Infrastructure
{
    public interface IRegistrar
    {
        void Register(Container container);
    }
}
