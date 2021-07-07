﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnitsNet;
using UnitsNet.Units;

namespace andrefmello91.Extensions
{
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

		/// <summary>
		///		Get values from this quantity collection.
		/// </summary>
		/// <param name="quantities">The quantity collection.</param>
		/// <param name="unit">The required unit.</param>
		public static IEnumerable<double> GetValues<TQuantity, TUnit>(this IEnumerable<TQuantity> quantities, TUnit unit)
			where TQuantity : IQuantity<TUnit>
			where TUnit : Enum =>
			quantities
				.Select(q => q.As(unit));

		/// <inheritdoc cref="GetValues{TQuantity,TUnit}(IEnumerable{TQuantity}, TUnit)"/>
		/// <remarks>
		///		This uses the unit of the first item of the collection.
		/// </remarks>
		public static IEnumerable<double> GetValues<TQuantity, TUnit>(this IEnumerable<TQuantity> quantities)
			where TQuantity : IQuantity<TUnit>
			where TUnit : Enum =>
			quantities
				.GetValues(quantities.First().Unit);

		/// <summary>
		///		Get quantities from a collection of doubles.
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
		///		Get the array of values from a 2D array of quantities.
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

		/// <inheritdoc cref="GetValues{TQuantity,TUnit}(TQuantity[,],TUnit)"/>
		/// <remarks>
		///		This uses the unit of the first item of the collection.
		/// </remarks>
		public static double[,] GetValues<TQuantity, TUnit>(this TQuantity[,] quantities)
			where TQuantity : IQuantity<TUnit>
			where TUnit : Enum =>
			quantities.GetValues(quantities[0, 0].Unit);

		/// <summary>
		///		Get the array of quantities from a 2D array of doubles.
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
		///     Get the <see cref="AreaUnit" /> based on <paramref name="unit" />.
		/// </summary>
		/// <remarks>
		///     If <paramref name="unit" /> is <see cref="LengthUnit.Millimeter" /> or <see cref="LengthUnit.Centimeter" />,
		///     <see cref="AreaUnit.SquareMillimeter" /> or <see cref="AreaUnit.SquareCentimeter" /> are returned; else
		///     <see cref="AreaUnit.SquareMeter" /> is returned.
		/// </remarks>
		public static AreaUnit GetAreaUnit(this LengthUnit unit) =>
			unit switch
			{
				LengthUnit.Millimeter => AreaUnit.SquareMillimeter,
				LengthUnit.Centimeter => AreaUnit.SquareCentimeter,
				_                     => AreaUnit.SquareMeter
			};

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

		#endregion

	}
}