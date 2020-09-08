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
		/// Returns the direction cosines (cos, sin) of an <paramref name="angle"/> in radians.
		/// </summary>
		/// <param name="absoluteValue">Return absolute values? (default: false).</param>
		public static (double cos, double sin) DirectionCosines(this double angle, bool absoluteValue = false) =>
			absoluteValue
				? (Trig.Cos(angle).CoerceZero(1E-6).Abs(), Trig.Sin(angle).CoerceZero(1E-6).Abs())
				: (Trig.Cos(angle).CoerceZero(1E-6), Trig.Sin(angle).CoerceZero(1E-6));
    }
}
