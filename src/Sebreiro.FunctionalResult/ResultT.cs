using System;
using Sebreiro.FunctionalResult.Extensions;
using Sebreiro.FunctionalResult.Abstractions;

namespace Sebreiro.FunctionalResult
{
    /// <summary>
    /// Результат выполнения метода с данными.
    /// </summary>
    public class Result<T> : ResultBase
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="code"></param>
        /// <param name="customErrorMessage"></param>
        public Result(bool isSuccess, Enum code, string customErrorMessage = null)
            : base(isSuccess, code, customErrorMessage)
        {
            _value = default;
        }

        /// <summary>
        /// Конструктор со значением.
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="result"></param>
        internal Result(bool isSuccess, T result) : base(isSuccess)
        {
            _value = result;
        }

        private Result(Result result)
        {
            IsSuccess = result.IsSuccess;
            if (result.IsFailure)
            {
                Code = result.ErrorCode;
            }
            else
            {
                Code = default;
            }

            _value = default;
            CustomErrorMessage = null;
        }

        /// <summary>
        /// Данные, возвращаемые при успешном результате операции.
        /// </summary>
        public T Value => IsSuccess
            ? _value
            : throw new ResultException("You attempted to access the Value property for a failed result " +
                $"A failed result has no Value YET. Code Description: {ErrorCode.GetDescription()}");

        private readonly T _value;

        /// <summary>
        /// Преобразование NonGeneric Result to GenericResult
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static implicit operator Result<T>(Result result)
        {
            return new Result<T>(result);
        }
    }
}
