using System;

namespace MS.Infrastructure.Messaging
{
    public interface IMessageChannel : IDisposable
    {
        void Startup();
    }
}
