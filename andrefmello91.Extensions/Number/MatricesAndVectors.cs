using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;


namespace andrefmello91.Extensions
{
    public static class LinearAlgebra
    {
        /// <summary>
        /// Convert this <paramref name="collection"/> to a <see cref="Vector"/>.
        /// </summary>
	    public static Vector<double> ToVector(this IEnumerable<double> collection) => Vector<double>.Build.DenseOfArray(collection.ToArray());

        /// <summary>
        /// Convert this <paramref name="array"/> to a <see cref="Matrix"/>.
        /// </summary>
	    public static Matrix<double> ToMatrix(this double[,] array) => Matrix<double>.Build.DenseOfArray(array);

        /// <summary>
        /// Returns true if this <paramref name="matrix"/> contains at least one <see cref="double.NaN"/>.
        /// </summary>
        public static bool ContainsNaN(this Matrix<double> matrix) => matrix.Exists(d => d.IsNaN());
		
        /// <summary>
        ///		Check if this <paramref name="matrix"/> is square.
        /// </summary>
        /// <returns>
        ///	True if <see cref="Matrix{T}.RowCount"/> is equal to <see cref="Matrix{T}.ColumnCount"/>.
        /// </returns>
        public static bool IsSquare<T>(this Matrix<T> matrix) where T : struct, IEquatable<T>, IFormattable =>
	        matrix.RowCount == matrix.ColumnCount;
    }
}
