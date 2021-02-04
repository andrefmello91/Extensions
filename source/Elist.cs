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

		#region

		/// <inheritdoc cref="List{T}.Add" />
		public new void Add(T item)
		{
			base.Add(item);

			RaiseCountEvent(CountChanged);
			RaiseItemEvent(ItemAdded, item);
		}

		/// <summary>
		///     <inheritdoc cref="Add" />
		///     <para>This method doesn't raise events.</para>
		/// </summary>
		public void AddBase(T item) => base.Add(item);

		//------------------------------------------------------------------
		/// <inheritdoc cref="List{T}.AddRange" />
		public new void AddRange(IEnumerable<T> collection)
		{
			base.AddRange(collection);

			RaiseCountEvent(CountChanged);

			RaiseRangeEvent(RangeAdded, collection);
		}

		/// <summary>
		///     <inheritdoc cref="AddRange" />
		///     <para>This method doesn't raise events.</para>
		/// </summary>
		public void AddRangeBase(IEnumerable<T> collection) => base.AddRange(collection);

		/// <summary>
		///     Add an object and sort this list.
		/// </summary>
		/// <param name="item">
		///     <inheritdoc cref="Add" />
		/// </param>
		/// <param name="comparer">
		///     <inheritdoc cref="Sort(IComparer{T})" />
		/// </param>
		public void AddAndSort(T item, IComparer<T> comparer = null)
		{
			Add(item);

			if (comparer is null)
				Sort();
			else
				Sort(comparer);
		}

		/// <summary>
		///     Add an object and sort this list.
		///     <para>This method doesn't raise events.</para>
		/// </summary>
		/// <param name="item">
		///     <inheritdoc cref="Add" />
		/// </param>
		/// <param name="comparer">
		///     <inheritdoc cref="Sort(IComparer{T})" />
		/// </param>
		public void AddAndSortBase(T item, IComparer<T> comparer = null)
		{
			AddBase(item);

			if (comparer is null)
				SortBase();
			else
				SortBase(comparer);
		}

		/// <summary>
		///     Add a collection of objects and sort this list.
		/// </summary>
		/// <param name="collection">
		///     <inheritdoc cref="AddRange" />
		/// </param>
		/// <param name="comparer">
		///     <inheritdoc cref="Sort(IComparer{T})" />
		/// </param>
		public void AddRangeAndSort(IEnumerable<T> collection, IComparer<T> comparer = null)
		{
			AddRange(collection);

			if (comparer is null)
				Sort();
			else
				Sort(comparer);
		}

		/// <summary>
		///     Add a collection of objects and sort this list.
		///     <para>This method doesn't raise events.</para>
		/// </summary>
		/// <param name="collection">
		///     <inheritdoc cref="AddRange" />
		/// </param>
		/// <param name="comparer">
		///     <inheritdoc cref="Sort(IComparer{T})" />
		/// </param>
		public void AddRangeAndSortBase(IEnumerable<T> collection, IComparer<T> comparer = null)
		{
			AddRangeBase(collection);

			if (comparer is null)
				SortBase();
			else
				SortBase(comparer);
		}

		//------------------------------------------------------------------
		/// <inheritdoc cref="List{T}.Clear" />
		public new void Clear()
		{
			// Get the initial collection
			var list = this.ToList();

			base.Clear();

			RaiseCountEvent(CountChanged);
			RaiseRangeEvent(RangeRemoved, list);
		}

		/// <summary>
		///     <inheritdoc cref="Clear" />
		///     <para>This method doesn't raise events.</para>
		/// </summary>
		public void ClearBase() => base.Clear();

		//------------------------------------------------------------------
		/// <inheritdoc cref="List{T}.Remove" />
		public new bool Remove(T item)
		{
			var index = IndexOf(item);

			var removed = base.Remove(item);

			if (removed)
			{
				RaiseCountEvent(CountChanged);
				RaiseItemEvent(ItemRemoved, item, index);
			}

			return removed;
		}

		/// <summary>
		///     <inheritdoc cref="Remove" />
		///     <para>This method doesn't raise events.</para>
		/// </summary>
		public bool RemoveBase(T item) => base.Remove(item);

		//------------------------------------------------------------------
		/// <summary>
		///     Remove an item and sort this list.
		/// </summary>
		/// <param name="item">
		///     <inheritdoc cref="Remove" />
		/// </param>
		/// <param name="comparer">
		///     <inheritdoc cref="Sort(IComparer{T})" />
		/// </param>
		public bool RemoveAndSort(T item, IComparer<T> comparer = null)
		{
			var removed = Remove(item);

			if (comparer is null)
				Sort();
			else
				Sort(comparer);

			return removed;
		}

		/// <summary>
		///     Remove an item and sort this list.
		/// </summary>
		/// <para>This method doesn't raise events.</para>
		/// <param name="item">
		///     <inheritdoc cref="Remove" />
		/// </param>
		/// <param name="comparer">
		///     <inheritdoc cref="Sort(IComparer{T})" />
		/// </param>
		public bool RemoveAndSortBase(T item, IComparer<T> comparer = null)
		{
			var removed = RemoveBase(item);

			if (comparer is null)
				SortBase();
			else
				SortBase(comparer);

			return removed;
		}

		/// <inheritdoc cref="List{T}.RemoveAll" />
		public new int RemoveAll(Predicate<T> match)
		{
			// Get the items
			var removed = this.Where(t => match(t)).ToList();

			var count = base.RemoveAll(match);

			RaiseRangeEvent(RangeRemoved, removed);

			RaiseCountEvent(CountChanged);

			return count;
		}

		/// <summary>
		///     <inheritdoc cref="RemoveAll" />
		///     <para>This method doesn't raise events.</para>
		/// </summary>
		public int RemoveAllBase(Predicate<T> match) => base.RemoveAll(match);

		/// <summary>
		///     Remove all the items that match a predicate and sort this list.
		/// </summary>
		/// <param name="match">
		///     <inheritdoc cref="RemoveAll" />
		/// </param>
		/// <param name="comparer">
		///     <inheritdoc cref="Sort(IComparer{T})" />
		/// </param>
		public int RemoveAllAndSort(Predicate<T> match, IComparer<T> comparer = null)
		{
			var count = RemoveAll(match);

			if (comparer is null)
				Sort();
			else
				Sort(comparer);

			return count;
		}

		/// <summary>
		///     Remove all the items that match a predicate and sort this list.
		/// </summary>
		/// <para>This method doesn't raise events.</para>
		/// <param name="match">
		///     <inheritdoc cref="RemoveAll" />
		/// </param>
		/// <param name="comparer">
		///     <inheritdoc cref="Sort(IComparer{T})" />
		/// </param>
		public int RemoveAllAndSortBase(Predicate<T> match, IComparer<T> comparer = null)
		{
			var count = RemoveAllBase(match);

			if (comparer is null)
				SortBase();
			else
				SortBase(comparer);

			return count;
		}

		/// <inheritdoc cref="List{T}.Sort()" />
		public new void Sort()
		{
			base.Sort();

			RaiseSortEvent(ListSorted);
		}

		/// <inheritdoc cref="List{T}.Sort(IComparer{T})" />
		public new void Sort(IComparer<T> comparer)
		{
			base.Sort(comparer);

			RaiseSortEvent(ListSorted);
		}

		/// <summary>
		///     <inheritdoc cref="Sort()" />
		///     <para>This method doesn't raise events.</para>
		/// </summary>
		public void SortBase() => base.Sort();

		/// <summary>
		///     <inheritdoc cref="Sort(IComparer{T})" />
		///     <para>This method doesn't raise events.</para>
		/// </summary>
		public void SortBase(IComparer<T> comparer) => base.Sort(comparer);

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
			;
		}

		/// <summary>
		///     Raise the range event.
		/// </summary>
		private void RaiseRangeEvent(EventHandler<RangeEventArgs<T>> eventHandler, IEnumerable<T> collection)
		{
			// Copy to a temporary variable to be thread-safe (MSDN).
			var tmp = eventHandler;
			tmp?.Invoke(this, new RangeEventArgs<T>(collection));
			;
		}

		/// <summary>
		///     Raise the sort event.
		/// </summary>
		private void RaiseSortEvent(EventHandler eventHandler)
		{
			// Copy to a temporary variable to be thread-safe (MSDN).
			var tmp = eventHandler;
			tmp?.Invoke(this, new EventArgs());
			;
		}

		#endregion

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