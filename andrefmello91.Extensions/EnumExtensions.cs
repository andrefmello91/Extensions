using System;
using System.Reflection;

namespace andrefmello91.Extensions
{
	public static class EnumExtensions
	{

		#region Methods

		/// <summary>
		///     Get a custom attribute from an <seealso cref="Enum" /> value.
		/// </summary>
		/// <typeparam name="TAttribute">Any class based on <see cref="Attribute" />.</typeparam>
		/// <param name="value">An <seealso cref="Enum" /> value.</param>
		public static TAttribute? GetAttribute<TAttribute>(this Enum value)
			where TAttribute : Attribute
		{
			var type = value.GetType();

			return
				type.GetField(value.ToString())?
					.GetCustomAttribute<TAttribute>();
		}

		#endregion

	}
}