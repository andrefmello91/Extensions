using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace andrefmello91.Extensions
{
	public static class EnumExtensions
	{
		/// <summary>
		///		Get a custom attribute from an <seealso cref="Enum"/> value.
		/// </summary>
		/// <typeparam name="TAttribute">Any class based on <see cref="Attribute"/>.</typeparam>
		/// <param name="value">An <seealso cref="Enum"/> value.</param>
		public static TAttribute? GetAttribute<TAttribute>(this Enum value)
			where TAttribute : Attribute
		{
            var type = value.GetType();

            return
                type.GetField(value.ToString())?
                    .GetCustomAttribute<TAttribute>();
		}
	}
}
