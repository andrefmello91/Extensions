using System;
using System.Linq;

namespace Extensions
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
			var name = Enum.GetName(type, value);
			return
				type.GetField(name!)?
				.GetCustomAttributes(false)
				.OfType<TAttribute>()
				.SingleOrDefault();
		}
	}
}
