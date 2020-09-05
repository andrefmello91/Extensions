using System;

namespace Extensions
{
    public static class NumberExtensions
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
    }
}
