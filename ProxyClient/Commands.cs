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
using Bricscad.Ribbon;
using Bricscad.Geometrical3dConstraints;
using BricscadDb;
using BricscadApp;
using _AcRx = Teigha.Runtime;
using _AcAp = Bricscad.ApplicationServices;
using _AcDb = Teigha.DatabaseServices;
using _AcGe = Teigha.Geometry;
using _AcEd = Bricscad.EditorInput;
using _AcGi = Teigha.GraphicsInterface;
using _AcClr = Teigha.Colors;
using _AcWnd = Bricscad.Windows;
using Services;
using Model;

[assembly: CommandClass(typeof(CsBrxMgd.Commands))]
[assembly: ExtensionApplication(typeof(CsBrxMgd.Commands))]

namespace CsBrxMgd
{
    public class Commands : IExtensionApplication
    {
        LoggingService _LoggingService;
        CadEntities _CadEntities;
        BricsCadEntityInjector _BricsCadEntityInjector;
        WcfService _WcfService;

        public Commands()
        {
            try
            {
                _CadEntities = new CadEntities();
                _LoggingService = new LoggingService(_CadEntities, true);
                _BricsCadEntityInjector = new BricsCadEntityInjector(_LoggingService);
                _WcfService = new WcfService(new Action<List<ActionWrapper>>(Process), _LoggingService);
            }
            catch (System.Exception)
            {
            }
        }

        public void Initialize()
        {
            try
            {
                if (RibbonServices.RibbonPaletteSet == null)
                    RibbonServices.CreateRibbonPaletteSet();

                System.Threading.Thread.CurrentThread.Name = Constants.BricscadExtensionProcessName;
                acutPrintf("ProxyClient Initialized!!!");
            }
            catch (System.Exception ex)
            {
                _LoggingService.WriteWithInner(ex, true, string.Format("\nError: {0}\nStackTrace: {1}", ex.Message, ex.StackTrace));
                throw;
            }
            finally
            {
                _LoggingService.Write("ProxyClient Commands Initialized!!", true);
            }
        }

        public void Terminate()
        {
            _LoggingService.Write("ProxyClient Commands terminated!!", true);
        }

        [CommandMethod("Test")]
        static public void Test()
        {
            Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                List<ActionWrapper> myWrappedActions = new List<ActionWrapper>();
                ActionWrapper myAction = new ActionWrapper();
                myAction.Type = ActionWrapper.TypeEnum.AddCircle;
                myWrappedActions.Add(myAction);
                acutPrintf("Start");
                new Commands().Process(myWrappedActions);
                acutPrintf("End");
            }
            catch (System.Exception ex)
            {
                string Error = string.Format("\nError: {0}\nStackTrace: {1}", ex.Message, ex.StackTrace);
                acutPrintf(Error);
            }
        }

        [DllImport("Brx18.DLL", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private extern static IntPtr acutPrintf(string str);

        private void Process(List<ActionWrapper> myWrappedActions)
        {
            try
            {
                for (int i = 0; i < myWrappedActions.Count; i++)
                {
                    ActionWrapper myActions = myWrappedActions[i];

                    if (myActions == null)
                        throw new ArgumentNullException();

                    acutPrintf(myActions.Type.ToString());
                    myWrappedActions[i] = _BricsCadEntityInjector.Dispatcher(myActions);
                }
            }
            catch (System.Exception ex)
            {
                string Error = string.Format("\nError: {0}\nStackTrace: {1}", ex.Message, ex.StackTrace);
                acutPrintf(Error);
                _LoggingService.WriteWithInner(ex, true, Error);
            }
            finally
            {
                _LoggingService.Write("ProxyClient Commands Initialized!!", true);
            }
        }
    }
}