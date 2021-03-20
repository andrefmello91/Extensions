﻿using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using UnitsNet;
using UnitsNet.Units;

namespace Extensions
{
	/// <summary>
	/// UnitConvertible interface.
	/// </summary>
	/// <typeparam name="T1">
	///     The type that represents the value of the object.
	/// </typeparam>
	/// <typeparam name="T2">
	///     The enum of unit that represents the object.
	///     <para>
	///         <see cref="LengthUnit"/>, <see cref="ForceUnit"/>, <see cref="PressureUnit"/>, etc.
	///     </para>
	/// </typeparam>
	public interface IUnitConvertible<out T1, T2>
		where T2 : Enum
	{
		/// <summary>
		/// Get/set the unit of this object.
		/// </summary>
		T2 Unit { get; set; }

		/// <summary>
		/// Change the unit of this object.
		/// </summary>
		/// <param name="unit">The unit to convert.</param>
		void ChangeUnit(T2 unit);

		/// <summary>
		/// Convert this object to another unit.
		/// </summary>
		/// <inheritdoc cref="ChangeUnit"/>
		T1 Convert(T2 unit);
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