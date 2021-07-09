using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace andrefmello91.Extensions
{
	public static class CollectionExtensions
	{

		#region Methods

		/// <summary>
		///     Clear this list when achieving an specified condition.
		/// </summary>
		/// <param name="list">The <see cref="List{T}" />.</param>
		/// <param name="condition">The condition that clears the list if evaluated as true.</param>
		/// <param name="rangeToRetain">A range of items to retain in this list</param>
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

		/// <summary>
		///		<inheritdoc cref="List{T}.RemoveRange"/>
		/// </summary>
		/// <param name="list">The <see cref="List{T}"/>.</param>
		/// <param name="toRemove">The <see cref="Range"/> to remove.</param>
		public static void RemoveRange<T>(this List<T> list, Range toRemove)
		{
			var start = toRemove.Start.IsFromEnd
				? list.Count - toRemove.Start.Value
				: toRemove.Start.Value;
			
			var end = toRemove.End.IsFromEnd
				? list.Count - toRemove.End.Value
				: toRemove.End.Value;

			var count = (end - start).Abs();
			
			list.RemoveRange(start, count);
		}

		/// <summary>
		///     Check if this collection is null or empty.
		/// </summary>
		/// <returns>
		///     True if <paramref name="collection" /> is null or empty, otherwise, false.
		/// </returns>
		/// <typeparam name="T">Any type.</typeparam>
		public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IEnumerable<T>? collection) => collection is null || !collection.Any();


		///  <summary>
		/// 		Get a column from a 2D array.
		///  </summary>
		///  <param name="array">The 2D array.</param>
		///  <param name="columnIndex">The column index.</param>
		public static IEnumerable<T> GetColumn<T>(this T[,] array, int columnIndex)
		{
			var rows = array.GetLength(0);

			for (var i = 0; i < rows; i++)
				yield return array[i, columnIndex];
		}

		///  <summary>
		/// 		Get a row from a 2D array.
		///  </summary>
		///  <param name="array">The 2D array.</param>
		/// <param name="rowIndex">The row index</param>
		public static IEnumerable<T> GetRow<T>(this T[,] array, int rowIndex) 		
		{
			var columns = array.GetLength(1);

			for (var j = 0; j < columns; j++)
				yield return array[rowIndex, j];
		}


		#endregion

	}
}