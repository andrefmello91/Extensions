﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnitsNet;
using UnitsNet.Units;

namespace andrefmello91.Extensions;

/// <summary>
///     Extensions for <see cref="UnitsNet" />.
/// </summary>
public static partial class UnitExtensions
{

	#region Methods

	/// <summary>
	///     Returns the abbreviation of this <paramref name="unit" />.
	/// </summary>
	public static string Abbrev<TUnit>(this TUnit unit)
		where TUnit : Enum =>
		UnitAbbreviationsCache.Default.GetDefaultAbbreviation(unit);

	/// <inheritdoc cref="Add{TQuantity,TUnit}(TQuantity,TQuantity)" />
	/// <param name="unit">The required unit.</param>
	public static TQuantity Add<TQuantity, TUnit>(this TQuantity quantity, TQuantity other, TUnit unit)
		where TQuantity : IQuantity<TUnit>
		where TUnit : Enum =>
		(TQuantity) (quantity.As(unit) + other.As(unit)).As(unit);

	/// <summary>
	///     Get a quantity with a summed values.
	/// </summary>
	public static TQuantity Add<TQuantity, TUnit>(this TQuantity quantity, TQuantity other)
		where TQuantity : IQuantity<TUnit>
		where TUnit : Enum =>
		quantity.Add(other, quantity.Unit);

	/// <summary>
	///     Returns true if this <paramref name="quantity" /> is approximately equal to <paramref name="other" />.
	/// </summary>
	/// <remarks>
	///     If the difference between these values is smaller than <paramref name="tolerance" />, true is returned.
	/// </remarks>
	/// <param name="other">The other quantity.</param>
	/// <param name="tolerance">The tolerance to consider <paramref name="quantity" /> approximately equal to other.</param>
	public static bool Approx<TQuantity>(this TQuantity quantity, TQuantity other, TQuantity tolerance)
		where TQuantity : IQuantity =>
		(quantity.Value - other.ToUnit(quantity.Unit).Value).Abs() <= tolerance.ToUnit(quantity.Unit).Value.Abs();

	/// <summary>
	///     Returns true if this <paramref name="quantity" /> is approximately equal to zero.
	/// </summary>
	/// <inheritdoc cref="Approx{TQuantity}" />
	public static bool ApproxZero<TQuantity>(this TQuantity quantity, TQuantity tolerance)
		where TQuantity : IQuantity =>
		quantity.Value.Abs() <= tolerance.ToUnit(quantity.Unit).Value.Abs();

	/// <summary>
	///     Create a quantity from this <paramref name="number" />.
	/// </summary>
	/// <param name="number">The number.</param>
	/// <param name="unit">The unit type: <see cref="LengthUnit" />, <see cref="AreaUnit" />, <see cref="ForceUnit" />.</param>
	/// <returns>
	///     A quantity based in <paramref name="unit" />. If <paramref name="number" /> is not finite, zero is returned.
	/// </returns>
	public static IQuantity As(this int number, Enum unit) => ((double) number).As(unit);

	/// <inheritdoc cref="As(int, Enum)" />
	public static IQuantity As(this double number, Enum unit) => Quantity.From(number.AsFinite(), unit);

	/// <inheritdoc cref="Divide{TQuantity,TUnit}(TQuantity,double)" />
	/// <param name="unit">The required unit.</param>
	public static TQuantity Divide<TQuantity, TUnit>(this TQuantity quantity, double divisor, TUnit unit)
		where TQuantity : IQuantity<TUnit>
		where TUnit : Enum =>
		(TQuantity) (quantity.As(unit) / divisor).As(unit);

	/// <summary>
	///     Get a quantity divided by a double.
	/// </summary>
	public static TQuantity Divide<TQuantity, TUnit>(this TQuantity quantity, double divisor)
		where TQuantity : IQuantity<TUnit>
		where TUnit : Enum =>
		quantity.Divide(divisor, quantity.Unit);

	/// <summary>
	///     Get a quantity divided by another quantity.
	/// </summary>
	public static double Divide<TQuantity, TUnit>(this TQuantity quantity, TQuantity divisor)
		where TQuantity : IQuantity<TUnit>
		where TUnit : Enum =>
		quantity.Value / divisor.As(quantity.Unit);


	/// <summary>
	///     Get the <see cref="AreaUnit" /> based on <paramref name="unit" />.
	/// </summary>
	/// <returns>
	///     The square of <paramref name="unit" />, or <see cref="AreaUnit.Undefined" /> if <paramref name="unit" /> cannot be
	///     parsed.
	/// </returns>
	public static AreaUnit GetAreaUnit(this LengthUnit unit) =>
		Enum.TryParse<AreaUnit>($"Square{unit}", out var areaUnit)
			? areaUnit
			: AreaUnit.Undefined;

	/// <summary>
	///     Get the force unit associated to this force per length unit.
	/// </summary>
	/// <returns>
	///     The relative force unit, or <see cref="ForceUnit.Undefined" /> if <paramref name="unit" /> cannot be parsed.
	/// </returns>
	public static ForceUnit GetForceUnit(this ForcePerLengthUnit unit) =>
		Enum.TryParse<ForceUnit>(Regex.Split(unit.ToString(), "Per")[0], out var forceUnit)
			? forceUnit
			: ForceUnit.Undefined;

	/// <summary>
	///     Get the <see cref="LengthUnit" /> based on <paramref name="unit" />.
	/// </summary>
	/// <returns>
	///     The relative length unit, or <see cref="LengthUnit.Undefined" /> if <paramref name="unit" /> cannot be parsed.
	/// </returns>
	public static LengthUnit GetLenghtUnit(this AreaUnit unit) =>
		Enum.TryParse<LengthUnit>(Regex.Split(unit.ToString(), "Square")[1], out var lenghtUnit)
			? lenghtUnit
			: LengthUnit.Undefined;

	/// <summary>
	///     Get the lenght unit associated to this force per length unit.
	/// </summary>
	/// <inheritdoc cref="GetLenghtUnit(AreaUnit)" />
	public static LengthUnit GetLenghtUnit(this ForcePerLengthUnit unit) =>
		Enum.TryParse<LengthUnit>(Regex.Split(unit.ToString(), "Per")[1], out var lengthUnit)
			? lengthUnit
			: LengthUnit.Undefined;

	/// <summary>
	///     Get quantities from a collection of doubles.
	/// </summary>
	/// <param name="values">The double collection.</param>
	/// <param name="unit">The required unit.</param>
	public static IEnumerable<TQuantity> GetQuantities<TQuantity, TUnit>(this IEnumerable<double> values, TUnit unit)
		where TQuantity : IQuantity<TUnit>
		where TUnit : Enum =>
		values
			.Select(v => v.As(unit))
			.Cast<TQuantity>();

	/// <summary>
	///     Get the array of quantities from a 2D array of doubles.
	/// </summary>
	/// <param name="values">The 2D array of doubles.</param>
	/// <param name="unit">The required unit.</param>
	public static TQuantity[,] GetQuantities<TQuantity, TUnit>(this double[,] values, TUnit unit)
		where TQuantity : IQuantity<TUnit>
		where TUnit : Enum
	{
		// Initiate array
		var rows    = values.GetLength(0);
		var columns = values.GetLength(1);
		var dArray  = new TQuantity[rows, columns];

		for (var i = 0; i < rows; i++)
		for (var j = 0; j < columns; j++)
			dArray[i, j] = (TQuantity) values[i, j].As(unit);

		return dArray;
	}

	/// <summary>
	///     Get values from this quantity collection.
	/// </summary>
	/// <param name="quantities">The quantity collection.</param>
	/// <param name="unit">The required unit.</param>
	public static IEnumerable<double> GetValues<TQuantity, TUnit>(this IEnumerable<TQuantity> quantities, TUnit unit)
		where TQuantity : IQuantity<TUnit>
		where TUnit : Enum =>
		quantities
			.Select(q => q.As(unit));

	/// <inheritdoc cref="GetValues{TQuantity,TUnit}(IEnumerable{TQuantity}, TUnit)" />
	/// <remarks>
	///     This uses the unit of the first item of the collection.
	/// </remarks>
	public static IEnumerable<double> GetValues<TQuantity, TUnit>(this IEnumerable<TQuantity> quantities)
		where TQuantity : IQuantity<TUnit>
		where TUnit : Enum =>
		quantities
			.GetValues(quantities.First().Unit);

	/// <summary>
	///     Get the array of values from a 2D array of quantities.
	/// </summary>
	/// <param name="quantities">The 2D array of quantities.</param>
	/// <param name="unit">The required unit.</param>
	public static double[,] GetValues<TQuantity, TUnit>(this TQuantity[,] quantities, TUnit unit)
		where TQuantity : IQuantity<TUnit>
		where TUnit : Enum
	{
		// Initiate array
		var rows    = quantities.GetLength(0);
		var columns = quantities.GetLength(1);
		var dArray  = new double[rows, columns];

		for (var i = 0; i < rows; i++)
		for (var j = 0; j < columns; j++)
			dArray[i, j] = quantities[i, j].As(unit);

		return dArray;
	}

	/// <inheritdoc cref="GetValues{TQuantity,TUnit}(TQuantity[,],TUnit)" />
	/// <remarks>
	///     This uses the unit of the first item of the collection.
	/// </remarks>
	public static double[,] GetValues<TQuantity, TUnit>(this TQuantity[,] quantities)
		where TQuantity : IQuantity<TUnit>
		where TUnit : Enum =>
		quantities.GetValues(quantities[0, 0].Unit);

	/// <summary>
	///     Returns true if this <paramref name="quantity" /> is between two bounds, in any order.
	/// </summary>
	/// <inheritdoc cref="NumberExtensions.IsBetween(double, double, double)" />
	public static bool IsBetween<TQuantity>(this TQuantity quantity, TQuantity bound1, TQuantity bound2)
		where TQuantity : IQuantity, IComparable =>
		quantity.Value >= UnitMath.Min(bound1, bound2).As(quantity.Unit) &&
		quantity.Value <= UnitMath.Max(bound1, bound2).As(quantity.Unit);

	/// <summary>
	///     Returns true if this quantity has negative value.
	/// </summary>
	public static bool IsNegative<TQuantity>(this TQuantity quantity)
		where TQuantity : IQuantity =>
		quantity.Value < 0;

	/// <summary>
	///     Returns true if this quantity has positive value.
	/// </summary>
	public static bool IsPositive<TQuantity>(this TQuantity quantity)
		where TQuantity : IQuantity =>
		quantity.Value > 0;

	/// <inheritdoc cref="Multiply{TQuantity,TUnit}(TQuantity,double)" />
	/// <param name="unit">The required unit.</param>
	public static TQuantity Multiply<TQuantity, TUnit>(this TQuantity quantity, double multiplier, TUnit unit)
		where TQuantity : IQuantity<TUnit>
		where TUnit : Enum =>
		(TQuantity) (quantity.As(unit) * multiplier).As(unit);

	/// <summary>
	///     Get a quantity multiplied by a double.
	/// </summary>
	public static TQuantity Multiply<TQuantity, TUnit>(this TQuantity quantity, double multiplier)
		where TQuantity : IQuantity<TUnit>
		where TUnit : Enum =>
		quantity.Multiply(multiplier, quantity.Unit);

	/// <inheritdoc cref="Negate{TQuantity,TUnit}(TQuantity)" />
	/// <param name="unit">The required unit.</param>
	public static TQuantity Negate<TQuantity, TUnit>(this TQuantity quantity, TUnit unit)
		where TQuantity : IQuantity<TUnit>
		where TUnit : Enum =>
		(TQuantity) (-quantity.Value).As(unit);

	/// <summary>
	///     Get this quantity with a negated value.
	/// </summary>
	public static TQuantity Negate<TQuantity, TUnit>(this TQuantity quantity)
		where TQuantity : IQuantity<TUnit>
		where TUnit : Enum =>
		quantity.Negate(quantity.Unit);

	/// <summary>
	///     Get the force per length unit associated to this force unit and a length unit
	/// </summary>
	/// <returns>
	///     The relative force per length unit, or <see cref="ForcePerLengthUnit.Undefined" /> if it cannot be parsed.
	/// </returns>
	public static ForcePerLengthUnit Per(this ForceUnit forceUnit, LengthUnit lengthUnit) =>
		Enum.TryParse<ForcePerLengthUnit>($"{forceUnit}Per{lengthUnit}", out var unit)
			? unit
			: ForcePerLengthUnit.Undefined;

	/// <inheritdoc cref="Subtract{TQuantity,TUnit}(TQuantity,TQuantity)" />
	/// <param name="unit">The required unit.</param>
	public static TQuantity Subtract<TQuantity, TUnit>(this TQuantity quantity, TQuantity other, TUnit unit)
		where TQuantity : IQuantity<TUnit>
		where TUnit : Enum =>
		(TQuantity) (quantity.As(unit) - other.As(unit)).As(unit);

	/// <summary>
	///     Get a quantity with subtracted values.
	/// </summary>
	public static TQuantity Subtract<TQuantity, TUnit>(this TQuantity quantity, TQuantity other)
		where TQuantity : IQuantity<TUnit>
		where TUnit : Enum =>
		quantity.Subtract(other, quantity.Unit);

	#endregion

}