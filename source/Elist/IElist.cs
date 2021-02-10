using System;
using System.Collections.Generic;

namespace Extensions
{
	/// <summary>
	///     Extended list class with events. Implementation by CodeProject:
	///     <para>https://www.codeproject.com/Articles/31539/List-With-Events</para>
	/// </summary>
	/// <typeparam name="T">Any type.</typeparam>
	public interface IEList<T> : IList<T>
	{
		#region Events

		/// <summary>
		///     Event to run when the list count changes.
		/// </summary>
		event EventHandler<CountChangedEventArgs> CountChanged;

		/// <summary>
		///     Event to run when an item is added.
		/// </summary>
		event EventHandler<ItemEventArgs<T>> ItemAdded;

		/// <summary>
		///     Event to run when an item is removed.
		/// </summary>
		event EventHandler<ItemEventArgs<T>> ItemRemoved;

		/// <summary>
		///     Event to run when a range of items is added.
		/// </summary>
		event EventHandler<RangeEventArgs<T>> RangeAdded;

		/// <summary>
		///     Event to run when a range of items is removed.
		/// </summary>
		event EventHandler<RangeEventArgs<T>> RangeRemoved;

		/// <summary>
		///     Event to run when the list is sorted.
		/// </summary>
		event EventHandler ListSorted;

		#endregion

		#region Properties

		/// <summary>
		///     Allow duplicate items in this list.
		/// </summary>
		/// <remarks>
		///     If false, an item is not added if this list contains it.
		/// </remarks>
		bool AllowDuplicates { get; set; }

		#endregion

		#region  Methods

		/// <inheritdoc cref="List{T}.Add" />
		/// <param name="raiseEvents">Raise events after modifying this list?</param>
		/// <param name="sort">Sort this list after modifying this list?</param>
		void Add(T item, bool raiseEvents = true, bool sort = true);

		/// <inheritdoc cref="List{T}.AddRange" />
		/// <inheritdoc cref="Add" />
		void AddRange(IEnumerable<T> collection, bool raiseEvents = true, bool sort = true);

		//------------------------------------------------------------------
		/// <inheritdoc cref="List{T}.Clear" />
		/// <inheritdoc cref="Add" />
		void Clear(bool raiseEvents = true);

		//------------------------------------------------------------------
		/// <inheritdoc cref="List{T}.Remove" />
		/// <inheritdoc cref="Add" />
		bool Remove(T item, bool raiseEvents = true, bool sort = true);

		/// <inheritdoc cref="List{T}.RemoveAll" />
		/// <inheritdoc cref="Add" />
		int RemoveAll(Predicate<T> match, bool raiseEvents = true, bool sort = true);

		/// <summary>
		///     Remove all of the items in this list that match any item in <paramref name="collection" />.
		/// </summary>
		/// <param name="collection">The collection of items to remove.</param>
		/// <inheritdoc cref="Add" />
		int RemoveRange(IEnumerable<T> collection, bool raiseEvents = true, bool sort = true);

		/// <inheritdoc cref="List{T}.Sort()" />
		/// <inheritdoc cref="Add(T, bool, bool)" />
		void Sort(bool raiseEvents = true);

		/// <inheritdoc cref="List{T}.Sort(IComparer{T})" />
		/// <inheritdoc cref="Add(T, bool, bool)" />
		void Sort(IComparer<T> comparer, bool raiseEvents = true);

		#endregion

		//---------------------------------------------------------------
		//------------------------------------------------------------------
	}
}