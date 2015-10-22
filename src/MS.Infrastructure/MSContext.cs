using System;
using SimpleInjector;

namespace MS.Infrastructure
{
    // ReSharper disable once InconsistentNaming
    public static class MSContext
    {
        public static Container Container { get; private set; }
        private static readonly object Lock = new object();

        public static void Initialize(params IRegistrar[] registrars)
        {
            lock (Lock)
            {
                if (Container != null) throw new Exception("The context was already initialized!");
                Container = new Container();
                //_container.Options.AllowOverridingRegistrations = true;
                foreach (var registrar in registrars)
                {
                    registrar.Register(Container);
                }
            }
        }

        public static void Verify()
        {
            Container.Verify();
        }

        public static T Resolve<T>() where T : class
        {
            EnsureInitialized();
            return Container.GetInstance<T>();
        }
        
        private static void EnsureInitialized()
        {
            if (Container == null) throw new Exception("The YenContext has not been initialized.");
        }
    }
}
