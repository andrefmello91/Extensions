using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    /// <summary>
    /// Extended list class with events. Implementation by CodeProject:
    /// <para>https://www.codeproject.com/Articles/31539/List-With-Events</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EList<T> : List<T>
    {
		#region Event(s)
		/// <summary>
		/// Event to run when the list count changes.
		/// </summary>
		public event EventHandler<CountChangedEventArgs> CountChanged;

		/// <summary>
		/// Event to run when an item is added.
		/// </summary>
		public event EventHandler<ItemEventArgs<T>> ItemAdded;

		/// <summary>
		/// Event to run when an item is removed.
		/// </summary>
		public event EventHandler<ItemEventArgs<T>> ItemRemoved;

		/// <summary>
		/// Event to run when a range of items is added.
		/// </summary>
		public event EventHandler<RangeEventArgs<T>> RangeAdded;

		/// <summary>
		/// Event to run when a range of items is removed.
		/// </summary>
		public event EventHandler<RangeEventArgs<T>> RangeRemoved;
		#endregion

		/// <summary>
		/// Create a new empty <see cref="EList{T}"/>.
		/// </summary>
		public EList()
		{
		}

		/// <summary>
		/// Create a new <see cref="EList{T}"/> from a <paramref name="collection"/>.
		/// </summary>
		public EList(IEnumerable<T> collection)
			: base(collection)
		{
		}

		//---------------------------------------------------------------
		#region Methods
		/// <summary>
		/// Adds an item.
		/// </summary>
		public new void Add(T item)
		{
			base.Add(item);

			RaiseCountEvent(CountChanged);
			RaiseItemEvent(ItemAdded, item);
		}

		/// <summary>
		/// Adds an item without raising list event.
		/// </summary>
		public void AddBase(T item) => base.Add(item);
		
		//------------------------------------------------------------------
		/// <summary>
		/// Adds a range of items.
		/// </summary>
		public new void AddRange(IEnumerable<T> collection)
		{
			var origCount = Count;

			base.AddRange(collection);

			RaiseCountEvent(CountChanged);

			RaiseRangeEvent(RangeAdded, collection);
		}

		/// <summary>
		/// Adds a range of items without raising events.
		/// </summary>
		public void AddRangeBase(IEnumerable<T> collection) => base.AddRange(collection);

		//------------------------------------------------------------------
		/// <summary>
		/// Clears the list.
		/// </summary>
		public new void Clear()
		{
			// Get the initial collection
			var list = this.ToList();

			base.Clear();

			RaiseCountEvent(CountChanged);
			RaiseRangeEvent(RangeRemoved, list);
		}

		/// <summary>
		/// Clears the list without raising events.
		/// </summary>
		public void ClearBase() => base.Clear();

		//------------------------------------------------------------------
		/// <summary>
		/// Removes the first matched item.
		/// </summary>
		public new void Remove(T item)
		{
			var index = IndexOf(item);

			var removed = base.Remove(item);

			if (!removed)
				return;

			RaiseCountEvent(CountChanged);

			RaiseItemEvent(ItemRemoved, item, index);
		}

		/// <summary>
		/// Removes the first matched item without raising events.
		/// </summary>
		public void RemoveBase(T item) => base.Remove(item);

		/// <summary>
		/// Remove all the items that match a predicate.
		/// </summary>
		public new void RemoveAll(Predicate<T> match)
		{
			// Get the items
			var removed = this.Where(t => match(t)).ToList();

			base.RemoveAll(match);

			RaiseRangeEvent(RangeRemoved, removed);

			RaiseCountEvent(CountChanged);
		}

		/// <summary>
		/// Remove all the items that match a predicate without raising events.
		/// </summary>
		public void RemoveAllBase(Predicate<T> match) => base.RemoveAll(match);

		/// <summary>
		/// Raise the count event.
		/// </summary>
		private void RaiseCountEvent(EventHandler<CountChangedEventArgs> eventHandler)
		{
			// Copy to a temporary variable to be thread-safe (MSDN).
			var tmp = eventHandler;
			tmp?.Invoke(this, new CountChangedEventArgs(Count));
		}

		/// <summary>
		/// Raise the item event.
		/// </summary>
		private void RaiseItemEvent(EventHandler<ItemEventArgs<T>> eventHandler, T item, int? index = null)
		{
			// Copy to a temporary variable to be thread-safe (MSDN).
			var tmp = eventHandler;
			tmp?.Invoke(this, new ItemEventArgs<T>(item, index ?? Count - 1));;
		}

		/// <summary>
		/// Raise the range event.
		/// </summary>
		private void RaiseRangeEvent(EventHandler<RangeEventArgs<T>> eventHandler, IEnumerable<T> collection)
		{
			// Copy to a temporary variable to be thread-safe (MSDN).
			var tmp = eventHandler;
			tmp?.Invoke(this, new RangeEventArgs<T>(collection));;
		}
		#endregion
		//------------------------------------------------------------------
		#region Subclasses

		#endregion
	}

    /// <summary>
    /// Item EventArgs.
    /// </summary>
    public class ItemEventArgs<T> : EventArgs
    {
	    /// <summary>
	    /// The item.
	    /// </summary>
	    public T Item { get; set; }

	    /// <summary>
	    /// The index of <see cref="Item"/>.
	    /// </summary>
	    public int Index { get; set; }

	    //----------------------------------------------------------
	    public ItemEventArgs(T item, int index = -1)
	    {
		    Item  = item;
		    Index = index;
	    }
    }

    /// <summary>
    /// Range EventArgs.
    /// </summary>
    public class RangeEventArgs<T> : EventArgs
    {
	    /// <summary>
	    /// The item collection.
	    /// </summary>
	    public List<T> ItemCollection { get; set; }

	    //----------------------------------------------------------
	    public RangeEventArgs(IEnumerable<T> collection)
	    {
		    ItemCollection = collection.ToList();
	    }
    }

    /// <summary>
    /// Count changed EventArgs.
    /// </summary>
    public class CountChangedEventArgs : EventArgs
    {
	    /// <summary>
	    /// Number of elements in the list.
	    /// </summary>
	    public int Count { get; set; }
	    //----------------------------------------------------------
	    public CountChangedEventArgs(int count)
	    {
		    Count = count;
	    }
    }
}
