using System;
using System.Globalization;
using MathNet.Numerics;
using UnitsNet;
using UnitsNet.Units;

namespace andrefmello91.Extensions
{
    /// <summary>
    /// Extensions for <see cref="UnitsNet"/>.
    /// </summary>
    public static partial class UnitExtensions
    {
	    /// <summary>
        /// Returns true if this <paramref name="quantity"/> is approximately equal to <paramref name="other"/>.
        /// </summary>
        /// <remarks>
	    ///     If the difference between these values is smaller than <paramref name="tolerance"/>, true is returned.
	    /// </remarks>
	    /// <param name="other">The other quantity.</param>
        /// <param name="tolerance">The tolerance to consider <paramref name="quantity"/> approximately equal to other.</param>
	    public static bool Approx<TQuantity>(this TQuantity quantity, TQuantity other, TQuantity tolerance)
	        where TQuantity : IQuantity =>
				(quantity.Value - other.ToUnit(quantity.Unit).Value).Abs() <= tolerance.ToUnit(quantity.Unit).Value.Abs();

        /// <summary>
        ///		Returns true if this <paramref name="quantity"/> is approximately equal to zero.
        /// </summary>
        /// <inheritdoc cref="Approx{TQuantity}"/>
        public static bool ApproxZero<TQuantity>(this TQuantity quantity, TQuantity tolerance)
	        where TQuantity : IQuantity =>
				quantity.Value.Abs() <= tolerance.ToUnit(quantity.Unit).Value.Abs();

        /// <summary>
        ///		Returns the abbreviation of this <paramref name="unit"/>.
        /// </summary>
        public static string Abbrev<TUnit>(this TUnit unit)
	        where TUnit : Enum =>
				UnitAbbreviationsCache.Default.GetDefaultAbbreviation(unit);

        /// <summary>
        ///     Get the <see cref="AreaUnit"/> based on <paramref name="unit"/>.
        /// </summary>
        /// <remarks>
        ///     If <paramref name="unit"/> is <see cref="LengthUnit.Millimeter"/> or <see cref="LengthUnit.Centimeter"/>, <see cref="AreaUnit.SquareMillimeter"/> or <see cref="AreaUnit.SquareCentimeter"/> are returned; else <see cref="AreaUnit.SquareMeter"/> is returned.
        /// </remarks>
        public static AreaUnit GetAreaUnit(this LengthUnit unit) =>
	        unit switch
	        {
		        LengthUnit.Millimeter => AreaUnit.SquareMillimeter,
		        LengthUnit.Centimeter => AreaUnit.SquareCentimeter,
		        _                     => AreaUnit.SquareMeter
	        };

        /// <summary>
        ///     Verify if this <see cref="Force"/> is finite.
        /// </summary>
        /// <returns>
        ///     Zero if the value is <see cref="double.NaN"/>, <see cref="double.PositiveInfinity"/> or <see cref="double.NegativeInfinity"/>.
        /// </returns>
        public static TQuantity ToZero<TQuantity>(this TQuantity force) where TQuantity : IQuantity, new() =>
	        force.Value.IsFinite() ? force : new TQuantity();

        /// <summary>
        ///		Returns true if this quantity has positive value.
        /// </summary>
        public static bool IsPositive<TQuantity>(this TQuantity quantity)
	        where TQuantity : IQuantity =>
				quantity.Value > 0;
        
        /// <summary>
        ///		Returns true if this quantity has negative value.
        /// </summary>
        public static bool IsNegative<TQuantity>(this TQuantity quantity)
	        where TQuantity : IQuantity =>
				quantity.Value < 0;
    }
}
