using System;
using MathNet.Numerics;

namespace Extensions.Number
{
    public static class Extensions
    {
	    /// <summary>
        /// Return the absolute value of this <paramref name="number"/>.
        /// </summary>
	    public static double Abs(this double number) => Math.Abs(number);

		/// <summary>
        /// Return the absolute value of this <paramref name="number"/>.
        /// </summary>
	    public static int Abs(this int number) => Math.Abs(number);

		/// <summary>
        /// Returns this <paramref name="number"/> elevated to <paramref name="power"/>.
        /// </summary>
		public static double Pow(this double number, double power) => Math.Pow(number, power);

		/// <summary>
		/// Returns this <paramref name="number"/> elevated to <paramref name="power"/>.
		/// </summary>
		public static double Pow(this double number, int power) => Math.Pow(number, power);

		/// <summary>
        /// Returns this <paramref name="number"/> elevated to <paramref name="power"/>.
        /// </summary>
		public static double Pow(this int number, double power) => Math.Pow(number, power);

		/// <summary>
		/// Returns this <paramref name="number"/> elevated to <paramref name="power"/>.
		/// </summary>
		public static double Pow(this int number, int power) => Math.Pow(number, power);

		/// <summary>
		/// Returns this <paramref name="number"/>'s square root.
		/// </summary>
		public static double Sqrt(this double number) => Math.Sqrt(number);

		/// <summary>
		/// Returns this <paramref name="number"/>'s square root.
		/// </summary>
		public static double Sqrt(this int number) => Math.Sqrt(number);

		/// <summary>
		/// Returns the cosine of an <paramref name="angle"/> in radians.
		/// </summary>
		/// <param name="absoluteValue">Return absolute values? (default: false).</param>
		public static double Cos(this double angle, bool absoluteValue = false) =>
			absoluteValue
				? Trig.Cos(angle).CoerceZero(1E-6).Abs()
				: Trig.Cos(angle).CoerceZero(1E-6);

		/// <summary>
		/// Returns the sine of an <paramref name="angle"/> in radians.
		/// </summary>
		/// <param name="absoluteValue">Return absolute values? (default: false).</param>
		public static double Sin(this double angle, bool absoluteValue = false) =>
			absoluteValue
				? Trig.Sin(angle).CoerceZero(1E-6).Abs()
				: Trig.Sin(angle).CoerceZero(1E-6);

		/// <summary>
		/// Returns the tangent of an <paramref name="angle"/> in radians.
		/// </summary>
		/// <param name="absoluteValue">Return absolute values? (default: false).</param>
		public static double Tan(this double angle, bool absoluteValue = false) =>
			absoluteValue
				? Trig.Tan(angle).CoerceZero(1E-6).Abs()
				: Trig.Tan(angle).CoerceZero(1E-6);

		/// <summary>
		/// Returns the cotangent of an <paramref name="angle"/> in radians.
		/// </summary>
		/// <param name="absoluteValue">Return absolute values? (default: false).</param>
		public static double Cotan(this double angle, bool absoluteValue = false) => 1 / angle.Tan(absoluteValue);

		/// <summary>
		/// Returns the arc-cosine of a <paramref name="cosine"/>, in radians.
		/// </summary>
		public static double Acos(this double cosine) => Trig.Acos(cosine);

		/// <summary>
		/// Returns the arc-sine of a <paramref name="sine"/>, in radians.
		/// </summary>
		public static double Asin(this double sine) => Trig.Asin(sine);

		/// <summary>
		/// Returns the arc-tangent of a <paramref name="tangent"/>, in radians.
		/// </summary>
		public static double Atan(this double tangent) => Trig.Atan(tangent);

		/// <summary>
		/// Returns the direction cosines (cos, sin) of an <paramref name="angle"/> in radians.
		/// </summary>
		/// <param name="absoluteValue">Return absolute values? (default: false).</param>
		public static (double cos, double sin) DirectionCosines(this double angle, bool absoluteValue = false) => (angle.Cos(absoluteValue), angle.Sin(absoluteValue));

		/// <summary>
		/// Return zero if <paramref name="number"/> is <see cref="double.NaN"/> or <see cref="double.PositiveInfinity"/> or <see cref="double.NegativeInfinity"/>.
		/// </summary>
		public static double ToZero(this double number) => !double.IsNaN(number) && !double.IsInfinity(number) ? number : 0;

		/// <summary>
        /// Returns true if <paramref name="number"/> is approximately zero, in given <paramref name="tolerance"/>.
        /// </summary>
        /// <param name="tolerance">The tolerance for approximating to zero.
        /// <para>Default: 1E-6.</para></param>
		public static bool ApproxZero(this double number, double tolerance = 1E-6) => number.Abs() <= tolerance;

		/// <summary>
		/// Returns true if <paramref name="number"/> is approximated equal to <paramref name="otherNumber"/>, in given <paramref name="tolerance"/>.
		/// </summary>
		/// <param name="otherNumber">The number to compare.</param>
		/// <param name="tolerance">The tolerance for approximating both numbers.
		/// <para>Default: 1E-6.</para></param>
		public static bool Approx(this double number, double otherNumber, double tolerance = 1E-6) => (number - otherNumber).Abs() <= tolerance;

		/// <summary>
        /// Returns true if this <paramref name="number"/> is between two bounds, in any order.
        /// </summary>
        /// <param name="bound1">First bound.</param>
        /// <param name="bound2">Second bound.</param>
		public static bool IsBetween(this double number, double bound1, double bound2) => number > Math.Min(bound1, bound2) && number < Math.Max(bound1, bound2);

		/// <summary>
        /// Returns true if this <paramref name="number"/> is between two bounds, in any order.
        /// </summary>
        /// <param name="bound1">First bound.</param>
        /// <param name="bound2">Second bound.</param>
		public static bool IsBetween(this int number, double bound1, double bound2) => number > Math.Min(bound1, bound2) && number < Math.Max(bound1, bound2);

        /// <summary>
        /// Convert an <paramref name="angle"/>, in radians, to degrees.
        /// </summary>
        public static double ToDegree(this double angle) => Trig.RadianToDegree(angle);

        /// <summary>
        /// Convert an <paramref name="angle"/>, in radians, to degrees.
        /// </summary>
        public static double ToDegree(this int angle) => Trig.RadianToDegree(angle);

        /// <summary>
        /// Convert an <paramref name="angle"/>, in degrees, to radians.
        /// </summary>
        public static double ToRadian(this double angle) => Trig.DegreeToRadian(angle);

        /// <summary>
        /// Convert an <paramref name="angle"/>, in degrees, to radians.
        /// </summary>
        public static double ToRadian(this int angle) => Trig.DegreeToRadian(angle);
    }
}
