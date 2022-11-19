using System;
using Sebreiro.FunctionalResult.Extensions;
using Sebreiro.FunctionalResult.Abstractions;

namespace Sebreiro.FunctionalResult
{
    /// <summary>
    /// Результат выполнения метода с данными.
    /// </summary>
    public class Result<T, E> : ResultBase
    {
        /// <summary>
        /// Конструктор с кодом.
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="error"></param>
        /// <param name="errorCode"></param>
        /// <param name="customErrorMessage"></param>
        internal Result(bool isSuccess, E error, Enum errorCode, string customErrorMessage = null)
            : base(isSuccess, errorCode, customErrorMessage)
        {
            _value = default;
            _error = error;
        }

        /// <summary>
        /// Конструктор со значением.
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="result"></param>
        internal Result(bool isSuccess, T result) : base(isSuccess)
        {
            _value = result;
            _error = default;
        }

        private readonly T _value;
        /// <summary>
        /// Данные, возвращаемые при успешном результате операции.
        /// </summary>
        public T Value => IsSuccess
            ? _value
            : throw new ResultException("You attempted to access the Value property for a failed result " +
                $"A failed result has no Value YET. Code Description: {ErrorCode.GetDescription()}");

        private readonly E _error;
        /// <summary>
        /// Данные, возвращаемые при неудачном результате операции.
        /// </summary>
        public E Error => IsFailure
            ? _error
            : throw new ResultException("You attempted to access the Error property for a success result." +
                $"Code Description: {ErrorCode.GetDescription()}");

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
            _error = default;
            CustomErrorMessage = null;
        }

        /// <summary>
        /// Преобразование NonGeneric Result to GenericResult
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static implicit operator Result<T, E>(Result result)
        {
            return new Result<T, E>(result);
        }
    }
}
