using System;
using Sebreiro.FunctionalResult.Extensions;

namespace Sebreiro.FunctionalResult.Abstractions
{
    /// <summary>
    /// Базовый класс ответа
    /// </summary>
    public abstract class ResultBase
    {
        /// <summary>
        /// Создает новый экземпляр <see cref="ResultBase"/>.
        /// </summary>
        internal ResultBase(bool isSuccess, Enum code = default, string customErrorMessage = null)
        {
            Code = code;
            IsSuccess = isSuccess;
            CustomErrorMessage = customErrorMessage;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        protected ResultBase()
        {
        }

        /// <summary>
        /// Операция прошла успешно.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Операция прошла не успешно.
        /// </summary>
        public bool IsFailure => !IsSuccess;

        /// <summary>
        /// Собственный код ошибки
        /// </summary>
        internal Enum Code { get; set; }

        /// <summary>
        /// Собственное описание ошибки
        /// </summary>
        internal string CustomErrorMessage { get; set; }

        /// <summary>
        /// Сообщение, поясняющее ошибку.
        /// </summary>
        public string ErrorMessage => IsFailure
            ? GetDescription()
            : throw new ResultException("You attempted to access the " +
                "ErrorMessage property for a successful result. A successful result has no ErrorMessage.");

        /// <summary>
        /// Код ошибки.
        /// </summary>
        public Enum ErrorCode => IsFailure
            ? Code
            : throw new ResultException("You attempted to access the " +
                "ErrorCode property for a successful result. A successful result has no ErrorCode.");

        /// <summary>
        /// Формирует описание ошибки
        /// </summary>
        private string GetDescription()
        {
            if (!string.IsNullOrEmpty(CustomErrorMessage))
            {
                return ErrorCode.GetDescription() + ". " + CustomErrorMessage;
            }

            return ErrorCode.GetDescription();
        }
    }
}
