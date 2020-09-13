using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Extensions.Number;
using MathNet.Numerics;
using UnitsNet;
using UnitsNet.Units;
using static Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace Extensions.AutoCAD
{
    public static class Extensions
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
        /// Return a <see cref="Point3d"/> with coordinates converted.
        /// </summary>
        /// <param name="fromUnit">The <see cref="LengthUnit"/> of origin.</param>
        /// <param name="toUnit">The <seealso cref="LengthUnit"/> to convert.</param>
        /// <returns></returns>
        public static Point3d Convert(this Point3d point, LengthUnit fromUnit, LengthUnit toUnit = LengthUnit.Millimeter) => fromUnit == toUnit ? point : new Point3d(point.X.Convert(fromUnit, toUnit), point.Y.Convert(fromUnit, toUnit), point.Z.Convert(fromUnit, toUnit));

		/// <summary>
        /// Returns true if this <paramref name="point"/> is approximately equal to <paramref name="otherPoint"/>.
        /// </summary>
        /// <param name="otherPoint">The other <see cref="Point3d"/> to compare.</param>
        /// <param name="tolerance">The tolerance to considering equivalent.</param>
        /// <returns></returns>
		public static bool Approx(this Point3d point, Point3d otherPoint, double tolerance = 1E-3)
			=> point.X.Approx(otherPoint.X, tolerance) && point.Y.Approx(otherPoint.Y, tolerance) && point.Z.Approx(otherPoint.Z, tolerance);

		/// <summary>
        /// Return this array of <see cref="Point3d"/> ordered in ascending Y then ascending X.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Point3d[] Order(this Point3d[] points) => points.OrderBy(p => p.Y).ThenBy(p => p.X).ToArray();

        /// <summary>
        /// Convert to a <see cref="List{T}"/> of <see cref="Point3d"/>.
        /// </summary>
        public static List<Point3d> ToList(this Point3dCollection points) => points.Cast<Point3d>().ToList();

        /// <summary>
        /// Convert to an <see cref="Array"/> of <see cref="Point3d"/>.
        /// </summary>
        public static Point3d[] ToArray(this Point3dCollection points) => points.ToList().ToArray();

		/// <summary>
        /// Convert this <paramref name="value"/> to a <see cref="double"/>.
        /// </summary>
        public static double ToDouble(this TypedValue value) => System.Convert.ToDouble(value.Value);

		/// <summary>
        /// Convert this <paramref name="value"/> to an <see cref="int"/>.
        /// </summary>
        public static int ToInt(this TypedValue value) => System.Convert.ToInt32(value.Value);

        /// <summary>
        /// Read a <see cref="DBObject"/> in the drawing from this <see cref="ObjectId"/>.
        /// </summary>
        public static DBObject ToDBObject(this ObjectId objectId)
		{
			// Start a transaction
			using (var trans = DocumentManager.MdiActiveDocument.Database.TransactionManager.StartTransaction())
				// Read the object as a point
				return trans.GetObject(objectId, OpenMode.ForRead);
		}

        /// <summary>
        /// Read a <see cref="Entity"/> in the drawing from this <see cref="ObjectId"/>.
        /// </summary>
        public static Entity ToEntity(this ObjectId objectId) => (Entity) objectId.ToDBObject();

        /// <summary>
        /// Read this <see cref="DBObject"/>'s XData as an <see cref="Array"/> of <see cref="TypedValue"/>.
        /// </summary>
        /// <param name="appName">The application name.</param>
        public static TypedValue[] ReadXData(this DBObject dbObject, string appName) => dbObject.GetXDataForApplication(appName).AsArray();

        /// <summary>
        /// Read this <see cref="ObjectId"/>'s XData as an <see cref="Array"/> of <see cref="TypedValue"/>.
        /// </summary>
        /// <param name="appName">The application name.</param>
        public static TypedValue[] ReadXData(ObjectId objectId, string appName)
        {
	        // Start a transaction
	        using (var trans = DocumentManager.MdiActiveDocument.Database.TransactionManager.StartTransaction())
	        {
		        // Get the NOD in the database
		        return trans.GetObject(objectId, OpenMode.ForRead).ReadXData(appName);
	        }
        }
    }
}
