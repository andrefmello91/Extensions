using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using UnitsNet;

namespace andrefmello91.Extensions
{
	public static class LinearAlgebra
	{

		#region Methods

		/// <summary>
		///     Returns true if this <paramref name="matrix" /> contains at least one <see cref="double.NaN" /> of infinity.
		/// </summary>
		public static bool ContainsNaN(this Matrix<double> matrix) => matrix.Exists(d => !d.IsFinite());

		/// <summary>
		///     Check if this <paramref name="matrix" /> is square.
		/// </summary>
		/// <returns>
		///     True if <see cref="Matrix{T}.RowCount" /> is equal to <see cref="Matrix{T}.ColumnCount" />.
		/// </returns>
		public static bool IsSquare<T>(this Matrix<T> matrix) where T : struct, IEquatable<T>, IFormattable =>
			matrix.RowCount == matrix.ColumnCount;

		/// <summary>
		///     Convert this <paramref name="array" /> to a <see cref="Matrix" />.
		/// </summary>
		public static Matrix<double> ToMatrix(this double[,] array) => Matrix<double>.Build.DenseOfArray(array);

		/// <summary>
		///     Convert this <paramref name="array" /> to a <see cref="Matrix" />, with components in <paramref name="unit"/>.
		/// </summary>
		public static Matrix<double> ToMatrix<TQuantity, TUnit>(this TQuantity[,] array, TUnit unit)
			where TQuantity : IQuantity<TUnit>
			where TUnit : Enum =>
			array
				.GetValues(unit)
				.ToMatrix();

		/// <inheritdoc cref="ToMatrix{TQuantity,TUnit}(TQuantity[,], TUnit)"/>
		/// <remarks>
		///		This uses the unit of the matrix's first component.
		/// </remarks>
		public static Matrix<double> ToMatrix<TQuantity, TUnit>(this TQuantity[,] array)
			where TQuantity : IQuantity<TUnit>
			where TUnit : Enum =>
			array.ToMatrix(array[0, 0].Unit);
		
		/// <summary>
		///     Convert this <paramref name="collection" /> to a <see cref="Vector" />.
		/// </summary>
		public static Vector<double> ToVector(this IEnumerable<double> collection) => Vector<double>.Build.DenseOfArray(collection.ToArray());
		
		/// <summary>
		///     Convert this <paramref name="collection" /> to a <see cref="Vector" /> with components in <paramref name="unit"/>.
		/// </summary>
		/// <param name="unit">The required unit to convert the components.</param>
		public static Vector<double> ToVector<TQuantity, TUnit>(this IEnumerable<TQuantity> collection, TUnit unit) 
			where TQuantity : IQuantity<TUnit>
			where TUnit : Enum =>
			collection
				.GetValues(unit)
				.ToVector();
		
		/// <inheritdoc cref="ToVector{TQuantity, TUnit}(IEnumerable{TQuantity}, TUnit)"/>
		/// <remarks>
		///		This uses the unit of the vector's first component.
		/// </remarks>
		public static Vector<double> ToVector<TQuantity, TUnit>(this IEnumerable<TQuantity> collection) 
			where TQuantity : IQuantity<TUnit>
			where TUnit : Enum =>
			collection
				.ToVector(collection.First().Unit);
		
		
		#endregion

	}
}