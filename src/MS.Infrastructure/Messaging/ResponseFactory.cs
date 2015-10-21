using System;
using System.Collections.Generic;
using MS.Infrastructure.ErrorHandling;

namespace MS.Infrastructure.Messaging
{
    public static class ResponseFactory
    {
        public static T Success<T>() where T : class, ICommandResponse
        {
            var response = Activator.CreateInstance<T>();
            response.Succeeded = true;
            return response;
        }

        public static T Failure<T>(Error error) where T : class, ICommandResponse
        {
            return Failure<T>(new[] {error});
        }

        public static T Failure<T>(IEnumerable<Error> errors) where T : class, ICommandResponse
        {
            var response = Activator.CreateInstance<T>();
            response.Succeeded = false;
            response.Errors = errors;
            return response;
        }
    }
}
