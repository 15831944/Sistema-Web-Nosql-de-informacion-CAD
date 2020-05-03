using System;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using Teigha.Runtime;
using Teigha.DatabaseServices;
using Teigha.Geometry;
using Bricscad.ApplicationServices;
using Bricscad.Runtime;
using Bricscad.EditorInput;
using BricscadDb;
using BricscadApp;
using _AcAp = Bricscad.ApplicationServices;
using _AcDb = Teigha.DatabaseServices;
using _AcGe = Teigha.Geometry;
using _AcEd = Bricscad.EditorInput;
using Services;
using Model;

namespace CsBrxMgd
{
    public class BricsCadEntityInjector
    {
        LoggingService _LoggingService;
        ActionWrapper currentActionWrapper;

        public BricsCadEntityInjector(LoggingService myLoggingService)
        {
            _LoggingService = myLoggingService;
        }

        public ActionWrapper Dispatcher(ActionWrapper myActionWrapper)
        {
            currentActionWrapper = myActionWrapper;

            switch (myActionWrapper.Type)
            {
                case ActionWrapper.TypeEnum.AddCircle:
                    addCircle();
                    break;
                case ActionWrapper.TypeEnum.AddLayer:
                    addLayer();
                    break;
                case ActionWrapper.TypeEnum.AddLine:
                    addLine();
                    break;
                case ActionWrapper.TypeEnum.AddPolyLine:
                    addPolyLine();
                    break;
                case ActionWrapper.TypeEnum.CreateTable:
                    createTable();
                    break;
                case ActionWrapper.TypeEnum.PerfomranceTest:
                    performanceTest();
                    break;
                case ActionWrapper.TypeEnum.ReadDwgFile:
                    readDwgFile(myActionWrapper.Parameter);
                    break;
                default:
                    myActionWrapper.Status = ActionWrapper.StatusEnum.DataError;
                    break;

            }

            return currentActionWrapper;
        }

        private void addLine()
        {
            Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                Point3d startPoint = new Point3d(0, 0, 0);
                Point3d endPoint = new Point3d(100, 100, 0);
                Line line = new Line(startPoint, endPoint);
                AddToModelSpace(line);
            }
            catch (System.Exception ex)
            {
                _LoggingService.WriteWithInner(ex, true, string.Format("\nError: {0}\nStackTrace: {1}", ex.Message, ex.StackTrace));
            }
        }

        private void addPolyLine()
        {
            Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                Polyline pline1 = new Polyline(4);
                pline1.AddVertexAt(0, new Point2d(0.0, 0.0), 0.1, 0, 0);
                pline1.AddVertexAt(1, new Point2d(0.0, 100.0), 0.1, 0, 0);
                pline1.AddVertexAt(2, new Point2d(100.0, 100.0), 0.1, 0, 0);
                pline1.AddVertexAt(3, new Point2d(100.0, 0.0), 0.1, 0, 0);
                pline1.Closed = true;

                Polyline pline2 = pline1.Clone() as Polyline;
                pline2.TransformBy(Matrix3d.Rotation(0.261799388, Vector3d.ZAxis, new Point3d(50, 50, 0)));

                Polyline pline3 = pline1.Clone() as Polyline;
                pline3.TransformBy(Matrix3d.Rotation(0.523598776, Vector3d.ZAxis, new Point3d(50, 50, 0)));

                AddToModelSpace(pline1, pline2, pline3);
            }
            catch (System.Exception ex)
            {
                _LoggingService.WriteWithInner(ex, true, string.Format("\nError: {0}\nStackTrace: {1}", ex.Message, ex.StackTrace));
            }
        }

        private void addLayer()
        {
            Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                LayerTableRecord myLayer = new LayerTableRecord();
                myLayer.Name = "CoolLayer";
                myLayer.Color = Teigha.Colors.Color.FromRgb(255, 0, 255);

                Database database = HostApplicationServices.WorkingDatabase;
                _AcDb.TransactionManager manager = database.TransactionManager;

                using (Transaction action = manager.StartTransaction())
                {
                    LayerTable layerTable =
                        action.GetObject(database.LayerTableId, OpenMode.ForWrite) as LayerTable;

                    if (layerTable == null)
                        throw new System.NullReferenceException("LayerTable == null");

                    if (!layerTable.Has(myLayer.Name))
                        layerTable.Add(myLayer);

                    action.AddNewlyCreatedDBObject(myLayer, true);
                    action.Commit();
                }
            }
            catch (System.Exception ex)
            {
                _LoggingService.WriteWithInner(ex, true, string.Format("\nError: {0}\nStackTrace: {1}", ex.Message, ex.StackTrace));
            }
        }

        private void addCircle()
        {
            Editor editor = _AcAp.Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                Circle circle = new Circle(new Point3d(0, 0, 0), Vector3d.ZAxis, 3);

                Ellipse ellipse = new Ellipse(new Point3d(0, 0, 0),
                    Vector3d.ZAxis, Vector3d.YAxis, 0.6, 0.0, 2 * Math.PI);

                AddToModelSpace(circle, ellipse);
            }
            catch (System.Exception ex)
            {
                _LoggingService.WriteWithInner(ex, true, string.Format("\nError: {0}\nStackTrace: {1}", ex.Message, ex.StackTrace));
            }
        }

        private void performanceTest()
        {
            Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                Database database = HostApplicationServices.WorkingDatabase;
                if (database == null)
                    throw new NullReferenceException("database is null");

                _AcDb.TransactionManager manager = database.TransactionManager;

                if (manager == null)
                    throw new NullReferenceException("manager is null");

                using (Transaction action = manager.StartTransaction())
                {
                    int countEnts = 0;
                    Stopwatch sw = Stopwatch.StartNew();
                    BlockTableRecord blockTableRecord = action.GetObject
                      (database.CurrentSpaceId, OpenMode.ForRead, false) as BlockTableRecord;

                    if (blockTableRecord == null)
                        throw new NullReferenceException("blockTableRecord is null");

                    foreach (ObjectId entityId in blockTableRecord)
                    {
                        Entity entity = action.GetObject(entityId, OpenMode.ForRead, false) as Entity;
                        if (entity == null)
                            throw new NullReferenceException("entity is null");

                        countEnts++;
                    }
                    editor.WriteMessage("\n{0} Entities found in {1} Milliseconds", countEnts, sw.ElapsedMilliseconds);
                    action.Commit();
                }
            }
            catch (System.Exception ex)
            {
                _LoggingService.WriteWithInner(ex, true, string.Format("\nError: {0}\nStackTrace: {1}", ex.Message, ex.StackTrace));
            }
        }

        private void readDwgFile(string path)
        {
            Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                using (Database db = new Database(false, true))
                {
                    db.ReadDwgFile(path, FileShare.Read, true, null);
                    editor.WriteMessage("\nApproxNumObjects = {0}", db.ApproxNumObjects);
                }
            }
            catch (System.Exception ex)
            {
                _LoggingService.WriteWithInner(ex, true, string.Format("\nError: {0}\nStackTrace: {1}", ex.Message, ex.StackTrace));
            }
        }

        private void createTable()
        {
            Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                Database database = HostApplicationServices.WorkingDatabase;

                Table table = new Table();
                table.SetDatabaseDefaults(database);

                table.Position = Point3d.Origin;

                table.NumRows = 6;
                table.NumColumns = 3;
                table.GenerateLayout();
                table.RecomputeTableBlock(true);

                for (int row = 0; row < table.Rows.Count; row++)
                {
                    for (int col = 0; col < table.Columns.Count; col++)
                    {
                        table.SetTextString(row, col, "Bricscad");
                    }
                }

                table.RecomputeTableBlock(true);
                AddToModelSpace(table);
            }
            catch (System.Exception ex)
            {
                _LoggingService.WriteWithInner(ex, true, string.Format("\nError: {0}\nStackTrace: {1}", ex.Message, ex.StackTrace));
            }
        }

        private ObjectIdCollection AddToModelSpace(params Entity[] list)
        {
            ObjectIdCollection ids = new ObjectIdCollection();
            Database database = HostApplicationServices.WorkingDatabase;
            _AcDb.TransactionManager manager = database.TransactionManager;
            using (Transaction action = manager.StartTransaction())
            {
                BlockTable blockTable =
                    action.GetObject(database.BlockTableId, OpenMode.ForRead) as BlockTable;
                if (blockTable == null)
                    throw new NullReferenceException("blockTable == null");

                BlockTableRecord blockTableRecord =
                    action.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                if (blockTableRecord == null)
                    throw new System.NullReferenceException("blockTableRecord == null");

                foreach (Entity ent in list)
                {
                    ids.Add(blockTableRecord.AppendEntity(ent));
                    action.AddNewlyCreatedDBObject(ent, true);
                }
                action.Commit();
            }
            return ids;
        }
    }
}