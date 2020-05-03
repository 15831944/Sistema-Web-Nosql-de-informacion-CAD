using Bricscad.EditorInput;
using Teigha.DatabaseServices;
using Teigha.Geometry;
using Bricscad.ApplicationServices;
using BricscadApp;
using System.IO;
using System.Reflection;
using System.Threading;

public class Actions
{
    //Public Class BricsCADHelper

    //#Region "Events"

    //    Public Shared Sub AddSelectionAddedHandler()
    //        AddHandler GlobalProperties.CurrentDocument.Editor.SelectionAdded, New SelectionAddedEventHandler(AddressOf BricsCADHelper.OnSelectionAdded)
    //    End Sub

    //    Public Shared Sub OnSelectionAdded(ByVal sender As Object, ByVal eventArgs As SelectionAddedEventArgs)

    //        Dim blockController = New BlockController()
    //        Dim planeController = New PlaneController()
    //        Dim objBlock = New Block()

    //        Dim destinationPlane As Plane
    //        Dim sourcePlane As Plane
    //        Dim sourceEntity As Entity
    //        Dim destinationEntity As Entity = Nothing
    //        Dim sourceText As String
    //        Dim destinationText As String

    //        GlobalProperties.CurrentEditor.WriteMessage("CADExplorer entity selected")

    //        Try
    //            Using tr As Transaction = GlobalProperties.CurrentDatabase.TransactionManager.StartTransaction

    //                sourceEntity = CType(tr.GetObject(eventArgs.AddedObjects.GetObjectIds()(0), OpenMode.ForRead), Entity)
    //                sourceText = GetEntityText(sourceEntity)
    //                sourcePlane = planeController.GetPlaneByNameAndRevision(Path.GetFileNameWithoutExtension(GlobalProperties.CurrentDocument.Name))

    //                If (Not sourceEntity Is Nothing AndAlso blockController.IsNavigational(sourceText)) Then

    //                    destinationPlane = planeController.GetPlaneByShortName(sourceText)

    //                    If (Not destinationPlane Is Nothing) Then
    //                        OpenPlane(destinationPlane)

    //                        If (Not sourcePlane Is Nothing) Then

    //                            destinationText = planeController.GetShortNameByPlaneName(sourcePlane.Name)
    //                            GetEntityByText(destinationText, destinationEntity, tr)

    //                            If (Not destinationEntity Is Nothing) Then
    //                                ZoomWin(destinationEntity)
    //                            End If
    //                        End If
    //                    End If
    //                End If

    //                If (Not sourceEntity Is Nothing AndAlso Not sourcePlane Is Nothing) Then

    //                    objBlock = blockController.GetBlockByHandle(sourceEntity.Handle.Value, sourcePlane.IdPlane)
    //                    If (Not objBlock Is Nothing) Then
    //                        If (Not objBlock.LogicDiagram Is Nothing) Then
    //                            JpgHelper.OpenJpg(objBlock.LogicDiagram)
    //                        End If

    //                        If (Not objBlock.HtmlTable Is Nothing) Then
    //                            Dim frmImage As frmHtmlTable = New frmHtmlTable() With {.objBlock = objBlock}
    //                            Application.ShowModelessDialog(frmImage)
    //                        End If

    //                        UISizeHelper.ActivateBricsCad()
    //                    End If
    //                End If

    //                tr.Commit()
    //            End Using
    //        Catch ex As Exception
    //            LoggingHelper.Write("OnSelectionAdded")
    //            LoggingHelper.WriteEx(ex)
    //        End Try
    //    End Sub

    //    Public Shared Function sortByIds(ByVal a As FullSubentityPath, ByVal b As FullSubentityPath) As Integer
    //        Return a.SubentId.Index.CompareTo(b.SubentId.Index)
    //    End Function

    //#End Region

    //#Region "Methods"

    //    Public Function GetEntityWithColorChange(ByVal lstIds As List(Of Integer)) As Entity

    //        Dim obj As ObjectId = Nothing
    //        Dim dbObj As Entity = Nothing

    //        Using transaction As Transaction = GlobalProperties.CurrentDocument.TransactionManager.StartTransaction
    //            Try
    //                For Each Id As Integer In lstIds

    //                    If (GlobalProperties.CurrentDatabase.TryGetObjectId(New Handle(Id), obj)) Then

    //                        dbObj = CType(transaction.GetObject(obj, OpenMode.ForWrite), Entity)
    //                        Dim objAuxEntity = CType(dbObj, Entity)
    //                        objAuxEntity.Color = Teigha.Colors.Color.FromRgb(255, 0, 0)

    //                        If (TypeOf objAuxEntity Is BlockReference) Then
    //                            IterativeColorChange(transaction, CType(objAuxEntity, BlockReference))
    //                        End If

    //                    End If
    //                Next
    //            Catch ex As Exception
    //                LoggingHelper.WriteEx(ex)
    //            End Try
    //            transaction.Commit()
    //        End Using

    //        Return dbObj
    //    End Function

    //    Private Sub IterativeColorChange(ByVal tr As Transaction, ByVal br As BlockReference)

    //        Dim btr As BlockTableRecord = CType(tr.GetObject(br.BlockTableRecord, OpenMode.ForRead), BlockTableRecord)

    //        For Each obj As ObjectId In btr
    //            Dim ent2 As Entity = CType(tr.GetObject(obj, OpenMode.ForWrite), Entity)
    //            ent2.Color = Teigha.Colors.Color.FromRgb(255, 0, 0)
    //        Next

    //    End Sub

    //    Public Shared Sub ZoomWin(ByVal currentEntity As Entity)

    //        Try
    //            Dim ext As Extents3d = currentEntity.GeometricExtents
    //            ext.TransformBy(GlobalProperties.CurrentEditor.CurrentUserCoordinateSystem.Inverse())
    //            ZoomWin(ext.MinPoint, ext.MaxPoint)
    //        Catch ex As Exception
    //            LoggingHelper.Write("ZoomWin")
    //            LoggingHelper.WriteEx(ex)
    //        End Try

    //    End Sub

    //    Private Shared Sub ZoomWin(ByVal min As Point3d, ByVal max As Point3d)

    //        Dim min2d As Point2d = New Point2d(min.X, min.Y)
    //        Dim max2d As Point2d = New Point2d(max.X, max.Y)
    //        Dim ZoomOffset = GlobalProperties.ZoomOffset

    //        Dim view As ViewTableRecord = New ViewTableRecord() _
    //                                      With {.CenterPoint = min2d + ((max2d - min2d) / 2.0), .Height = max2d.Y - min2d.Y + ZoomOffset, .Width = max2d.X - min2d.X + ZoomOffset}
    //        GlobalProperties.CurrentEditor.SetCurrentView(view)

    //    End Sub

    //    Public Shared Function OpenPlane(ByVal objPlane As Plane) As Boolean

    //        Dim app As AcadApplication = CType(Bricscad.ApplicationServices.Application.AcadApplication, AcadApplication)
    //        Dim document As AcadDocument = Nothing

    //        Try

    //            For Each objDoc As AcadDocument In app.Documents
    //                If (objDoc.Name = objPlane.Name + objPlane.Extension) Then
    //                    document = objDoc
    //                End If
    //            Next

    //            If (Not document Is Nothing) Then
    //                document.Activate()
    //            Else
    //                Dim filePath = DownloadPlane(objPlane)
    //                LoggingHelper.Write(filePath + ": Opening")              
    //                app.Documents.Open(filePath, False, Nothing)
    //                Thread.Sleep(500)
    //                LoggingHelper.Write(filePath + ": Opened")
    //                AddSelectionAddedHandler()
    //                LoggingHelper.Write(filePath + ": HandlerAdded")
    //            End If

    //            Return True
    //        Catch ex As Exception
    //            LoggingHelper.Write("OpenPlane")
    //            LoggingHelper.WriteEx(ex)
    //            Return False
    //        End Try

    //    End Function

    //    Private Shared Function DownloadPlane(ByVal objPlane As Plane) As String

    //        Dim objPlaneRevision As PlaneRevision
    //        Dim objPlaneRevisionController As PlaneRevisionController = New PlaneRevisionController()

    //        If (Not objPlane Is Nothing) Then

    //            objPlaneRevision = objPlaneRevisionController.GetPlaneRevision(objPlane.IdPlane, GlobalProperties.CurrentRevision.IdRevision)
    //            If (Not objPlaneRevision Is Nothing AndAlso Not objPlaneRevision.File Is Nothing) Then
    //                Return IOHelper.DownloadObject(objPlane.Name, objPlane.Extension, objPlaneRevision.File)
    //            End If
    //        End If

    //        Return String.Empty
    //    End Function

    //    Public Shared Function GetDownloadedObjectPath(ByVal Name As String, ByVal Extension As String) As String
    //        Return String.Format("{0}\{1}{2}", Directory.GetParent(Assembly.GetExecutingAssembly().Location), Name, Extension)
    //    End Function

    //    Private Shared Function GetEntityText(ByVal currentEntity As Entity) As String

    //        Using transaction As Transaction = GlobalProperties.CurrentDocument.TransactionManager.StartTransaction

    //            If (currentEntity Is Nothing) Then
    //                Return String.Empty
    //            ElseIf (TypeOf currentEntity Is MText) Then
    //                Return DirectCast(currentEntity, MText).Text
    //            ElseIf (TypeOf currentEntity Is DBText) Then
    //                Return DirectCast(currentEntity, DBText).TextString
    //            End If

    //            transaction.Commit()
    //        End Using

    //        Return String.Empty
    //    End Function

    //    Private Shared Sub GetEntityByText(ByVal text As String, ByRef currentEntity As Entity, ByRef tr As Transaction)
    //        Try

    //            Dim btr As BlockTableRecord = DirectCast(tr.GetObject( _
    //                    SymbolUtilityServices.GetBlockModelSpaceId(GlobalProperties.CurrentDatabase), OpenMode.ForRead), BlockTableRecord)

    //            For Each id As ObjectId In btr

    //                currentEntity = DirectCast(tr.GetObject(id, OpenMode.ForRead, False, True), Entity)

    //                If (currentEntity Is Nothing) Then
    //                    Continue For
    //                ElseIf (TypeOf currentEntity Is MText AndAlso DirectCast(currentEntity, MText).Text = text) Then
    //                    Return
    //                ElseIf (TypeOf currentEntity Is DBText AndAlso DirectCast(currentEntity, DBText).TextString = text) Then
    //                    Return
    //                End If
    //            Next

    //        Catch ex As Exception
    //            LoggingHelper.Write("GetEntityByText")
    //            LoggingHelper.WriteEx(ex)
    //        End Try

    //        currentEntity = Nothing
    //    End Sub

    //    Public Shared Function GetEntitiesByHandles(ByVal lstHandles As List(Of Integer)) As List(Of Entity)

    //        Dim lstReturn As List(Of Entity) = New List(Of Entity)
    //        Dim lstObjectId As List(Of ObjectId)

    //        Try

    //            Using transaction As Transaction = GlobalProperties.CurrentDocument.TransactionManager.StartTransaction

    //                lstObjectId = GetObjectIdsByHandles(lstHandles)
    //                For Each objId As ObjectId In lstObjectId
    //                    Dim objDBObject As DBObject = transaction.GetObject(objId, OpenMode.ForRead, False, True)

    //                    If (Not objDBObject Is Nothing AndAlso TypeOf objDBObject Is Entity) Then
    //                        lstReturn.Add(DirectCast(objDBObject, Entity))
    //                    End If
    //                Next

    //                transaction.Commit()
    //            End Using

    //        Catch ex As Exception
    //            LoggingHelper.WriteEx(ex)
    //        End Try

    //        Return lstReturn
    //    End Function

    //    Public Shared Function GetObjectIdsByHandlesTransactional(ByVal lstHandles As List(Of Integer)) As List(Of ObjectId)

    //        Using transaction As Transaction = GlobalProperties.CurrentDocument.TransactionManager.StartTransaction
    //            Return GetObjectIdsByHandles(lstHandles)
    //        End Using

    //    End Function

    //    Public Shared Function GetObjectIdsByHandles(ByVal lstHandles As List(Of Integer)) As List(Of ObjectId)

    //        Dim lstReturn As List(Of ObjectId) = New List(Of ObjectId)
    //        Try

    //            For Each hndl As Integer In lstHandles
    //                Dim objId As ObjectId
    //                If (GlobalProperties.CurrentDatabase.TryGetObjectId(New Handle(hndl), objId)) Then
    //                    lstReturn.Add(objId)
    //                End If
    //            Next

    //        Catch ex As Exception
    //            LoggingHelper.WriteEx(ex)
    //        End Try

    //        Return lstReturn
    //    End Function

    //#End Region

    //End Class
}