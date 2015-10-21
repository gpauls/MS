using MS.Infrastructure;
using SimpleInjector;

namespace MS.Web
{
    public class Registrar : IRegistrar
    {
        public void Register(Container container)
        {
            container.RegisterMvcControllers(typeof(Web.Controllers.HomeController).Assembly);
            container.RegisterMvcIntegratedFilterProvider();
        }
    }
}
