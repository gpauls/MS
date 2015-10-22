using System;
using System.Linq;
using System.Reflection;
using SimpleInjector;

namespace MS.Infrastructure.Handling
{
    public class CommandDiscovery : ICommandDiscovery
    {
        private readonly Container _container;

        public CommandDiscovery(Container container)
        {
            _container = container;
        }

        public void Register(ICommandRegistrar registrar)
        {
            var registrations = _container.GetCurrentRegistrations();
            var commandHandlers = registrations.Where(r => typeof (ICommandHandler).IsAssignableFrom(r.ServiceType));

            foreach (var commandHandler in commandHandlers)
            {
                var genericArguments = commandHandler.ServiceType.GetGenericArguments().ToList();
                switch (genericArguments.Count())
                {
                    case 1:
                        RegisterCommandHandler(genericArguments[0], registrar);
                        break;
                    case 2:
                        RegisterCommandResponseHandler(genericArguments[0], genericArguments[1], registrar);
                        break;
                    default:
                        throw new Exception("The service " + commandHandler.ServiceType + " must by of type ICommandHandler<T> or ICommandHandler<TRequest, TResponse>");
                }
            }
        }

        private void RegisterCommandHandler(Type type, ICommandRegistrar registrar)
        {
            var methodInfo = typeof(ICommandRegistrar).GetMethod("RegisterCommand", BindingFlags.Public | BindingFlags.Instance).MakeGenericMethod(type);
            methodInfo.Invoke(registrar, new object[0]);
        }

        private void RegisterCommandResponseHandler(Type request, Type response, ICommandRegistrar registrar)
        {
            var methodInfo = typeof(ICommandRegistrar).GetMethod("RegisterCommandResponse", BindingFlags.Public | BindingFlags.Instance).MakeGenericMethod(request, response);
            methodInfo.Invoke(registrar, new object[0]);
        }
    }
}
