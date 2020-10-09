using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
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
	    /// Return the horizontal distance from this <paramref name="point"/> to <paramref name="otherPoint"/> .
	    /// </summary>
	    /// <param name="otherPoint">The other <see cref="Point3d"/>.</param>
	    public static double DistanceInX(this Point3d point, Point3d otherPoint) => (otherPoint.X - point.X).Abs();

	    /// <summary>
	    /// Return the vertical distance from this <paramref name="point"/> to <paramref name="otherPoint"/> .
	    /// </summary>
	    /// <param name="otherPoint">The other <see cref="Point3d"/>.</param>
	    public static double DistanceInY(this Point3d point, Point3d otherPoint) => (otherPoint.Y - point.Y).Abs();
	    
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
        /// Return this collection of <see cref="Point3d"/> ordered in ascending Y then ascending X.
        /// </summary>
        /// <param name="points"></param>
        public static IEnumerable<Point3d> Order(this IEnumerable<Point3d> points) => points.OrderBy(p => p.Y).ThenBy(p => p.X);

        /// <summary>
        /// Convert to a <see cref="IEnumerable{T}"/> of <see cref="Point3d"/>.
        /// </summary>
        public static IEnumerable<Point3d> ToCollection(this Point3dCollection points) => points.Cast<Point3d>();

        /// <summary>
        /// Convert to a <see cref="IEnumerable{T}"/> of <see cref="ObjectId"/>.
        /// </summary>
        public static IEnumerable<ObjectId> ToCollection(this ObjectIdCollection objectIds) => objectIds.Cast<ObjectId>();

        /// <summary>
        /// Convert to a <see cref="IEnumerable{T}"/> of <see cref="DBObject"/>.
        /// </summary>
        public static IEnumerable<DBObject> ToCollection(this DBObjectCollection objects) => objects.Cast<DBObject>();

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
        /// Return a <see cref="DBObjectCollection"/> from an <see cref="ObjectIdCollection"/>.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static DBObjectCollection ToDBObjectCollection(this ObjectIdCollection collection)
        {
	        if (collection is null || collection.Count == 0)
		        return null;

			var dbCollection = new DBObjectCollection();

	        // Start a transaction
	        using (var trans = DocumentManager.MdiActiveDocument.Database.TransactionManager.StartTransaction())
		        foreach (ObjectId objectId in collection)
			        dbCollection.Add(trans.GetObject(objectId, OpenMode.ForRead));

	        return dbCollection;
        }

        /// <summary>
        /// Return an <see cref="ObjectIdCollection"/> from an<see cref="DBObjectCollection"/>.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static ObjectIdCollection ToObjectIdCollection(this DBObjectCollection collection)
        {
	        if (collection is null || collection.Count == 0)
		        return null;

	        return
				new ObjectIdCollection((from DBObject obj in collection select obj.ObjectId).ToArray());
        }

		/// <summary>
        /// Get the collection of <see cref="ObjectId"/>'s of <paramref name="objects"/>.
        /// </summary>
        public static IEnumerable<ObjectId> GetObjectIds(this IEnumerable<DBObject> objects) => objects.Select(obj => obj.ObjectId);

		/// <summary>
		/// Get the collection of <see cref="ObjectId"/>'s of <paramref name="objectIds"/>.
		/// </summary>
		public static IEnumerable<DBObject> GetDBObjects(this IEnumerable<ObjectId> objectIds)
		{
			// Start a transaction
			using (var trans = DocumentManager.MdiActiveDocument.Database.TransactionManager.StartTransaction())
				return objectIds.Select(obj => trans.GetObject(obj, OpenMode.ForRead));
		}

        /// <summary>
        /// Get the <see cref="Point3d"/> vertices of this <paramref name="solid"/>.
        /// </summary>
        public static IEnumerable<Point3d> GetVertices(this Solid solid)
        {
	        using (var points = new Point3dCollection())
	        {
		        solid.GetGripPoints(points, new IntegerCollection(), new IntegerCollection());
		        return points.Cast<Point3d>();
	        }
        }

        /// <summary>
        /// Read this <see cref="DBObject"/>'s XData as an <see cref="Array"/> of <see cref="TypedValue"/>.
        /// </summary>
        /// <param name="appName">The application name.</param>
        public static TypedValue[] ReadXData(this DBObject dbObject, string appName) => dbObject.GetXDataForApplication(appName).AsArray();

        /// <summary>
        /// Read this <see cref="Entity"/>'s XData as an <see cref="Array"/> of <see cref="TypedValue"/>.
        /// </summary>
        /// <param name="appName">The application name.</param>
        public static TypedValue[] ReadXData(this Entity entity, string appName) => entity.GetXDataForApplication(appName).AsArray();

        /// <summary>
        /// Read this <see cref="ObjectId"/>'s XData as an <see cref="Array"/> of <see cref="TypedValue"/>.
        /// </summary>
        /// <param name="appName">The application name.</param>
        public static TypedValue[] ReadXData(this ObjectId objectId, string appName)
        {
	        // Start a transaction
	        using (var trans = DocumentManager.MdiActiveDocument.Database.TransactionManager.StartTransaction())
	        {
		        // Get the NOD in the database
		        return trans.GetObject(objectId, OpenMode.ForRead).ReadXData(appName);
	        }
        }

		/// <summary>
        /// Set extended data to this <paramref name="objectId"/>.
        /// </summary>
        /// <param name="objectId">The <see cref="ObjectId"/>.</param>
        /// <param name="data">The <see cref="ResultBuffer"/> containing the extended data.</param>
        public static void SetXData(this ObjectId objectId, ResultBuffer data)
        {
	        // Start a transaction
	        using (var trans = DocumentManager.MdiActiveDocument.Database.TransactionManager.StartTransaction())
	        using (var obj   = trans.GetObject(objectId, OpenMode.ForWrite))
		        obj.XData = data;
        }

		/// <summary>
        /// Set extended data to this <paramref name="objectId"/>.
        /// </summary>
        /// <param name="objectId">The <see cref="ObjectId"/>.</param>
        /// <param name="data">The collection of <see cref="TypedValue"/> containing the extended data.</param>
        public static void SetXData(this ObjectId objectId, IEnumerable<TypedValue> data)
        {
	        using (var rb = new ResultBuffer(data.ToArray()))
		        objectId.SetXData(rb);
        }

		/// <summary>
		/// Set extended data to this <paramref name="dbObject"/>.
		/// </summary>
		/// <param name="dbObject">The <see cref="DBObject"/>.</param>
		/// <param name="data">The <see cref="ResultBuffer"/> containing the extended data.</param>
		public static void SetXData(this DBObject dbObject, ResultBuffer data) => dbObject.ObjectId.SetXData(data);

        /// <summary>
        /// Set extended data to this <paramref name="dbObject"/>.
        /// </summary>
        /// <param name="dbObject">The <see cref="DBObject"/>.</param>
        /// <param name="data">The collection of <see cref="TypedValue"/> containing the extended data.</param>
		public static void SetXData(this DBObject dbObject, IEnumerable<TypedValue> data) => dbObject.ObjectId.SetXData(data);

        /// <summary>
        /// Set extended data to this <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">The <see cref="Entity"/>.</param>
        /// <param name="data">The <see cref="ResultBuffer"/> containing the extended data.</param>
        public static void SetXData(this Entity entity, ResultBuffer data) => entity.ObjectId.SetXData(data);

        /// <summary>
        /// Set extended data to this <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">The <see cref="Entity"/>.</param>
        /// <param name="data">The collection of <see cref="TypedValue"/> containing the extended data.</param>
        public static void SetXData(this Entity entity, IEnumerable<TypedValue> data) => entity.ObjectId.SetXData(data);

        /// <summary>
        /// Erase all the objects in this collection.
        /// </summary>
        /// <param name="objects">The collection containing the <see cref="ObjectId"/>'s to erase.</param>
        public static void Erase(this IEnumerable<ObjectId> objects)
        {
	        if (objects is null || objects.ToArray().Length == 0)
		        return;

            // Start a transaction
            using (var trans = DocumentManager.MdiActiveDocument.Database.TransactionManager.StartTransaction())
	        {
                foreach (var obj in objects)
			        using (var ent = trans.GetObject(obj, OpenMode.ForWrite))
				        ent.Erase();

		        // Commit changes
		        trans.Commit();
	        }
        }

        /// <summary>
        /// Erase all the objects in this collection.
        /// </summary>
        /// <param name="objects">The collection containing the <see cref="DBObject"/>'s to erase.</param>
        public static void Erase(this IEnumerable<DBObject> objects) => objects.GetObjectIds().Erase();

        /// <summary>
        /// Erase all the objects in this <see cref="ObjectIdCollection"/>.
        /// </summary>
        /// <param name="objects">The <see cref="ObjectIdCollection"/> containing the objects to erase.</param>
        public static void Erase(this ObjectIdCollection objects) => objects.Cast<ObjectId>().Erase();

        /// <summary>
        /// Erase all the objects in this <see cref="DBObjectCollection"/>.
        /// </summary>
        /// <param name="objects">The <see cref="DBObjectCollection"/> containing the objects to erase.</param>
        public static void Erase(this DBObjectCollection objects) => objects.ToObjectIdCollection().Erase();
    }
}
