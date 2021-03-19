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
		///     Creates an <see cref="EList{T}" /> based in an <see cref="IEnumerable{T}" />.
		/// </summary>
		/// <inheritdoc cref="IEList{T}" />
		/// <param name="collection">The collection to transform.</param>
		public static EList<T>? ToEList<T>(this IEnumerable<T>? collection)
			where T : IEquatable<T>, IComparable<T> =>
			collection is null
				? null
				: !collection.Any()
					? new EList<T>()
					: new EList<T>(collection);

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