﻿using Extensions.Number;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Extensions.LinearAlgebra
{
    public static class MatricesAndVectors
    {
        /// <summary>
        /// Convert this <paramref name="array"/> to a <see cref="Vector"/>.
        /// </summary>
        /// <param name="array"></param>
	    public static Vector<double> ToVector(this double[] array) => Vector<double>.Build.DenseOfArray(array);

        /// <summary>
        /// Convert this <paramref name="array"/> to a <see cref="Matrix"/>.
        /// </summary>
        /// <param name="array"></param>
	    public static Matrix<double> ToMatrix(this double[,] array) => Matrix<double>.Build.DenseOfArray(array);

        /// <summary>
        /// Returns true if this <paramref name="matrix"/> contains at least one <see cref="double.NaN"/>.
        /// </summary>
        public static bool ContainsNaN(this Matrix<double> matrix) => matrix.Exists(d => d.IsNaN());
    }
}
