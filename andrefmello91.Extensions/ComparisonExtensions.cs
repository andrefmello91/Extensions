using System;

namespace andrefmello91.Extensions
{
	/// <summary>
	///     Comparison extensions class.
	/// </summary>
	public static class ComparisonExtensions
	{

		#region Methods

		/// <summary>
		///     Compare <paramref name="object1" /> to <paramref name="object2" />.
		/// </summary>
		/// <typeparam name="TComparable">Any type that implements <see cref="IComparable{T}" />.</typeparam>
		/// <returns>
		///     0 if both objects are null.
		///     <para>
		///         -1 if <paramref name="object1" /> is null and <paramref name="object2" /> is not null.
		///     </para>
		///     <para>
		///         Default comparer if objects are not null.
		///     </para>
		/// </returns>
		public static int Compare<TComparable>(this TComparable? object1, TComparable? object2)
			where TComparable : IComparable<TComparable> =>
			object1 is null
				? object2 is null
					? 0
					: -1
				: object1.CompareTo(object2);

		/// <summary>
		///     Check equality of two objects.
		/// </summary>
		/// <typeparam name="TEquatable">Any type that implements <see cref="IEquatable{T}" />.</typeparam>
		/// <returns>
		///     True if both objects are null or equal.
		/// </returns>
		public static bool IsEqualTo<TEquatable>(this TEquatable? object1, TEquatable? object2)
			where TEquatable : IEquatable<TEquatable> =>
			object1 is null && object2 is null ||
			object1 is not null && object1.Equals(object2);

		/// <summary>
		///     Check equality of two objects.
		/// </summary>
		/// <typeparam name="TEquatable">Any type that implements <see cref="IEquatable{T}" />.</typeparam>
		/// <returns>
		///     True if one of the objects is null or they are not equal.
		/// </returns>
		public static bool IsNotEqualTo<TEquatable>(this TEquatable? object1, TEquatable? object2)
			where TEquatable : IEquatable<TEquatable> =>
			object1 is null && object2 is not null ||
			object1 is not null && (object2 is null || !object1.Equals(object2));

		#endregion

	}
}