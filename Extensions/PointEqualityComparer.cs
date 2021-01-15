using System.Collections.Generic;
using Autodesk.AutoCAD.Geometry;
using UnitsNet.Units;

namespace Extensions.AutoCAD
{
	/// <summary>
	/// <see cref="Point3d"/> equality comparer class.
	/// </summary>
    public class Point3dEqualityComparer : IEqualityComparer<Point3d>
    {
		/// <summary>
		/// Returns true if this <paramref name="point"/> is approximately equal to <paramref name="otherPoint"/>.
		/// </summary>
	    public bool Equals(Point3d point, Point3d otherPoint) => point.Approx(otherPoint);

		/// <summary>
		/// Returns true if this <paramref name="point"/> is approximately equal to <paramref name="otherPoint"/>.
		/// </summary>
		/// <param name="tolerance">The tolerance to considering equivalent.</param>
		public bool Equals(Point3d point, Point3d otherPoint, double tolerance) => point.Approx(otherPoint, tolerance);

		public int GetHashCode(Point3d obj) => obj.GetHashCode();
    }
}
