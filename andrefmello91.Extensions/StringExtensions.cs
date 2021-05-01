using System.Linq;
using System.Text.RegularExpressions;

namespace andrefmello91.Extensions
{
	public static class StringExtensions
	{

		#region Methods

		/// <summary>
		///     Split a <see cref="string" /> at each capital letter.
		/// </summary>
		public static string SplitCamelCase(this string source) =>
			Regex.Split(source, @"(?<!^)(?=[A-Z])").Aggregate((i, f) => $"{i} {f}");

		#endregion

	}
}