using System.Collections.Generic;
using MS.Infrastructure.ErrorHandling;

namespace MS.Infrastructure.Messaging
{
    public interface ICommandResponse
    {
        bool Succeeded { get; set; }
        IEnumerable<Error> Errors { get; set; }
    }
}
