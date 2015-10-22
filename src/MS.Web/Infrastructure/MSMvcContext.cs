using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;
using MS.Infrastructure;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using SimpleInjector.Extensions.ExecutionContextScoping;

namespace MS.Web.Infrastructure
{
    // ReSharper disable once InconsistentNaming
    public static class MSMvcContext
    {
        private static Container _container;
        private static readonly object Lock = new object();

        public static void Initialize()
        {
            if (_container != null) throw new Exception("The context was already initialized!");
            _container = new Container();
            //_container.Options.AllowOverridingRegistrations = true;
        }

        public static void Register(params IRegistrar[] registrars)
        {
            lock (Lock)
            {
                foreach (var registrar in registrars)
                {
                    registrar.Register(_container);
                }
            }
        }

        public static void Verify()
        {
            _container.Verify();
        }

        public static T Resolve<T>() where T : class
        {
            EnsureInitialized();
            return _container.GetInstance<T>();
        }

        private static void EnsureInitialized()
        {
            if (_container == null) throw new Exception("The MSContext has not been initialized.");
        }

        public static void RegisterControllers(IApplicationBuilder app)
        {
            // Register ASP.NET controllers
            var provider = app.ApplicationServices.GetRequiredService<IControllerTypeProvider>();
            foreach (var type in provider.ControllerTypes)
            {
                var registration = Lifestyle.Transient.CreateRegistration(type, _container);
                _container.AddRegistration(type, registration);
                registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "ASP.NET disposes controllers.");
            }
        }

        public static Scope BeginExecutionContextScope()
        {
            return _container.BeginExecutionContextScope();
        }

        public static IControllerActivator CreateControllerActivator()
        {
            return new SimpleInjectorControllerActivator(_container);
        }
    }
}
