using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Services;

namespace Sistema_Web_Nosql_de_informacion_CAD.Controllers
{
    public class PlaneController : Controller
    {
        private LoggingService _LoggingService;
        private PreferenceRepository _PreferenceRepository;
        private PlaneRepository _PlaneRepository;
        private ScriptRepository _ScriptRepository;

        public PlaneController(LoggingService myLoggingService, PreferenceRepository myPreferenceRepository, PlaneRepository myPlaneRepository,
                              ScriptRepository myScriptRepository)
        {
            _LoggingService = myLoggingService;
            _PreferenceRepository = myPreferenceRepository;
            _PlaneRepository = myPlaneRepository;
            _ScriptRepository = myScriptRepository;
        }

        public ActionResult New()
        {
            PlaneViewModel vmPlane = new PlaneViewModel();
            _PreferenceRepository.LoadBasePreferences(vmPlane);
            return View(vmPlane);
        }

        [HttpPost]
        public ActionResult Save(PlaneViewModel myModel)
        {
            try
            {
                _LoggingService.Write("PlaneController (Save) page access", true);

                var myModelState = ModelState;
                if (_PlaneRepository.Validate(ref myModelState, myModel))
                {
                    myModel.Current.FileContent = ReadAll(myModel.PostedFile);
                    _PlaneRepository.Save(myModel.Current);
                    _LoggingService.Write("PlaneController - Save () successful: ", true);
                    return RedirectToAction("Index", "Home");
                }

                return View("New", myModel);
            }
            catch (Exception ex)
            {
                _LoggingService.WriteWithInner(ex, true, "PlaneController(Save) error: ");
                return new HttpNotFoundResult();
            }
        }

        private byte[] ReadAll(HttpPostedFileBase postedFile)
        {
            using (var binaryReader = new BinaryReader(postedFile.InputStream))
            {
                return binaryReader.ReadBytes(postedFile.ContentLength);
            }
        }
    }
}