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

        public PlaneController(LoggingService myLoggingService, PreferenceRepository myPreferenceRepository, PlaneRepository myPlaneRepository)
        {
            _LoggingService = myLoggingService;
            _PreferenceRepository = myPreferenceRepository;
            _PlaneRepository = myPlaneRepository;
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
                    myModel.Current.Id = Guid.NewGuid().ToString();
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

        public ActionResult Delete(string name)
        {
            try
            {
                _LoggingService.Write("PlaneController (DeletePlane) page access", true);
                _PlaneRepository.Delete(name);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _LoggingService.WriteWithInner(ex, true, "PlaneController(Delete) error: ");
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