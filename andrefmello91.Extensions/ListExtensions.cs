using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Extensions
{
	public static class ListExtensions
	{
		#region  Methods

		/// <summary>
		///     Check if this collection is null or empty.
		/// </summary>
		/// <returns>
		///     True if <paramref name="collection" /> is null or empty, otherwise, false.
		/// </returns>
		/// <typeparam name="T">Any type.</typeparam>
		public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IEnumerable<T>? collection) => collection is null || !collection.Any();

		#endregion
	}
}