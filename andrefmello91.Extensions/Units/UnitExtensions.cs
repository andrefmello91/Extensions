using System;
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