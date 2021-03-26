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
        /// Returns true if this <paramref name="length"/> is approximately equal to <paramref name="other"/>.
        /// </summary>
        /// <remarks>
        ///     If the difference between these values is smaller than <paramref name="tolerance"/>, true is returned.
        /// </remarks>
        /// <param name="length"></param>
        /// <param name="other">The other <see cref="Length"/>.</param>
        /// <param name="tolerance">The tolerance to consider <paramref name="length"/> approximately equal to <paramref name="other"/>.</param>
	    public static bool Approx(this Length length, Length other, Length tolerance) => (length - other).Abs() <= tolerance;

        /// <summary>
        /// Returns true if this <paramref name="length"/> is approximately equal to <see cref="Length.Zero"/>.
        /// </summary>
        /// <inheritdoc cref="Approx(Length,Length,Length)"/>
        public static bool ApproxZero(this Length length, Length tolerance) => length.Approx(Length.Zero, tolerance);

        /// <summary>
        /// Returns true if this <paramref name="area"/> is approximately equal to <paramref name="other"/>.
        /// </summary>
        /// <param name="other">The other <see cref="Area"/>.</param>
        /// <param name="tolerance">The tolerance to consider <paramref name="area"/> approximately equal to other.</param>
        /// <inheritdoc cref="Approx(Length,Length,Length)"/>
	    public static bool Approx(this Area area, Area other, Area tolerance) => (area - other).Abs() <= tolerance;

        /// <summary>
        /// Returns true if this <paramref name="area"/> is approximately equal to <see cref="Area.Zero"/>.
        /// </summary>
        /// <inheritdoc cref="Approx(Area, Area, Area)"/>
        public static bool ApproxZero(this Area area, Area tolerance) => area.Approx(Area.Zero, tolerance);

        /// <summary>
        /// Returns true if this <paramref name="force"/> is approximately equal to <paramref name="other"/>.
        /// </summary>
        /// <param name="other">The other <see cref="Force"/>.</param>
        /// <param name="tolerance">The tolerance to consider <paramref name="force"/> approximately equal to other.</param>
        /// <inheritdoc cref="Approx(Length,Length,Length)"/>
	    public static bool Approx(this Force force, Force other, Force tolerance) => (force - other).Abs() <= tolerance;

        /// <summary>
        /// Returns true if this <paramref name="force"/> is approximately equal to <see cref="Force.Zero"/>.
        /// </summary>
        /// <inheritdoc cref="Approx(Force, Force, Force)"/>
        public static bool ApproxZero(this Force force, Force tolerance) => force.Approx(Force.Zero, tolerance);

        /// <summary>
        /// Returns true if this <paramref name="pressure"/> is approximately equal to <paramref name="other"/>.
        /// </summary>
        /// <param name="other">The other <see cref="Pressure"/>.</param>
        /// <param name="tolerance">The tolerance to consider <paramref name="pressure"/> approximately equal to other.</param>
        /// <inheritdoc cref="Approx(Length,Length,Length)"/>
	    public static bool Approx(this Pressure pressure, Pressure other, Pressure tolerance) => (pressure - other).Abs() <= tolerance;

        /// <summary>
        /// Returns true if this <paramref name="pressure"/> is approximately equal to <see cref="Pressure.Zero"/>.
        /// </summary>
        /// <inheritdoc cref="Approx(Pressure, Pressure, Pressure)"/>
        public static bool ApproxZero(this Pressure pressure, Pressure tolerance) => pressure.Approx(Pressure.Zero, tolerance);

        /// <summary>
        /// Returns the abbreviation of this <paramref name="unit"/>.
        /// </summary>
	    public static string Abbrev(this LengthUnit unit) => Length.GetAbbreviation(unit);

        /// <inheritdoc cref="Abbrev(LengthUnit)"/>
	    public static string Abbrev(this ForceUnit unit) => Force.GetAbbreviation(unit);

        /// <inheritdoc cref="Abbrev(LengthUnit)"/>
	    public static string Abbrev(this PressureUnit unit) => Pressure.GetAbbreviation(unit);

        /// <inheritdoc cref="Abbrev(LengthUnit)"/>
        public static string Abbrev(this AreaUnit unit) => Area.GetAbbreviation(unit);

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
        /// Get the minimum value between two <see cref="Length"/>'s.
        /// </summary>
        public static Length Min(Length length1, Length length2) =>
	        length1 <= length2
		        ? length1
		        : length2;

        /// <summary>
        /// Get the maximum value between two <see cref="Length"/>'s.
        /// </summary>
        public static Length Max(Length length1, Length length2) =>
	        length1 >= length2
		        ? length1
		        : length2;

        /// <summary>
        /// Get the minimum value between two <see cref="Force"/>'s.
        /// </summary>
        public static Force Min(Force force1, Force force2) =>
	        force1 <= force2
		        ? force1
		        : force2;

        /// <summary>
        /// Get the maximum value between two <see cref="Force"/>'s.
        /// </summary>
        public static Force Max(Force force1, Force force2) =>
	        force1 >= force2
		        ? force1
		        : force2;

        /// <summary>
        /// Get the minimum value between two <see cref="Area"/>'s.
        /// </summary>
        public static Area Min(Area area1, Area area2) =>
	        area1 <= area2
		        ? area1
		        : area2;

        /// <summary>
        /// Get the maximum value between two <see cref="Area"/>'s.
        /// </summary>
        public static Area Max(Area area1, Area area2) =>
	        area1 >= area2
		        ? area1
		        : area2;

        /// <summary>
        /// Get the minimum value between two <see cref="Pressure"/>'s.
        /// </summary>
        public static Pressure Min(Pressure pressure1, Pressure pressure2) =>
	        pressure1 <= pressure2
		        ? pressure1
		        : pressure2;

        /// <summary>
        /// Get the maximum value between two <see cref="Pressure"/>'s.
        /// </summary>
        public static Pressure Max(Pressure pressure1, Pressure pressure2) =>
	        pressure1 >= pressure2
		        ? pressure1
		        : pressure2;

        /// <summary>
        ///     Verify if this <see cref="Length"/> is finite.
        /// </summary>
        /// <remarks>
        ///     Returns Zero if the value is <see cref="double.NaN"/>, <see cref="double.PositiveInfinity"/> or <see cref="double.NegativeInfinity"/>.
        /// </remarks>
        public static Length ToZero(this Length length) => length.Value.IsFinite() ? length : Length.Zero;

        /// <summary>
        ///     Verify if this <see cref="Area"/> is finite.
        /// </summary>
        /// <inheritdoc cref="ToZero(Length)"/>
        public static Area ToZero(this Area area) => area.Value.IsFinite() ? area : Area.Zero;

        /// <summary>
        ///     Verify if this <see cref="Force"/> is finite.
        /// </summary>
        /// <inheritdoc cref="ToZero(Length)"/>
        public static Force ToZero(this Force force) => force.Value.IsFinite() ? force : Force.Zero;

        /// <summary>
        ///     Verify if this <see cref="Pressure"/> is finite.
        /// </summary>
        /// <inheritdoc cref="ToZero(Length)"/>
        public static Pressure ToZero(this Pressure pressure) => pressure.Value.IsFinite() ? pressure : Pressure.Zero;
    }
}
