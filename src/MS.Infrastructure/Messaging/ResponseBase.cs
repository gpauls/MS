using System.Collections.Generic;
using MS.Infrastructure.ErrorHandling;

namespace MS.Infrastructure.Messaging
{
    public abstract class ResponseBase : ICommandResponse
    {
        public bool Succeeded { get; set; }
        public IEnumerable<Error> Errors { get; set; }
    }
}
