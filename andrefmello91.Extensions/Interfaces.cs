using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using UnitsNet;
using UnitsNet.Units;

namespace andrefmello91.Extensions
{
	/// <summary>
	/// UnitConvertible interface.
	/// </summary>
	/// <typeparam name="TUnit">
	///     The enum of unit that represents the object.
	///		<seealso cref="LengthUnit"/>
	///		<seealso cref="AreaUnit"/>
	///		<seealso cref="ForceUnit"/>
	///		<seealso cref="PressureUnit"/>
	/// </typeparam>
	public interface IUnitConvertible<TUnit>
		where TUnit : Enum
	{
		/// <summary>
		/// Get/set the unit of this object.
		/// </summary>
		TUnit Unit { get; set; }

		/// <summary>
		/// Change the unit of this object.
		/// </summary>
		/// <param name="unit">The unit to convert.</param>
		void ChangeUnit(TUnit unit);

		/// <summary>
		/// Convert this object to another unit.
		/// </summary>
		/// <inheritdoc cref="ChangeUnit"/>
		IUnitConvertible<TUnit> Convert(TUnit unit);
	}

	/// <summary>
	/// Approachable interface
	/// </summary>
	/// <typeparam name="T1">The type that represents the object.</typeparam>
	/// <typeparam name="T2">The type that represents the tolerance to compare objects.</typeparam>
	public interface IApproachable<in T1, in T2>
	{
		/// <summary>
		/// Returns true if this object is approximately equivalent to <paramref name="other"/>.
		/// </summary>
		/// <param name="other">The other object to compare.</param>
		/// <param name="tolerance">The tolerance to consider this object approximately equivalent to <paramref name="other"/>.</param>
		bool Approaches(T1 other, T2 tolerance);
	}

	/// <typeparam name="T2">The type that represents an object that can be compared to <see cref="T1"/>.</typeparam>
	/// <typeparam name="T3">The type that represents the tolerance to compare objects.</typeparam>
	/// <inheritdoc cref="IApproachable{T1,T2}"/>
	public interface IApproachable<in T1, in T2, in T3>
	{
		/// <inheritdoc cref="IApproachable{T1,T2}.Approaches"/>
		bool Approaches(T1 other, T3 tolerance);

		/// <inheritdoc cref="IApproachable{T1,T2}.Approaches"/>
		bool Approaches(T2 other, T3 tolerance);
	}

	/// <summary>
	///		Interface to compare an object with two different types.
	/// </summary>
	/// <typeparam name="T1">Any type.</typeparam>
	/// <typeparam name="T2">Any type.</typeparam>
	public interface IEquatable<in T1, in T2>
	{
		/// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
		bool Equals(T1 other);

		/// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
		bool Equals(T2 other);
	}

	/// <summary>
	/// Cloneable interface with generic type.
	/// </summary>
	/// <typeparam name="T">Any type.</typeparam>
	public interface ICloneable<out T>
	{
		/// <inheritdoc cref="ICloneable.Clone"/>
		T Clone();
	}
}