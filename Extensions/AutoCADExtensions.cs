using System;
using System.Collections;
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
	    /// Get current active <see cref="Autodesk.AutoCAD.ApplicationServices.Document"/>.
	    /// </summary>
	    private static Document Document => DocumentManager.MdiActiveDocument;

	    /// <summary>
	    /// Get current <see cref="Autodesk.AutoCAD.DatabaseServices.Database"/>.
	    /// </summary>
	    private static Database Database => Document.Database;

	    /// <summary>
	    /// Get the Block Table <see cref="ObjectId"/>.
	    /// </summary>
	    public static ObjectId BlockTableId => Database.BlockTableId;

	    /// <summary>
	    /// Get the Layer Table <see cref="ObjectId"/>.
	    /// </summary>
	    public static ObjectId LayerTableId => Database.LayerTableId;

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
			    return x > 0 ? 0 : Constants.Pi;

		    if (x.Abs() < tolerance)
			    return y > 0 ? Constants.PiOver2 : Constants.Pi3Over2;

		    return
			    (y / x).Atan();
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
		public static bool Approx(this Point3d point, Point3d otherPoint, double tolerance = 1E-3)
			=> point.X.Approx(otherPoint.X, tolerance) && point.Y.Approx(otherPoint.Y, tolerance) && point.Z.Approx(otherPoint.Z, tolerance);

		/// <summary>
        /// Return this collection of <see cref="Point3d"/>'s ordered in ascending Y then ascending X.
        /// </summary>
        public static IEnumerable<Point3d> Order(this IEnumerable<Point3d> points) => points.OrderBy(p => p.Y).ThenBy(p => p.X);

		/// <summary>
        /// Return this collection of <see cref="DBPoint"/>'s ordered in ascending Y then ascending X coordinates.
        /// </summary>
        public static IEnumerable<DBPoint> Order(this IEnumerable<DBPoint> points) => points.OrderBy(p => p.Position.Y).ThenBy(p => p.Position.X);

		/// <summary>
        /// Return this collection of <see cref="Line"/>'s ordered in ascending Y then ascending X midpoint coordinates.
        /// </summary>
        public static IEnumerable<Line> Order(this IEnumerable<Line> lines) => lines.OrderBy(l => l.MidPoint().Y).ThenBy(l => l.MidPoint().X);

		/// <summary>
        /// Return this collection of <see cref="Solid"/>'s ordered in ascending Y then ascending X center point coordinates.
        /// </summary>
        public static IEnumerable<Solid> Order(this IEnumerable<Solid> solids) => solids.OrderBy(s => s.CenterPoint().Y).ThenBy(s => s.CenterPoint().X);
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
	        if (objectId.IsNull || objectId.IsErased)
		        return null;

			// Start a transaction
			var trans = StartTransaction();

	        // Read the object as a point
	        return
		        trans.GetObject(objectId, OpenMode.ForRead);
		}

        /// <summary>
        /// Read a <see cref="Entity"/> in the drawing from this <see cref="ObjectId"/>.
        /// </summary>
        public static Entity ToEntity(this ObjectId objectId) => (Entity) objectId.ToDBObject();

		/// <summary>
        /// Get the <see cref="ObjectId"/> related to this <paramref name="handle"/>.
        /// </summary>
        public static ObjectId ToObjectId(this Handle handle) => Database.TryGetObjectId(handle, out var obj) ? obj : ObjectId.Null;

		/// <summary>
		/// Get the <see cref="Entity"/> related to this <paramref name="handle"/>.
		/// </summary>
		public static Entity ToEntity(this Handle handle) => handle.ToObjectId().ToEntity();

        /// <summary>
        /// Return a <see cref="DBObjectCollection"/> from an <see cref="ObjectIdCollection"/>.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static DBObjectCollection ToDBObjectCollection(this ObjectIdCollection collection)
        {
	        if (collection is null)
		        return null;

			var dbCollection = new DBObjectCollection();

	        // Start a transaction
			if (collection.Count > 0)
		        using (var trans = StartTransaction())
			        foreach (ObjectId objectId in collection)
						if (!objectId.IsNull && !objectId.IsErased)
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
	        if (collection is null)
		        return null;

	        return
		        collection.Count > 0 ? new ObjectIdCollection((from DBObject obj in collection select obj.ObjectId).ToArray()) : new ObjectIdCollection();
        }

		/// <summary>
        /// Get the collection of <see cref="ObjectId"/>'s of <paramref name="objects"/>.
        /// </summary>
        public static IEnumerable<ObjectId> GetObjectIds(this IEnumerable<DBObject> objects) => objects?.Select(obj => obj.ObjectId);

        /// <summary>
        /// Get the collection of <see cref="DBObject"/>'s of <paramref name="objectIds"/>.
        /// </summary>
        public static IEnumerable<DBObject> GetDBObjects(this IEnumerable<ObjectId> objectIds)
		{
			if (objectIds is null)
				return null;

            // Start a transaction
            using (var trans = StartTransaction())
	            return objectIds.Select(obj => trans.GetObject(obj, OpenMode.ForRead)).ToArray();
		}

        /// <summary>
        /// Get this collection as <see cref="DBPoint"/>'s.
        /// </summary>
        public static IEnumerable<DBPoint> ToPoints(this IEnumerable<DBObject> objects) => objects?.Cast<DBPoint>();

        /// <summary>
        /// Get this collection as <see cref="Line"/>'s.
        /// </summary>
        public static IEnumerable<Line> ToLines(this IEnumerable<DBObject> objects) => objects?.Cast<Line>();

        /// <summary>
        /// Get this collection as <see cref="Solid"/>'s.
        /// </summary>
        public static IEnumerable<Solid> ToSolids(this IEnumerable<DBObject> objects) => objects?.Cast<Solid>();

        /// <summary>
        /// Get this collection as <see cref="BlockReference"/>'s.
        /// </summary>
        public static IEnumerable<BlockReference> ToBlocks(this IEnumerable<DBObject> objects) => objects?.Cast<BlockReference>();

        /// <summary>
        /// Get this collection as <see cref="DBText"/>'s.
        /// </summary>
        public static IEnumerable<DBText> ToTexts(this IEnumerable<DBObject> objects) => objects?.Cast<DBText>();

        /// <summary>
        /// Get the <see cref="Point3d"/> vertices of this <paramref name="solid"/>.
        /// </summary>
        public static IEnumerable<Point3d> GetVertices(this Solid solid)
        {
	        var points = new Point3dCollection();
	        solid.GetGripPoints(points, new IntegerCollection(), new IntegerCollection());
	        return points.Cast<Point3d>();
        }

        /// <summary>
        /// Get the mid <see cref="Point3d"/> of a <paramref name="line"/>.
        /// </summary>
        public static Point3d MidPoint(this Line line) => line.StartPoint.MidPoint(line.EndPoint);

        /// <summary>
        /// Get the approximated center <see cref="Point3d"/> of a rectangular <paramref name="solid"/>.
        /// </summary>
        public static Point3d CenterPoint(this Solid solid)
        {
	        var verts = solid.GetVertices().ToArray();

	        if (verts.Length != 4)
		        throw new NotImplementedException();

	        var mid1 = verts[0].MidPoint(verts[3]);
	        var mid2 = verts[1].MidPoint(verts[2]);

	        return mid1.MidPoint(mid2);
        }

        /// <summary>
        /// Read this <see cref="DBObject"/>'s XData as an <see cref="Array"/> of <see cref="TypedValue"/>.
        /// </summary>
        /// <param name="appName">The application name.</param>
        public static TypedValue[] ReadXData(this DBObject dbObject, string appName) => dbObject?.GetXDataForApplication(appName)?.AsArray();

        /// <summary>
        /// Read this <see cref="Entity"/>'s XData as an <see cref="Array"/> of <see cref="TypedValue"/>.
        /// </summary>
        /// <param name="appName">The application name.</param>
        public static TypedValue[] ReadXData(this Entity entity, string appName) => entity?.GetXDataForApplication(appName)?.AsArray();

        /// <summary>
        /// Read this <see cref="ObjectId"/>'s XData as an <see cref="Array"/> of <see cref="TypedValue"/>.
        /// </summary>
        /// <param name="appName">The application name.</param>
        public static TypedValue[] ReadXData(this ObjectId objectId, string appName)
        {
	        if (objectId.IsNull || objectId.IsErased)
		        return null;

	        // Start a transaction
	        using (var trans = StartTransaction())
		        return trans.GetObject(objectId, OpenMode.ForRead).ReadXData(appName);
        }

		/// <summary>
        /// Set extended data to this <paramref name="objectId"/>.
        /// </summary>
        /// <param name="objectId">The <see cref="ObjectId"/>.</param>
        /// <param name="data">The <see cref="ResultBuffer"/> containing the extended data.</param>
        public static void SetXData(this ObjectId objectId, ResultBuffer data)
        {
	        if (objectId.IsNull || objectId.IsErased)
		        return;

            // Start a transaction
            using (var trans = StartTransaction())
	        using (var ent   = (Entity) trans.GetObject(objectId, OpenMode.ForWrite))
	        {
				if (ent != null)
					ent.XData = data;
				trans.Commit();
	        }
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
        /// Clean extended data attached to this <paramref name="objectId"/>.
        /// </summary>
        public static void CleanXData(this ObjectId objectId) => objectId.SetXData((ResultBuffer) null);

		/// <summary>
        /// Clean extended data attached to this <paramref name="dbObject"/>.
        /// </summary>
        public static void CleanXData(this DBObject dbObject) => dbObject.SetXData((ResultBuffer) null);

		/// <summary>
        /// Clean extended data attached to this <paramref name="entity"/>.
        /// </summary>
        public static void CleanXData(this Entity entity) => entity.SetXData((ResultBuffer) null);

        /// <summary>
        /// Add this <paramref name="dbObject"/> to the drawing.
        /// </summary>
        /// <param name="dbObject">The <see cref="DBObject"/>.</param>
        /// <param name="erasedEvent">The event to call if <paramref name="dbObject"/> is erased.</param>
        public static void Add(this DBObject dbObject, ObjectErasedEventHandler erasedEvent = null) => ((Entity)dbObject).Add(erasedEvent);

        /// <summary>
        /// Add this <paramref name="entity"/> to the drawing.
        /// </summary>
        /// <param name="entity">The <see cref="Entity"/>.</param>
        /// <param name="erasedEvent">The event to call if <paramref name="entity"/> is erased.</param>
        public static void Add(this Entity entity, ObjectErasedEventHandler erasedEvent = null)
        {
	        if (entity is null)
		        return;


            // Start a transaction
            using (var trans = StartTransaction())

		        // Open the Block table for read
	        using (var blkTbl = (BlockTable) trans.GetObject(BlockTableId, OpenMode.ForRead))

		        // Open the Block table record Model space for write
	        using (var blkTblRec = (BlockTableRecord)trans.GetObject(blkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite))
	        {
		        // Add the object to the drawing
		        blkTblRec.AppendEntity(entity);
		        trans.AddNewlyCreatedDBObject(entity, true);

				if (erasedEvent != null)
					entity.Erased += erasedEvent;

		        // Commit changes
		        trans.Commit();
	        }
        }

        /// <summary>
        /// Add the <paramref name="objects"/> in this collection to the drawing.
        /// </summary>
        /// <param name="erasedEvent">The event to call if <paramref name="objects"/> are erased.</param>
        public static void Add(this IEnumerable<DBObject> objects, ObjectErasedEventHandler erasedEvent = null) => objects?.Cast<Entity>().Add(erasedEvent);

        /// <summary>
        /// Add the <paramref name="entities"/> in this collection to the drawing.
        /// </summary>
        /// <param name="erasedEvent">The event to call if <paramref name="entities"/> are erased.</param>
        public static void Add(this IEnumerable<Entity> entities, ObjectErasedEventHandler erasedEvent = null)
        {
	        if (entities is null || !entities.Any())
		        return;

	        // Start a transaction
	        using (var trans = StartTransaction())

		        // Open the Block table for read
	        using (var blkTbl = (BlockTable)trans.GetObject(BlockTableId, OpenMode.ForRead))

		        // Open the Block table record Model space for write
	        using (var blkTblRec = (BlockTableRecord)trans.GetObject(blkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite))
	        {
		        // Add the objects to the drawing
		        foreach (var ent in entities)
		        {
			        blkTblRec.AppendEntity(ent);
			        trans.AddNewlyCreatedDBObject(ent, true);

			        if (erasedEvent != null)
				        ent.Erased += erasedEvent;
		        }

                // Commit changes
                trans.Commit();
	        }
        }

        /// <summary>
        /// Register a <see cref="ObjectErasedEventHandler"/> to this <paramref name="objectId"/>
        /// </summary>
        /// <param name="handler"> The <see cref="ObjectErasedEventHandler"/> to add.</param>
        public static void RegisterErasedEvent(this ObjectId objectId, ObjectErasedEventHandler handler)
        {
			if (handler is null || objectId.IsNull || objectId.IsErased)
				return;

			// Start a transaction
			using (var trans = StartTransaction())
			{
				using (var ent = (Entity) trans.GetObject(objectId, OpenMode.ForWrite))
					ent.Erased += handler;

				// Commit changes
				trans.Commit();
			}
        }

        /// <summary>
        /// Register a <see cref="ObjectErasedEventHandler"/> to these <paramref name="objectIds"/>
        /// </summary>
        /// <param name="handler"> The <see cref="ObjectErasedEventHandler"/> to add.</param>
        public static void RegisterErasedEvent(this IEnumerable<ObjectId> objectIds, ObjectErasedEventHandler handler)
        {
			if (handler is null || objectIds is null || !objectIds.Any())
				return;

			// Start a transaction
			using (var trans = StartTransaction())
			{
				foreach (var obj in objectIds)
					if (!obj.IsNull || !obj.IsErased)
						using (var ent = (Entity) trans.GetObject(obj, OpenMode.ForWrite))
							ent.Erased += handler;

				// Commit changes
				trans.Commit();
			}
        }

        /// <summary>
        /// Unregister a <see cref="ObjectErasedEventHandler"/> from this <paramref name="objectId"/>
        /// </summary>
        /// <param name="handler"> The <see cref="ObjectErasedEventHandler"/> to remove.</param>
        public static void UnregisterErasedEvent(this ObjectId objectId, ObjectErasedEventHandler handler)
        {
	        if (handler is null || objectId.IsNull || objectId.IsErased)
		        return;

            // Start a transaction
            using (var trans = StartTransaction())
			{
				using (var ent = (Entity) trans.GetObject(objectId, OpenMode.ForWrite))
					ent.Erased -= handler;

				// Commit changes
				trans.Commit();
			}
        }

        /// <summary>
        /// Unregister a <see cref="ObjectErasedEventHandler"/> from these <paramref name="objectIds"/>
        /// </summary>
        /// <param name="handler"> The <see cref="ObjectErasedEventHandler"/> to add.</param>
        public static void UnregisterErasedEvent(this IEnumerable<ObjectId> objectIds, ObjectErasedEventHandler handler)
        {
	        if (handler is null || objectIds is null || !objectIds.Any())
		        return;

	        // Start a transaction
	        using (var trans = StartTransaction())
	        {
		        foreach (var obj in objectIds)
			        if (!obj.IsNull || !obj.IsErased)
				        using (var ent = (Entity)trans.GetObject(obj, OpenMode.ForWrite))
					        ent.Erased -= handler;

		        // Commit changes
		        trans.Commit();
	        }
        }

        /// <summary>
        /// Remove this object from drawing.
        /// </summary>
        public static void Remove(this ObjectId obj)
        {
	        if (obj.IsNull || obj.IsErased)
		        return;

            // Start a transaction
            using (var trans = StartTransaction())
            {
	            using (var ent = trans.GetObject(obj, OpenMode.ForWrite))
		            ent.Erase();

	            // Commit changes
	            trans.Commit();
            }
        }

        /// <summary>
        /// Remove this object from drawing.
        /// </summary>
        public static void Remove(this DBObject obj) => obj?.ObjectId.Remove();

        /// <summary>
        /// Remove this <paramref name="entity"/> from drawing.
        /// </summary>
        public static void Remove(this Entity entity) => entity?.ObjectId.Remove();

        /// <summary>
        /// Remove all the objects in this collection from drawing.
        /// </summary>
        /// <param name="objects">The collection containing the <see cref="ObjectId"/>'s to erase.</param>
        public static void Remove(this IEnumerable<ObjectId> objects)
        {
	        if (objects is null || !objects.Any())
		        return;

            // Start a transaction
            using (var trans = StartTransaction())
	        {
                foreach (var obj in objects)
	                if (!obj.IsNull && !obj.IsErased)
		                using (var ent = trans.GetObject(obj, OpenMode.ForWrite))
			                ent.Erase();

                // Commit changes
                trans.Commit();
	        }
        }

        /// <summary>
        /// Remove all the objects in this collection from drawing.
        /// </summary>
        /// <param name="objects">The collection containing the <see cref="DBObject"/>'s to erase.</param>
        public static void Remove(this IEnumerable<DBObject> objects) => objects?.GetObjectIds()?.Remove();

        /// <summary>
        /// Remove all the objects in this collection from drawing.
        /// </summary>
        /// <param name="objects">The <see cref="ObjectIdCollection"/> containing the objects to erase.</param>
        public static void Remove(this ObjectIdCollection objects) => objects?.Cast<ObjectId>().Remove();

        /// <summary>
        /// Erase all the objects in this <see cref="DBObjectCollection"/>.
        /// </summary>
        /// <param name="objects">The <see cref="DBObjectCollection"/> containing the objects to erase.</param>
        public static void Remove(this DBObjectCollection objects) => objects?.ToObjectIdCollection()?.Remove();

		/// <summary>
        /// Move the objects in this collection to drawing bottom.
        /// </summary>
        public static void MoveToBottom(this IEnumerable<ObjectId> objectIds)
        {
	        if (objectIds is null || !objectIds.Any())
		        return;

            using (var trans = StartTransaction())
	        {
		        var blkTbl = (BlockTable)trans.GetObject(BlockTableId, OpenMode.ForRead);

		        var blkTblRec = (BlockTableRecord) trans.GetObject(blkTbl[BlockTableRecord.ModelSpace], OpenMode.ForRead);

                var drawOrder = (DrawOrderTable)trans.GetObject(blkTblRec.DrawOrderTableId, OpenMode.ForWrite);

                // Move the panels to bottom
                using (var objs = new ObjectIdCollection(objectIds.ToArray()))
	                drawOrder.MoveToBottom(objs);

				trans.Commit();
	        }
        }

        /// <summary>
        /// Move the objects in this collection to drawing bottom.
        /// </summary>
        public static void MoveToBottom(this IEnumerable<DBObject> objects) => objects?.GetObjectIds()?.MoveToBottom();

        /// <summary>
        /// Move the objects in this collection to drawing top.
        /// </summary>
        public static void MoveToTop(this IEnumerable<ObjectId> objectIds)
        {
			if (objectIds is null || !objectIds.Any())
				return;

	        using (var trans = StartTransaction())
	        {
		        var blkTbl = (BlockTable)trans.GetObject(BlockTableId, OpenMode.ForRead);

		        var blkTblRec = (BlockTableRecord)trans.GetObject(blkTbl[BlockTableRecord.ModelSpace], OpenMode.ForRead);

		        var drawOrder = (DrawOrderTable)trans.GetObject(blkTblRec.DrawOrderTableId, OpenMode.ForWrite);

                // Move the panels to bottom
                using (var objs = new ObjectIdCollection(objectIds.ToArray()))
	                drawOrder.MoveToTop(objs);

                trans.Commit();
	        }
        }

        /// <summary>
        /// Move the objects in this collection to drawing top.
        /// </summary>
        public static void MoveToTop(this IEnumerable<DBObject> objects) => objects?.GetObjectIds()?.MoveToTop();

		/// <summary>
        /// Start a new transaction.
        /// </summary>
        private static Transaction StartTransaction() => Database.TransactionManager.StartTransaction();
    }
}
