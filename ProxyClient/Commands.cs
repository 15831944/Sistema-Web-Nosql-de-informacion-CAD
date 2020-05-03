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
            _CadEntities = new CadEntities();
            _LoggingService = new LoggingService(_CadEntities);
            _BricsCadEntityInjector = new BricsCadEntityInjector(_LoggingService);
            _WcfService = new WcfService(null, _LoggingService);
        }

        public void Initialize()
        {
            try
            {
                if (RibbonServices.RibbonPaletteSet == null)
                    RibbonServices.CreateRibbonPaletteSet();

                _Action<List<ActionWrapper>> jj = new Action<List<ActionWrapper>>();
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


            //To-Do: Wcf initialization
        }

        public void Terminate()
        {
            _LoggingService.Write("ProxyClient Commands terminated!!", true);
        }

        [DllImport("Brx17.DLL", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private extern static IntPtr acutPrintf(string str);
    }
}





