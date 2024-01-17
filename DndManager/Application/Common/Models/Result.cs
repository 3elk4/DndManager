using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class Result
    {
        public bool Succeeded { get; init; }
        public string[] Errors { get; init; }

        public bool IsFailure => !Succeeded;

        protected internal Result(bool success, IEnumerable<string> errors)
        {
            if (success && errors.Count() > 0)
                throw new InvalidOperationException();
            if (!success && errors.Count() == 0)
                throw new InvalidOperationException();
            Succeeded = success;
            Errors = errors.ToArray();
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }

        public static Result Success()
        {
            return new Result(true, Array.Empty<string>());
        }
    }

    public class Result<T> : Result
    {
        private Result(T value, bool success, IEnumerable<string> errors)
            : base(success, errors)
        {
            Value = value;
        }

        public T Value { get; set; }

        public static Result<T> Failure(T value, IEnumerable<string> errors)
        {
            return new Result<T>(value, false, errors);
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(value, true, Array.Empty<string>());
        }
    }
}
