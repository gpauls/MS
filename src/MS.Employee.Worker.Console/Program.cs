using MS.Infrastructure;
using MS.Infrastructure.Messaging;

namespace MS.Employee.Worker.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            MSContext.Initialize(new Registrar(),
                new Infrastructure.Messaging.Registrar(), 
                new Employee.Registrar());

            var messageChannel = MSContext.Resolve<IMessageChannel>();

            messageChannel.Startup();
            System.Console.WriteLine("Started worker...");
            System.Console.WriteLine("Press key to exit!");
            System.Console.ReadLine();
            messageChannel.Dispose();
        }
    }
}
