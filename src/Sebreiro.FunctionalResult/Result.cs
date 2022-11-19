using System;
using Sebreiro.FunctionalResult.Abstractions;

namespace Sebreiro.FunctionalResult
{
    /// <summary>
    /// Результат выполнения метода.
    /// </summary>
    public class Result : ResultBase
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="code"></param>
        /// <param name="customErrorMessage"></param>
        private Result(bool isSuccess, Enum code = default, string customErrorMessage = null) 
            : base(isSuccess, code, customErrorMessage)
        {
        }

        /// <summary>
        /// Метод, возвращающий успешный результат.
        /// </summary>
        /// <returns></returns>
        public static Result Success()
        {
            return new Result(true);
        }

        /// <summary>
        /// Метод, возращающий провальный результат.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Result Failure(Enum code)
        {
            return new Result(false, code);
        }

        /// <summary>
        /// Метод, возращающий провальный результат и возможностью установить сообщение.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="customErrorMessage"></param>
        /// <returns></returns>
        public static Result Failure(Enum code, string customErrorMessage)
        {
            return new Result(false, code, customErrorMessage);
        }

        /// <summary>
        /// Метод, возвращающий успешный результат с данными.
        /// </summary>
        /// <returns></returns>
        public static Result<T> Success<T>(T result)
        {
            return new Result<T>(true, result);
        }

        /// <summary>
        /// Метод, возвращающий провальный результат с кодом.
        /// </summary>
        /// <param name="code"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Result<T> Failure<T>(Enum code)
        {
            return new Result<T>(false, code);
        }

        /// <summary>
        /// Метод, возвращающий провальный результат с данными и возможностью установить сообщение.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="customErrorMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Result<T> Failure<T>(Enum code, string customErrorMessage)
        {
            return new Result<T>(false, code, customErrorMessage);
        }

        /// <summary>
        /// Метод, возвращающий успешный результат с данными.
        /// </summary>
        /// <returns></returns>
        public static Result<T, E> Success<T, E>(T result)
        {
            return new Result<T, E>(true, result);
        }

        /// <summary>
        /// Метод, возрашщающий провальный результат с данными.
        /// </summary>
        /// <param name="error"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Result<T, E> Failure<T, E>(Enum code, E error)
        {
            return new Result<T, E>(false, error, code);
        }

        /// <summary>
        /// Метод, возрашщающий провальный результат с данными и возможностью установить сообщение.
        /// </summary>
        /// <param name="error"></param>
        /// <param name="code"></param>
        /// <param name="customErrorMessage"></param>
        /// <returns></returns>
        public static Result<T, E> Failure<T, E>(Enum code, string customErrorMessage, E error)
        {
            return new Result<T, E>(false, error, code, customErrorMessage);
        }
    }
}
