using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Utils
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public string Error { get; private set; }
        public T Value { get; private set; }

        private Result(bool isSuccess, T value = default, string error = null)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public static Result<T> Success(T value) => new Result<T>(true, value);
        public static Result<T> Failure(string error) => new Result<T>(false, error: error);
    }

    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string Error { get; private set; }

        private Result(bool isSuccess, string error = null)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new Result(true);
        public static Result Failure(string error) => new Result(false, error);
    }
}