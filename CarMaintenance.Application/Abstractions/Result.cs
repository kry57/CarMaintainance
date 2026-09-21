using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.Abstractions
{
    public record Result(bool isSuccess ,Error Error)
    {
        public bool IsSuccess => isSuccess; 
        public bool IsFaliure => !isSuccess;
        public Error error => Error;
        public static  Result Success() => new Result(true, Error.None);
        public static  Result Failure(Error error) => new Result(false, error);
    }
    public record Result<TValue>(TValue? ValueOuter,bool isSuccess ,Error Error) : Result(isSuccess, Error)
    {
        private TValue? value => IsSuccess ? ValueOuter : throw new InvalidOperationException("Must not has a value !");
        public static Result<TValue> Success(TValue value) => new(value, true, Error.None);
        public static new Result<TValue> Failure(Error error) => new (default,false, error);
    }
}
