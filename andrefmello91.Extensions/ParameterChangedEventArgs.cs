using System;

namespace andrefmello91.Extensions
{
	/// <summary>
	///     Parameter changed class.
	/// </summary>
	public class ParameterChangedEventArgs<T> : EventArgs
	{

		#region Properties

		/// <summary>
		///     Get the new value of the parameter.
		/// </summary>
		public T NewValue { get; }

		/// <summary>
		///     Get the old value of the parameter.
		/// </summary>
		public T OldValue { get; }

		#endregion

		#region Constructors

		public ParameterChangedEventArgs(T oldValue, T newValue)
		{
			OldValue = oldValue;
			NewValue = newValue;
		}

		#endregion

	}
}