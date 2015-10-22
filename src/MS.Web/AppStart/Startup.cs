using MS.Infrastructure.Messaging;
using MS.Web.Infrastructure;

namespace MS.Web.AppStart
{
    public class Boot
    {
        public static void AppStart()
        {
            StartMessageChannel();
        }

        private static void StartMessageChannel()
        {
            var messageChannel = MSMvcContext.Resolve<IMessageChannel>();
            messageChannel.Startup();
        }
    }
}
