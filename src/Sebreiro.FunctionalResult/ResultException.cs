using System;
using System.Runtime.Serialization;

namespace Sebreiro.FunctionalResult
{
    /// <summary>
    /// Ошибка при работе с результатом операций.
    /// </summary>
    [Serializable]
    public class ResultException : Exception
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public ResultException()
        {
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        protected ResultException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public ResultException(string message) : base(message)
        {
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public ResultException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}