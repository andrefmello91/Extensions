using System.Linq;
using Autodesk.AutoCAD.Geometry;
using MathNet.Numerics;

namespace Extensions
{
    public static class AutoCADExtensions
    {
	    /// <summary>
        /// Return the angle (in radians), related to horizontal axis, of a line that connects this to <paramref name="otherPoint"/> .
        /// </summary>
        /// <param name="otherPoint">The other <see cref="Point3d"/>.</param>
        /// <param name="tolerance">The tolerance to consider being zero.</param>
        public static double AngleTo(this Point3d point, Point3d otherPoint, double tolerance = 1E-6)
	    {
		    double
			    x = otherPoint.X - point.X,
			    y = otherPoint.Y - point.Y;

		    if (x.Abs() < tolerance && y.Abs() < tolerance)
			    return 0;

		    if (y.Abs() < tolerance)
		    {
			    if (x > 0)
				    return 0;

			    return Constants.Pi;
		    }

		    if (x.Abs() < tolerance)
		    {
			    if (y > 0)
				    return Constants.PiOver2;

			    return Constants.Pi3Over2;
		    }

		    return
			    Trig.Cos(y / x).CoerceZero(1E-6);
	    }

        /// <summary>
        /// Return the mid <see cref="Point3d"/> between this and <paramref name="otherPoint"/>.
        /// </summary>
        /// <param name="otherPoint">The other <see cref="Point3d"/>.</param>
        public static Point3d MidPoint(this Point3d point, Point3d otherPoint) => point == otherPoint ? point : new Point3d(0.5 * (point.X + otherPoint.X), 0.5 * (point.Y + otherPoint.Y), 0.5 * (point.Z + otherPoint.Z));

		/// <summary>
        /// Return this array of <see cref="Point3d"/> ordered in ascending Y then ascending X.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Point3d[] Order(this Point3d[] points) => points.OrderBy(p => p.Y).ThenBy(p => p.X).ToArray();
    }
}
