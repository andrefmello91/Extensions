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
		//------------------------------------------------------------------
		/// <summary>
		/// Adds a range
		/// </summary>
		public new void AddRange(IEnumerable<T> collection)
		{
			var origCount = Count;

			base.AddRange(collection);

			RaiseCountEvent(CountChanged);

			for (int i = 0; i < collection.Count(); i++)
				RaiseItemEvent(ItemAdded, collection.ElementAt(i), origCount + i);
		}
		//------------------------------------------------------------------
		/// <summary>
		/// Clears the list.
		/// </summary>
		public new void Clear()
		{
			base.Clear();

			RaiseCountEvent(CountChanged);
		}
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
	    public ItemEventArgs(T item, int index)
	    {
		    Item  = item;
		    Index = index;
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
