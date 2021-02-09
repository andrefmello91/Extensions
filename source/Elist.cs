using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
	/// <summary>
	///     Extended list class with events. Implementation by CodeProject:
	///     <para>https://www.codeproject.com/Articles/31539/List-With-Events</para>
	/// </summary>
	/// <typeparam name="T">Any type.</typeparam>
	public class EList<T> : List<T>
	{
		#region Events

		/// <summary>
		///     Event to run when the list count changes.
		/// </summary>
		public event EventHandler<CountChangedEventArgs> CountChanged;

		/// <summary>
		///     Event to run when an item is added.
		/// </summary>
		public event EventHandler<ItemEventArgs<T>> ItemAdded;

		/// <summary>
		///     Event to run when an item is removed.
		/// </summary>
		public event EventHandler<ItemEventArgs<T>> ItemRemoved;

		/// <summary>
		///     Event to run when a range of items is added.
		/// </summary>
		public event EventHandler<RangeEventArgs<T>> RangeAdded;

		/// <summary>
		///     Event to run when a range of items is removed.
		/// </summary>
		public event EventHandler<RangeEventArgs<T>> RangeRemoved;

		/// <summary>
		///     Event to run when the list is sorted.
		/// </summary>
		public event EventHandler ListSorted;

		#endregion

		#region Properties

		/// <summary>
		///     Allow duplicate items in this list.
		/// </summary>
		/// <remarks>
		///     If false, an item is not added if this list contains it.
		/// </remarks>
		public bool AllowDuplicates { get; set; } = false;

		#endregion

		#region Constructors

		/// <summary>
		///     Create a new empty <see cref="EList{T}" />.
		/// </summary>
		public EList()
		{
		}

		/// <summary>
		///     Create a new <see cref="EList{T}" /> from a <paramref name="collection" />.
		/// </summary>
		public EList(IEnumerable<T> collection)
			: base(collection)
		{
		}

		#endregion

		#region  Methods

		/// <inheritdoc cref="List{T}.Add" />
		/// <param name="raiseEvents">Raise events after adding?</param>
		/// <param name="sort">Sort this list after adding?</param>
		public void Add(T item, bool raiseEvents = true, bool sort = true)
		{
			if (!AllowDuplicates && Contains(item))
				return;

			base.Add(item);

			if (raiseEvents)
			{
				RaiseCountEvent(CountChanged);
				RaiseItemEvent(ItemAdded, item);
			}

			if (sort)
				Sort(raiseEvents);
		}

		/// <inheritdoc cref="List{T}.AddRange" />
		/// <inheritdoc cref="Add" />
		public void AddRange(IEnumerable<T> collection, bool raiseEvents = true, bool sort = true)
		{
			var toAdd = AllowDuplicates ? collection : collection.Distinct().Where(t => !Contains(t));

			base.AddRange(toAdd);

			if (raiseEvents)
			{
				RaiseCountEvent(CountChanged);
				RaiseRangeEvent(RangeAdded, toAdd);
			}

			if (sort)
				Sort(raiseEvents);
		}

		//------------------------------------------------------------------
		/// <inheritdoc cref="List{T}.Clear" />
		/// <inheritdoc cref="Add" />
		public void Clear(bool raiseEvents = true)
		{
			// Get the initial collection
			var list = this.ToList();

			base.Clear();

			if (!raiseEvents)
				return;

			RaiseCountEvent(CountChanged);
			RaiseRangeEvent(RangeRemoved, list);
		}

		//------------------------------------------------------------------
		/// <inheritdoc cref="List{T}.Remove" />
		/// <inheritdoc cref="Add" />
		public bool Remove(T item, bool raiseEvents = true, bool sort = true)
		{
			var index = IndexOf(item);

			if (!base.Remove(item))
				return false;

			if (raiseEvents)
			{
				RaiseCountEvent(CountChanged);
				RaiseItemEvent(ItemRemoved, item, index);
			}

			if (sort)
				Sort(raiseEvents);

			return true;
		}

		/// <inheritdoc cref="List{T}.RemoveAll" />
		/// <inheritdoc cref="Add" />
		public int RemoveAll(Predicate<T> match, bool raiseEvents = true, bool sort = true)
		{
			// Get the items
			var removed = this.Where(t => match(t)).ToList();

			var count = base.RemoveAll(match);

			if (raiseEvents)
			{
				RaiseRangeEvent(RangeRemoved, removed);
				RaiseCountEvent(CountChanged);
			}

			if (sort)
				Sort(raiseEvents);

			return count;
		}

		/// <summary>
		///     Remove all of the items in this list that match any item in <paramref name="collection" />.
		/// </summary>
		/// <param name="collection">The collection of items to remove.</param>
		/// <inheritdoc cref="Add" />
		public int RemoveRange(IEnumerable<T> collection, bool raiseEvents = true, bool sort = true) => RemoveAll(collection.Contains, raiseEvents, sort);

		/// <inheritdoc cref="List{T}.Sort()" />
		/// <inheritdoc cref="Add(T, bool, bool)" />
		public void Sort(bool raiseEvents = true)
		{
			base.Sort();

			if (raiseEvents)
				RaiseSortEvent(ListSorted);
		}

		/// <inheritdoc cref="List{T}.Sort(IComparer{T})" />
		/// <inheritdoc cref="Add(T, bool, bool)" />
		public void Sort(IComparer<T> comparer, bool raiseEvents = true)
		{
			base.Sort(comparer);

			if (raiseEvents)
				RaiseSortEvent(ListSorted);
		}

		/// <summary>
		///     Raise the count event.
		/// </summary>
		private void RaiseCountEvent(EventHandler<CountChangedEventArgs> eventHandler)
		{
			// Copy to a temporary variable to be thread-safe (MSDN).
			var tmp = eventHandler;
			tmp?.Invoke(this, new CountChangedEventArgs(Count));
		}

		/// <summary>
		///     Raise the item event.
		/// </summary>
		private void RaiseItemEvent(EventHandler<ItemEventArgs<T>> eventHandler, T item, int? index = null)
		{
			// Copy to a temporary variable to be thread-safe (MSDN).
			var tmp = eventHandler;
			tmp?.Invoke(this, new ItemEventArgs<T>(item, index ?? Count - 1));
		}

		/// <summary>
		///     Raise the range event.
		/// </summary>
		private void RaiseRangeEvent(EventHandler<RangeEventArgs<T>> eventHandler, IEnumerable<T> collection)
		{
			// Copy to a temporary variable to be thread-safe (MSDN).
			var tmp = eventHandler;
			tmp?.Invoke(this, new RangeEventArgs<T>(collection));
		}

		/// <summary>
		///     Raise the sort event.
		/// </summary>
		private void RaiseSortEvent(EventHandler eventHandler)
		{
			// Copy to a temporary variable to be thread-safe (MSDN).
			var tmp = eventHandler;
			tmp?.Invoke(this, new EventArgs());
		}

		#endregion

		//---------------------------------------------------------------
		//------------------------------------------------------------------
	}

	/// <summary>
	///     Item EventArgs.
	/// </summary>
	public class ItemEventArgs<T> : EventArgs
	{
		#region Properties

		/// <summary>
		///     The index of <see cref="Item" />.
		/// </summary>
		public int Index { get; set; }

		/// <summary>
		///     The item.
		/// </summary>
		public T Item { get; set; }

		#endregion

		#region Constructors

		//----------------------------------------------------------
		public ItemEventArgs(T item, int index = -1)
		{
			Item  = item;
			Index = index;
		}

		#endregion
	}

	/// <summary>
	///     Range EventArgs.
	/// </summary>
	public class RangeEventArgs<T> : EventArgs
	{
		#region Properties

		/// <summary>
		///     The item collection.
		/// </summary>
		public List<T> ItemCollection { get; set; }

		#endregion

		#region Constructors

		//----------------------------------------------------------
		public RangeEventArgs(IEnumerable<T> collection) => ItemCollection = collection.ToList();

		#endregion
	}

	/// <summary>
	///     Count changed EventArgs.
	/// </summary>
	public class CountChangedEventArgs : EventArgs
	{
		#region Properties

		/// <summary>
		///     Number of elements in the list.
		/// </summary>
		public int Count { get; set; }

		#endregion

		#region Constructors

		//----------------------------------------------------------
		public CountChangedEventArgs(int count) => Count = count;

		#endregion
	}
}