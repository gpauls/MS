using System.Collections.Generic;

namespace MS.Infrastructure.ErrorHandling
{
    public class Result
    {
        public bool Succeeded { get; set; }
        public virtual IEnumerable<Error> Errors { get; set; }
        
        public Result() { }
        
        protected Result(bool succeeded, IEnumerable<Error> errors)
        {
            Succeeded = succeeded;
            Errors = errors;
        }

        public static Result Success()
        {
            return new Result(true, null);
        }

        public static Result<T> Success<T>(T content)
        {
            return new Result<T>(true, null, content);
        }

        public static Result Failure(IEnumerable<Error> errors)
        {
            return new Result(false, errors);
        }
    }

    public class Result<T> : Result
    {
        public T Content { get; set; }

        protected internal Result(bool succeeded, IEnumerable<Error> errors, T content) : base(succeeded, errors)
        {
            Content = content;
        }
    }
}
