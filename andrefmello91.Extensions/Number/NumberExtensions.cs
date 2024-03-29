﻿using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics;
using UnitsNet;

namespace andrefmello91.Extensions;

public static partial class NumberExtensions
{

	#region Methods

	/// <summary>
	///     Return the absolute value of this <paramref name="number" />.
	/// </summary>
	public static double Abs(this double number) => Math.Abs(number);

	/// <summary>
	///     Return the absolute value of this <paramref name="number" />.
	/// </summary>
	public static int Abs(this int number) => Math.Abs(number);

	/// <summary>
	///     Returns true if <paramref name="number" /> is approximated equal to <paramref name="otherNumber" />, in given
	///     <paramref name="tolerance" />.
	/// </summary>
	/// <param name="otherNumber">The number to compare.</param>
	/// <param name="tolerance">
	///     The tolerance for approximating both numbers.
	///     <para>Default: 1E-12.</para>
	/// </param>
	public static bool Approx(this double number, double otherNumber, double tolerance = 1E-12) => (number - otherNumber).Abs() <= tolerance;

	/// <summary>
	///     Returns true if <paramref name="number" /> is approximately zero, in given <paramref name="tolerance" />.
	/// </summary>
	/// <param name="tolerance">
	///     The tolerance for approximating to zero.
	///     <para>Default: 1E-12.</para>
	/// </param>
	public static bool ApproxZero(this double number, double tolerance = 1E-12) => number.Abs() <= tolerance;

	/// <summary>
	///     Correct this <paramref name="number" /> to a finite value.
	/// </summary>
	/// <returns>
	///     The <paramref name="number" /> itself if it's not <see cref="double.NaN" /> or infinity, otherwise zero.
	/// </returns>
	public static double AsFinite(this double number) => number.IsFinite() ? number : 0;

	/// <summary>
	///     Returns true if this <paramref name="collection" /> contains at least one <see cref="double.NaN" /> or Infinity.
	/// </summary>
	public static bool ContainsNaNOrInfinity(this IEnumerable<double> collection) => collection.Any(d => !d.IsFinite());

	/// <inheritdoc cref="ContainsNaNOrInfinity" />
	public static bool ContainsNaNOrInfinity<TQuantity>(this IEnumerable<TQuantity> collection)
		where TQuantity : IQuantity => collection.Select(q => q.Value).ContainsNaNOrInfinity();

	/// <summary>
	///     Returns true if this <paramref name="number" /> is between two bounds, in any order.
	/// </summary>
	/// <param name="bound1">First bound.</param>
	/// <param name="bound2">Second bound.</param>
	public static bool IsBetween(this double number, double bound1, double bound2) => number > Math.Min(bound1, bound2) && number < Math.Max(bound1, bound2);

	/// <summary>
	///     Returns true if this <paramref name="number" /> is between two bounds, in any order.
	/// </summary>
	/// <param name="bound1">First bound.</param>
	/// <param name="bound2">Second bound.</param>
	public static bool IsBetween(this int number, double bound1, double bound2) => number > Math.Min(bound1, bound2) && number < Math.Max(bound1, bound2);

	/// <summary>
	///     Try parse a string and verify if is not zero.
	/// </summary>
	/// <param name="numberAsString">String to parse.</param>
	/// <param name="number">The parsed number.</param>
	public static bool ParsedAndNotZero(this string numberAsString, out double number)
	{
		var parsed = double.TryParse(numberAsString, out number);

		return parsed && !number.ApproxZero();
	}

	/// <summary>
	///     Returns this <paramref name="number" /> elevated to <paramref name="power" />.
	/// </summary>
	public static double Pow(this double number, double power) => Math.Pow(number, power);

	/// <summary>
	///     Returns this <paramref name="number" /> elevated to <paramref name="power" />.
	/// </summary>
	public static double Pow(this double number, int power) => Math.Pow(number, power);

	/// <summary>
	///     Returns this <paramref name="number" /> elevated to <paramref name="power" />.
	/// </summary>
	public static double Pow(this int number, double power) => Math.Pow(number, power);

	/// <summary>
	///     Returns this <paramref name="number" /> elevated to <paramref name="power" />.
	/// </summary>
	public static double Pow(this int number, int power) => Math.Pow(number, power);

	/// <summary>
	///     Round this <paramref name="number" />.
	/// </summary>
	/// <param name="digits">The number of digits.</param>
	public static double Round(this double number, int digits = 2) => Math.Round(number, digits);

	/// <summary>
	///     Returns this <paramref name="number" />'s square root.
	/// </summary>
	public static double Sqrt(this double number) => Math.Sqrt(number);

	/// <summary>
	///     Returns this <paramref name="number" />'s square root.
	/// </summary>
	public static double Sqrt(this int number) => Math.Sqrt(number);

	#endregion

}