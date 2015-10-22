using System;
using MS.Infrastructure;
using MS.Infrastructure.Messaging;

namespace MS.Employees.Worker.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            MSContext.Initialize(new Registrar(),
                new Infrastructure.Messaging.Registrar(),
                new Infrastructure.Handling.Registrar(),
                new Employees.Registrar(),
                new Employees.Worker.Registrar());
            MSContext.Verify();

            var messageChannel = MSContext.Resolve<IMessageChannel>();

            messageChannel.Startup();
            Console.WriteLine("Started worker...");
            Console.WriteLine("Press key to exit!");
            Console.ReadLine();
            messageChannel.Dispose();
        }
    }
}
