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
        WcfService _WcfService;
        BricsCadEntityInjector _BricsCadEntityInjector;

        public Commands()
        {
            try
            {
                _BricsCadEntityInjector = new BricsCadEntityInjector(null);
                _WcfService = new WcfService(new Action<List<ActionWrapper>>(Process), null);
                acutPrintf("\nCommands constructor ended");
            }
            catch (System.Exception ex)
            {
                string Error = string.Format("\nError: {0}\nStackTrace: {1}", ex.Message, ex.StackTrace);
                acutPrintf(Error);
            }
        }

        public void Initialize()
        {
            try
            {
                if (RibbonServices.RibbonPaletteSet == null)
                    RibbonServices.CreateRibbonPaletteSet();

                System.Threading.Thread.CurrentThread.Name = Constants.BricscadExtensionProcessName;
                acutPrintf("\nProxyClient Initialized!!!");
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public void Terminate()
        {
        }

        [DllImport("Brx18.DLL", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private extern static IntPtr acutPrintf(string str);

        private void Process(List<ActionWrapper> myWrappedActions)
        {
            try
            {
                acutPrintf("Process Started!!");
                for (int i = 0; i < myWrappedActions.Count; i++)
                {
                    ActionWrapper myActions = myWrappedActions[i];

                    if (myActions == null)
                        throw new ArgumentNullException();

                    acutPrintf(myActions.Type.ToString());
                    myWrappedActions[i] = _BricsCadEntityInjector.Dispatcher(myActions);
                    myWrappedActions[i].Status = ActionWrapper.StatusEnum.Ok;
                }
            }
            catch (System.Exception ex)
            {
                string Error = string.Format("\nError: {0}\nStackTrace: {1}", ex.Message, ex.StackTrace);
                acutPrintf(Error);
            }
        }
    }
}