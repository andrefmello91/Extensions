using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace andrefmello91.Extensions
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

		///  <summary>
		/// 		Clear this list when achieving an specified condition.
		///  </summary>
		///  <param name="list">The <see cref="List{T}"/>.</param>
		///  <param name="condition">The condition that clears the list if evaluated as true.</param>
		///  <param name="rangeToRetain">A range of items to retain in this list</param>
		public static void ClearIf<T>(this List<T> list, Predicate<List<T>> condition, Range? rangeToRetain = null)
		{
			if (!condition(list))
				return;

			if (!rangeToRetain.HasValue)
			{
				list.Clear();
				return;
			}
			
			// Get the range to retain
			#if NETSTANDARD
				var toRetain = list
					.ToArray()
					.AsSpan()[rangeToRetain.Value]
					.ToArray();
				
			#else
				var toRetain = list
					.ToArray()[rangeToRetain.Value];
			
			#endif

			// Clear list and re-add values
			list.Clear();
			list.AddRange(toRetain);
		}

		#endregion
	}
}