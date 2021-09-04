using System.Linq;
using System.Text.RegularExpressions;

namespace andrefmello91.Extensions
{
	public static class StringExtensions
	{

		#region Methods

		/// <summary>
		///     Verify if a <see cref="char" /> is valid to a double number.
		/// </summary>
		/// <param name="c">The <see cref="char" />.</param>
		/// <param name="decimalSeparator">The decimal separator.</param>
		/// <param name="allowNegative">Allow negative signal?</param>
		/// <returns>
		///     True if the <see cref="char" /> is valid to a double number.
		/// </returns>
		public static bool IsValidForDouble(this char c, char decimalSeparator = '.', bool allowNegative = true) =>
			char.IsDigit(c) || c == decimalSeparator || c is 'e' or 'E' || allowNegative && c == '-';

		/// <summary>
		///     Verify if a <see cref="char" /> is valid to an integer number.
		/// </summary>
		/// <param name="c">The <see cref="char" />.</param>
		/// <param name="allowNegative">Allow negative signal?</param>
		/// <returns>
		///     True if the <see cref="char" /> is valid to an integer number.
		/// </returns>
		public static bool IsValidForInt(this char c, bool allowNegative = true) =>
			char.IsDigit(c) || allowNegative && c == '-';

		/// <summary>
		///     Split a <see cref="string" /> at each capital letter.
		/// </summary>
		public static string SplitCamelCase(this string source) =>
			Regex.Split(source, @"(?<!^)(?=[A-Z])").Aggregate((i, f) => $"{i} {f}");

		#endregion

	}
}