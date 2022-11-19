using System;
using System.ComponentModel;
using System.Reflection;

namespace Sebreiro.FunctionalResult.Extensions
{
    /// <summary>
    /// Расширение для enum.
    /// </summary>
    internal static class ErrorCodeEnumExtensions
    {
        /// <summary>
        /// Получить сообщение из атрибута Description
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            var fieldName = value.ToString();
            var fieldInfo = value.GetType().GetField(fieldName);

            var descriptionAttribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute), false);

            return descriptionAttribute?.Description ?? fieldName;
        }
    }
}
